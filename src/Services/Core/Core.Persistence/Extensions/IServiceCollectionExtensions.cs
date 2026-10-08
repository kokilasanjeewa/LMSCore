using Core.Application.Interfaces.Repositories;
using Core.Persistence.Contexts;
using Core.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System;
using Core.Persistence.DbContextInterceptors;
using Audit.EntityFramework;

namespace Core.Persistence.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMappings();
            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
            services.AddSingleton<SoftDeleteInterceptor>();
            services.AddDbContext(configuration);
            services.AddSingleton<DapperContext>();
            services.AddRepositories();
            services.AddHttpContextAccessor();
        }

        //private static void AddMappings(this IServiceCollection services)
        //{
        //    services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //}

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var updateAuditableInterceptor = serviceProvider.GetService<UpdateAuditableEntitiesInterceptor>();
                 options.UseSqlServer(connectionString,
                 builder => {
                     builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                     builder.EnableRetryOnFailure(maxRetryCount: 3, // Number of retry attempts
                maxRetryDelay: TimeSpan.FromSeconds(5), // Delay between retries
                errorNumbersToAdd: null);

                 })
                .AddInterceptors(updateAuditableInterceptor).AddInterceptors(serviceProvider.GetRequiredService<SoftDeleteInterceptor>()).AddInterceptors(new AuditSaveChangesInterceptor());
            });
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient(typeof(IUserRepository), typeof(UserRepository))
                .AddTransient<ICountryRepository, CountryRepository>()
                .AddTransient<ISqlDataAccess, SqlDataAccess>()
                .AddTransient(typeof(ISortHelper<>), typeof(SortHelper<>))
                .AddTransient(typeof(IGroupRepository), typeof(GroupRepository))
                .AddTransient(typeof(IDocTypeRepository), typeof(DocTypeRepository))
                .AddTransient(typeof(ICostCenterRepository), typeof(CostCenterRepository))

                .AddTransient(typeof(IApprovalService), typeof(ApprovalService))
                .AddTransient(typeof(IUserRoleService), typeof(UserRoleService))
                .AddTransient(typeof(IApproverResolver), typeof(ApproverResolver))
                .AddTransient(typeof(IApprovalAdminService), typeof(ApprovalAdminService))
                
               .AddTransient(typeof(ICompanyRepository), typeof(CompanyRepository));

        }
    }
}
