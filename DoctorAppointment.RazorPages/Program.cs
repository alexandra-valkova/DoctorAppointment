using DoctorAppointment.Application;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.Persistence;
using Microsoft.EntityFrameworkCore;
using static DoctorAppointment.Domain.Constants.Database;
using static DoctorAppointment.Domain.Constants.Roles;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDatabase(builder.Configuration.GetConnectionString(ConnectionStringName) ??
    throw new InvalidOperationException($"Connection string '{ConnectionStringName}' not found."));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
}).AddIdentityStorageProvider();

// Repositories and Services
builder.Services.AddRepositories();
builder.Services.AddServices();

// Authorization
builder.Services.AddAuthorizationBuilder()
                .AddPolicy(name: Admin, policy => policy.RequireRole(Admin))
                .AddPolicy(name: Patient, policy => policy.RequireRole(Patient))
                .AddPolicy(name: Doctor, policy => policy.RequireRole(Doctor));

builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
    options.Conventions.AuthorizeAreaFolder(areaName: Admin, folderPath: "/", policy: Admin);
    options.Conventions.AuthorizeAreaFolder(areaName: Patient, folderPath: "/", policy: Patient);
    options.Conventions.AuthorizeAreaFolder(areaName: Doctor, folderPath: "/", policy: Doctor);
    options.Conventions.AllowAnonymousToAreaPage(areaName: "Account", pageName: "/AccessDenied");
});

builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePagesWithReExecute("/Errors/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
