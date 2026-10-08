using Asp.Versioning;
using AutoMapper;
using Core.Infrastructure.Services;
using Core.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Core.WebAPI.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [ApiController]
    [Route("api/v{v:apiVersion}/core/[controller]")]
    public class GenericController<TEntity, TContext, TGetDto, TCreateDto, TUpdateDto> : ControllerBase
     where TEntity : class
     where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IMapper _mapper;
        private readonly TheNumbersService _theNumbersService;
        private static readonly string[] PrimaryKeyNames =
        {
            "SerialID", "DocTypeSerialID","FilePathSerialID","AssetId","BankSerialID","BankBrnchSerialID",
            "ComSerialID","CntrySerialID","CurSerialID","DocTypeSerialID","DocUploadSerialID","FilePathSerialID","GrpSerialID",
            "GrpMnuSerialID","InvdTokenID","LoginLogSerialID","MnuSerialID","ModSerialID","RTSerialID","TheNumberSerialID",
            "UserSerialID","UserComSerialID","UserMnuPermsSerialID","DelRecSerialID","ReasonSerialID","StateSerialID","CitySerialID"
        };
        /// <summary>
        /// Initializes a new instance of the generic controller.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entities and DTOs.</param>
        public GenericController(TContext context, IMapper mapper, TheNumbersService theNumbersService)
        {
            _context = context;
            _mapper = mapper;
            _theNumbersService = theNumbersService;
        }

        #region CRUD Operations

        [HttpGet(Name = "GetAll")]
        public virtual async Task<ActionResult<IEnumerable<TGetDto>>> GetAll()
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            var dtos = _mapper.Map<List<TGetDto>>(entities);
            return Ok(dtos);
        }
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TGetDto>> Get(int id)
        {
            // Try to find a matching key property from known key names
            var entityType = typeof(TEntity);
            var keyProperty = entityType.GetProperties()
                .FirstOrDefault(p => PrimaryKeyNames.Contains(p.Name));

            if (keyProperty == null)
                return BadRequest("Unable to determine primary key.");

            // Convert id to the correct type
            var keyType = keyProperty.PropertyType;
            object typedKey;

            try
            {
                typedKey = Convert.ChangeType(id, keyType);
            }
            catch (Exception)
            {
                return BadRequest($"Invalid key type. Expected: {keyType.Name}");
            }

            // Find entity
            var entity = await _context.Set<TEntity>().FindAsync(typedKey);
            if (entity == null) return NotFound();

            var dto = _mapper.Map<TGetDto>(entity);
            return Ok(dto);
        }

 
        [HttpPost]
        public virtual async Task<ActionResult<TGetDto>> Create([FromBody] TCreateDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            var entity = _mapper.Map<TEntity>(createDto);

            if (uniquePropertyNames?.Length > 0 && await CheckDuplicatesAsync(createDto, uniquePropertyNames))
            {
                return Conflict(new { message = $"A record already exists." });
            }

            // Fetch and increment LastNumber for the entity name
            var lastNumber = await _theNumbersService.GetAndIncrementLastNumberAsync(typeof(TEntity).Name);
            Console.WriteLine($"Generated LastNumber for {typeof(TEntity).Name}: {lastNumber}");


            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<TGetDto>(entity);
            var primaryKeyValue = GetPrimaryKeyValue(entity);

            if (primaryKeyValue == null)
                throw new InvalidOperationException("No primary key property found for the entity.");

            return CreatedAtAction(nameof(Get), new { id = primaryKeyValue }, dto);
        }

        [HttpPut]
        public virtual async Task<ActionResult<TGetDto>> Update([FromBody] TUpdateDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            if (updateDto == null) return BadRequest("Invalid update data.");

            var idValue = GetIdValueFromDto(updateDto);

            if (idValue == null) return BadRequest("The update DTO does not contain a valid ID.");
            var entity = await FindEntityByIdAsync(idValue);
            if (entity == null) return NotFound("Entity not found.");
            var primaryKeyPropertyName = GetPrimaryKeyPropertyName(entity);
            if (uniquePropertyNames?.Length > 0 && await CheckDuplicatesAsync(updateDto, uniquePropertyNames, primaryKeyPropertyName))
            {
                return Conflict(new { message = $"A record already exists." });
            }

            _mapper.Map(updateDto, entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = idValue }, updateDto);

        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id, [FromQuery] bool hardDelete = false)
        {
            var dbSet = _context.Set<TEntity>();

            var keyPropertyName = _context.Model
                .FindEntityType(typeof(TEntity))?
                .FindPrimaryKey()?
                .Properties
                .FirstOrDefault()?
                .Name;

            if (string.IsNullOrEmpty(keyPropertyName))
                return BadRequest("Primary key not found.");

            var entity = await dbSet
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(e => EF.Property<object>(e, keyPropertyName).Equals(id));

            if (entity == null)
                return NotFound();

            if (hardDelete && _context is ApplicationDbContext ctx)
            {
                ctx.BypassSoftDelete = true; // ✅ enable hard delete
            }

            dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            if (_context is ApplicationDbContext ctxReset)
            {
                ctxReset.BypassSoftDelete = false; // ✅ reset to default
            }

            return NoContent();
        }

        #endregion

        #region Helper Methods

        private object? GetIdValueFromDto(TUpdateDto dto)
        {
            var idProperty = typeof(TUpdateDto).GetProperties()
                .FirstOrDefault(prop => PrimaryKeyNames.Contains(prop.Name, StringComparer.OrdinalIgnoreCase));
            return idProperty?.GetValue(dto);
        }

        private async Task<TEntity?> FindEntityByIdAsync(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        private object? GetPrimaryKeyValue(TEntity entity)
        {
            var primaryKeyProperty = entity.GetType().GetProperties()
                .FirstOrDefault(prop => PrimaryKeyNames.Contains(prop.Name, StringComparer.OrdinalIgnoreCase));
            return primaryKeyProperty?.GetValue(entity);
        }

        private async Task<bool> CheckDuplicatesAsync(TUpdateDto updateDto, string[] uniquePropertyNames, string keyPropertyName)
        {
            if (updateDto == null)
                throw new ArgumentNullException(nameof(updateDto));
            if (uniquePropertyNames == null || uniquePropertyNames.Length == 0)
                throw new ArgumentException("Unique property names must be provided.", nameof(uniquePropertyNames));
            if (string.IsNullOrWhiteSpace(keyPropertyName))
                throw new ArgumentException("Key property name must be provided.", nameof(keyPropertyName));

            // Extract the key property value from the updateDto (used for excluding the current entity)
            var keyPropertyInfo = updateDto.GetType().GetProperty(keyPropertyName);
            if (keyPropertyInfo == null)
                throw new ArgumentException($"Key property '{keyPropertyName}' does not exist on type '{typeof(TUpdateDto).Name}'.");

            var keyPropertyValue = keyPropertyInfo.GetValue(updateDto);
            List<bool> boolList = new List<bool>();

            // Add conditions for each unique property with exact match for strings or other types
            foreach (var propertyName in uniquePropertyNames)
            {
                // Get the DbSet for the entity type
                IQueryable<TEntity> query = _context.Set<TEntity>();

                if (keyPropertyValue != null)
                {
                    // Exclude the current entity from the query (same way as before)
                    query = query.Where(e => !EF.Property<object>(e, keyPropertyName).Equals(keyPropertyValue));
                }

                var propertyInfo = updateDto.GetType().GetProperty(propertyName);
                if (propertyInfo == null)
                    throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(TUpdateDto).Name}'.");

                var propertyValue = propertyInfo.GetValue(updateDto);

                if (propertyValue != null)
                {
                    if (propertyValue is string strValue)
                    {
                        // Use exact match for string properties
                        query = query.Where(e => EF.Property<string>(e, propertyName) == strValue);
                        boolList.Add(await query.AnyAsync());
                    }
                    else
                    {
                        // Handle non-string properties as exact matches
                        query = query.Where(e => EF.Property<object>(e, propertyName).Equals(propertyValue));
                        boolList.Add(await query.AnyAsync());
                    }
                }
                else
                {
                    // Handle null values explicitly
                    query = query.Where(e => EF.Property<object>(e, propertyName) == null);
                    boolList.Add(await query.AnyAsync());
                }
            }

            // Check if any of the bool values in the list are true
            bool hasDuplicate = boolList.Any(b => b == true);
            return hasDuplicate;
        }

        private async Task<bool> CheckDuplicatesAsync(TCreateDto createDto, string[] uniquePropertyNames)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));
            if (uniquePropertyNames == null || uniquePropertyNames.Length == 0)
                throw new ArgumentException("Unique property names must be provided.", nameof(uniquePropertyNames));

            // store results in a list to query if any of the results are true or false
            List<bool> boolList = new List<bool>();


            foreach (var propertyName in uniquePropertyNames)
            {
                // Start with all entities of the type
                var query = _context.Set<TEntity>().AsQueryable();

                // Get the property info
                var propertyInfo = createDto.GetType().GetProperty(propertyName);
                if (propertyInfo == null)
                {
                    throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(TEntity).Name}'.");
                }

                // Get the property value
                var propertyValue = propertyInfo.GetValue(createDto);
                if (propertyValue != null)
                {
                    if (propertyValue is string strValue)
                    {
                        // Use exact match for string properties
                        query = query.Where(e => EF.Property<string>(e, propertyName) == strValue);
                        boolList.Add(await query.AnyAsync());
                    }
                    else
                    {
                        // Handle non-string properties as exact matches
                        query = query.Where(e => EF.Property<object>(e, propertyName).Equals(propertyValue));
                        boolList.Add(await query.AnyAsync());
                    }

                }
                else
                {
                    // Handle null values explicitly if needed
                    query = query.Where(e => EF.Property<object>(e, propertyName) == null);
                    boolList.Add(await query.AnyAsync());
                }
            }

            // Check if any of the bool values in the list are true
            bool hasDuplicate = boolList.Any(b => b == true);
            return hasDuplicate;
        }

        private string? GetPrimaryKeyPropertyName(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Common naming conventions for primary keys
            var primaryKeyProperty = entity.GetType().GetProperties()
                .FirstOrDefault(prop => PrimaryKeyNames.Contains(prop.Name, StringComparer.OrdinalIgnoreCase));

            return primaryKeyProperty?.Name;
        }

        #endregion
    }
}
