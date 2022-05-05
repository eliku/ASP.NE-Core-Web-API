using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using MetricsAgent.DAL.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.SQLite;
using AutoMapper;
using FluentMigrator.Runner;
using MetricsAgent.DAL.Jobs;
using Quartz.Spi;
using Quartz;
using Quartz.Impl;
using MetricsAgent.DAL;

namespace MetricsAgent
{
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
            services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));
            services.AddSingleton<IConnectionManager, ConnectionManager>();

            services.AddFluentMigratorCore().ConfigureRunner(rb => rb
            // Добавляем поддержку SQLite
            .AddSQLite()
            // Устанавливаем строку подключения
            .WithGlobalConnectionString(ConnectionManager.ConnectionString)
            // Подсказываем, где искать классы с миграциями
            .ScanIn(typeof(Startup).Assembly).For.Migrations()).AddLogging(lb => lb.AddFluentMigratorConsole());
            
            services.AddTransient<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddTransient<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddTransient<IHddMetricsRepository, HddMetricsRepository>();
            services.AddTransient<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddTransient<IRamMetricsRepository,RamMetricsRepository>();

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            // Добавляем сервисы
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            // Добавляем нашу задачу
            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(CpuMetricJob),
            cronExpression: "0/5 * * * * ?")); // Запускать каждые 5 секунд

            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(DotNetMetricJob),
            cronExpression: "0/5 * * * * ?")); // Запускать каждые 5 секунд

            services.AddSingleton<HddMetricJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(HddMetricJob),
            cronExpression: "0/5 * * * * ?")); // Запускать каждые 5 секунд

            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(NetworkMetricJob),
            cronExpression: "0/5 * * * * ?")); // Запускать каждые 5 секунд

            services.AddSingleton<RamMetricJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(RamMetricJob),
            cronExpression: "0/5 * * * * ?"));// Запускать каждые 5 секунд

            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton<HddMetricJob>();
            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton<RamMetricJob>();

            services.AddSingleton<QuartzHostedService>();
            services.AddHttpClient();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // Запускаем миграции

            migrationRunner.MigrateDown(1);
            migrationRunner.MigrateUp();
        }

    }
}
