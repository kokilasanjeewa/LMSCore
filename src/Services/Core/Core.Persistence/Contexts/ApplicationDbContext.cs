
using Core.Domain.Entities;
using Core.Domain.Common;
using Core.Domain.Common.interfaces;
using Core.Persistence.Audit;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using AppModule = Core.Domain.Entities.AppModule;
using Core.Application.Interfaces;
using Core.Application.Interfaces.Repositories;

namespace Core.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IHttpContextAccessor httpcontext;

        private readonly IDomainEventDispatcher _dispatcher;
        public bool BypassSoftDelete { get; set; } = false;


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
          IDomainEventDispatcher dispatcher, IHttpContextAccessor _httpcontext)
            : base(options)
        {
            _dispatcher = dispatcher;
            httpcontext = _httpcontext;
        }

        public DbSet<TheNumber> TheNumbers => Set<TheNumber>();
        public DbSet<Currency> Currency => Set<Currency>();
        public DbSet<Country> Country => Set<Country>();
        public DbSet<Company> Company => Set<Company>();
        public DbSet<User> User => Set<User>();
        public DbSet<AppModule> Module => Set<AppModule>();
        public DbSet<Menu> Menu => Set<Menu>();
        public DbSet<UserMenuPermission> UserMenuPermission => Set<UserMenuPermission>();
        public DbSet<UserCompany> UserCompany => Set<UserCompany>();
        public DbSet<Group> Group => Set<Group>();
        public DbSet<GroupMenu> GroupMenu => Set<GroupMenu>();
        public DbSet<InvalidateToken> InvalidateToken => Set<InvalidateToken>();
        public DbSet<RefreshToken> RefreshToken => Set<RefreshToken>();
        public DbSet<AuditTrail> AuditTrail { get; set; }
        public DbSet<LoginLog> LoginLog { get; set; }
        public DbSet<DocType> DocType => Set<DocType>();
        public DbSet<DocUpload> DocUpload => Set<DocUpload>();
        public DbSet<FilePaths> FilePaths => Set<FilePaths>();
        public DbSet<DelRecord> DelRecord => Set<DelRecord>();
        public DbSet<Reason> Reason => Set<Reason>();
        public DbSet<Report> Report => Set<Report>();
        public DbSet<City> City => Set<City>();
        public DbSet<State> State => Set<State>();
        public DbSet<Region> Region => Set<Region>();
        public DbSet<Subregion> Subregion => Set<Subregion>();
        public DbSet<EntityType> EntityType => Set<EntityType>();
        public DbSet<ApprovalActionHistory> ApprovalActionHistory => Set<ApprovalActionHistory>();
        public DbSet<ApprovalRequest> ApprovalRequest => Set<ApprovalRequest>();
        public DbSet<ApprovalRequestStep> ApprovalRequestStep => Set<ApprovalRequestStep>();
        public DbSet<ApprovalStep> ApprovalStep => Set<ApprovalStep>();
        public DbSet<ApprovalWorkflow> ApprovalWorkflow => Set<ApprovalWorkflow>();
        public DbSet<ApprovalStepApprover> ApprovalStepApprover => Set<ApprovalStepApprover>();
        public DbSet<CostCenter> CostCenter => Set<CostCenter>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Global query filter
            var softDeletableInterface = typeof(ISoftDeletable);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;

                if (!softDeletableInterface.IsAssignableFrom(clrType))
                    continue;

                var method = typeof(ApplicationDbContext)
                    .GetMethod(nameof(SetSoftDeleteFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?
                    .MakeGenericMethod(clrType);

                method?.Invoke(null, new object[] { modelBuilder });
            }
            base.OnModelCreating(modelBuilder);

            //Configure default schema
            modelBuilder.HasDefaultSchema("Core");
            modelBuilder.HasSequence<int>("BNKBRID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("BNKID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("TheNumberID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("CNTRYID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("COMID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("CURNCYID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("DEPTID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("GrpComID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("GrpID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("GrpMnuID", schema: "dbo").StartsAt(1).IncrementsBy(1);
        //    modelBuilder.HasSequence<int>("MNUID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("MODID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("RTID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SECTID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("UserComID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("UserMnuPermsID", schema: "dbo").StartsAt(1).IncrementsBy(1);

            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        }
        private static void SetSoftDeleteFilter<TEntity>(ModelBuilder builder) where TEntity : class, ISoftDeletable
        {
            builder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int userSerialID = 0;
            long loginLogSerialID = 0;
            var token = httpcontext?.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                userSerialID = Convert.ToInt32(jwt.Claims.First(c => c.Type == "userSerialID").Value);
                loginLogSerialID = Convert.ToInt64(jwt.Claims.First(c => c.Type == "loginLogSerialID").Value);
            }
            var mnuHeader = httpcontext?.HttpContext?.Request?.Headers["MnuSerialID"].FirstOrDefault();

            int mnuSerialID = 0;
            if (!string.IsNullOrWhiteSpace(mnuHeader))
            {
                int.TryParse(mnuHeader, out mnuSerialID);
            }
            foreach (var entry in ChangeTracker.Entries<AuditTrail>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                entry.Entity.UserSerialID = userSerialID;
                entry.Entity.LoginLogSerialID = loginLogSerialID;
                entry.Entity.MnuSerialID = mnuSerialID;

            }
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}





//modelBuilder.Entity<RefreshToken>().Property(e => e.RTID).HasDefaultValueSql("NEXT VALUE FOR dbo.RTID");

/*var token = httpcontext?.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                string user = jwt.Claims.First(c => c.Type == "user").Value;
                modelBuilder.Entity<AuditTrail>(a =>
                {
                    a.Property(x => x.FrmSerialID).HasDefaultValue(token);
                });
            }*/