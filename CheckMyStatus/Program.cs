using CheckMyStatus;
using CheckMyStatus.Domain;
using Microsoft.EntityFrameworkCore;
using Quartz;
using QuartzApp.Jobs;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connection));

builder.Services.InitializeRepositories();
builder.Services.InitializeServices();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var emailSendingJobKey = new JobKey("emailSendingJob");
    q.AddJob<EmailSender>(opts => opts.WithIdentity(emailSendingJobKey));
    q.AddTrigger(opts => opts
        .ForJob(emailSendingJobKey)
        .WithIdentity("emailSendingJob-trigger")
        .StartAt(DateBuilder.DateOf(7, 00, 00))
        .WithSimpleSchedule(x => x
            .WithIntervalInHours(24)
            .RepeatForever())); ;

});

builder.Services.AddQuartzHostedService(
    q => q.WaitForJobsToComplete = true);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
