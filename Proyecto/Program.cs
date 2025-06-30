using Application.IUseCase;
using Application.UseCase;
using Proyecto.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddScoped<ILoginUser, LoginUser>();
builder.Services.AddScoped<IRegisterUser, RegisterUser>();
builder.Services.AddScoped<IOrder, Order>();
builder.Services.AddScoped<IOrderRequests, OrderRequests>();
builder.Services.AddScoped<IPaymentOrder, PaymentOrder>();
builder.Services.AddScoped<ISendOrder, SendOrder>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseSwagger();

    // Swagger UI en la raíz "/"
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = string.Empty; // Esto hace que Swagger UI esté en la raíz
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();