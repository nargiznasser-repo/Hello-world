namespace MariApps.MS.ITP.MSA.Program.Api
{
    using MariApps.Framework.Audit;
    using MariApps.Framework.DataAccess;
    using Mariapps.Framework.MS.RequestCorrelation;
    using MariApps.MS.ITP.MSA.Program.Business;
    using MariApps.MS.ITP.MSA.Program.Business.Contract;
    using MariApps.MS.ITP.MSA.Program.DAL.Context;
    using MariApps.MS.ITP.MSA.Program.DAL.DataContract;
    using MariApps.MS.ITP.MSA.Program.DAL.Repositories;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRequestCorrelation();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddAuditService(Configuration);
            services.AddScoped<IDBContext, AdoDbContext>();
            services.AddControllers();

            services.AddScoped<IProgramsDeploymentService, ProgramsDeploymentService>();
            services.AddScoped<IProgramsDeploymentAdoRepository, ProgramsDeploymentAdoRepository>();

            services.AddScoped<IItineraryService, ItineraryService>();
            services.AddScoped<IItineraryAdoRepository, ItineraryAdoRepository>();

            services.AddScoped<ICommonProgramService, CommonProgramService>();
            services.AddScoped<ICommonProgramAdoRepository, CommonProgramAdoRepository>();

            services.AddScoped<IItineraryMasterService, ItineraryMasterService>();
            services.AddScoped<IItineraryMasterAdoRepository, ItineraryMasterAdoRepository>();

            services.AddScoped<IManageBusinessFlowService, ManageBusinessFlowService>();
            services.AddScoped<IManageBusinessFlowAdoRepository, ManageBusinessFlowAdoRepository>();

            services.AddScoped< IProcessTemplateService, ProcessTemplateService>();
            services.AddScoped<IProcessTemplateAdoRepository, ProcessTemplateAdoRepository>();

            services.AddScoped<ICopyItineraryService, CopyItineraryService>();
            services.AddScoped<ICopyItineraryAdoRepository, CopyItineraryAdoRepository>();

            services.AddScoped<IItineraryDetailsService, ItineraryDetailsService>();
            services.AddScoped<IItineraryDetailsAdoRepository, ItineraryDetailsAdoRepository>();

            #region FunctionalRole
            services.AddScoped<IFunctionalRoleService, FunctionalRoleService>();
            services.AddScoped<IFunctionalRoleRepository, FunctionalRoleRepository>();
            #endregion

            #region AllocationOfVessel
            services.AddScoped<IAllocationOfVesselService, AllocationOfVesselService>();
            services.AddScoped<IAllocationOfVesselRepository, AllocationOfVesselRepository>();
            #endregion

            services.AddScoped<ICopyPastItineraryService, CopyPastItineraryService>();
            services.AddScoped<ICopyPastItineraryAdoRepository, CopyPastItineraryAdoRepository>();

            services.AddScoped<IProgramsCompareService, ProgramsCompareService>();
            services.AddScoped<IProgramsCompareAdoRepository, ProgramsCompareAdoRepository>();

            #region ItinerarySegmentSummary
            services.AddScoped<IItinerarySegmentSummaryService, ItinerarySegmentSummaryService>();
            services.AddScoped<IItinerarySegmentSummaryRepository, ItinerarySegmentSummaryRepository>();

            services.AddScoped<IProgramsPerformaService, ProgramsPerformaService>();
            services.AddScoped<IProgramsPerformaAdoRepository, ProgramsPerformaAdoRepository>();
            #endregion

            services.AddScoped<IManagePortCostResquestService, ManagePortCostResquestService>();
            services.AddScoped<IManagePortCostResquestRepository, ManagePortCostResquestRepository>();

            services.AddScoped<IItineraryReportService, ItineraryReportService>();
            services.AddScoped<IItineraryReportRepository, ItineraryReportRepository>();

            #region PortStaySummary
            services.AddScoped<IPortStaySummaryService, PortStaySummaryService>();
            services.AddScoped<IPortStaySummaryAdoRepository, PortStaySummaryAdoRepository>();
            #endregion

            #region ArchiveDetails
            services.AddScoped<IArchiveDetailsService, ArchiveDetailsService>();
            services.AddScoped<IArchiveDetailsAdoRepository, ArchiveDetailsAdoRepository>();
            #endregion

            #region ManageCIIReport
            services.AddScoped<IManageCIIReportService, ManageCIIReportService>();
            services.AddScoped<IManageCIIReportRepository, ManageCIIReportRepository>();
            #endregion

            # region ItineraryOverview

            services.AddScoped<IItineraryOverviewService, ItineraryOverviewService>();
            services.AddScoped<IItineraryOverviewRepository, ItineraryOverviewRepository>();

            #endregion

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRequestCorrelation();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MariApps.MS.ITP.MSA.Program API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
