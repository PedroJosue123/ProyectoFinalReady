namespace Proyecto.Middleware;

using Domain.Dtos;
using System.Text.Json;



public class RegisterValidationMiddleware
{
    private readonly RequestDelegate _next;

    public RegisterValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.Equals("/api/Auth/register", StringComparison.OrdinalIgnoreCase) &&
            context.Request.Method == HttpMethods.Post)
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            var dto = JsonSerializer.Deserialize<RegisterRequestDto>(body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (dto is null ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password) ||
                string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.ManagerName) ||
                string.IsNullOrWhiteSpace(dto.ManagerDni) ||
                string.IsNullOrWhiteSpace(dto.ManagerEmail) ||
                string.IsNullOrWhiteSpace(dto.Phone) ||
                string.IsNullOrWhiteSpace(dto.Address) ||
                string.IsNullOrWhiteSpace(dto.PaymentPasswordHash) ||
                dto.UserTypeId <= 0)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { message = "Datos del registro incompletos o inválidos." });
                return;
            }
        }

        await _next(context);
    }
}