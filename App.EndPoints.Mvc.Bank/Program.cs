using App.Domain.AppServices.Bank.Transaction;
using App.Domain.Core.Bank.Card.AppServices;
using App.Domain.Core.Bank.Transaction.AppServices;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddScoped<IRepositoryCard,RepositoryCard>();
builder.Services.AddScoped<IRepositoryTransaction,RepositoryTransaction>();
builder.Services.AddScoped<IRepositoryUser,RepositoryUser>();
builder.Services.AddScoped<ICardAppService, CardAppService>();
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<ITransactionAppService,TransactionAppService>();
builder.Services.AddScoped<IServiceCard,ServiceCard>();
builder.Services.AddScoped<IServiceUser,ServiceUser>();
builder.Services.AddScoped<IServiceTransaction,ServiceTransaction>();
builder.Services.AddSingleton<AppDbContext, AppDbContext>();
//builder.Services.AddMvc()
    
//.AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Card}/{action=Login}/{id?}");

app.Run();
