using FoodDelivery.Models.DTO;
using Microsoft.AspNetCore.Http;

namespace FoodDelivery.Services.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) 
        { 
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (IncorrectDataException e)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new Response { Status = "400", Message = e.Message });
            }
            catch (ItemNotFoundException e)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new Response { Status = "404", Message = e.Message });
            }
            catch (NoPermissionException e)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new Response { Status = "403", Message = e.Message });
            }
            catch (ElementAlreadyExistsException e)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new Response { Status = "400", Message = e.Message });
            }
            catch (Exception e)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new Response
                {
                    Status = StatusCodes.Status500InternalServerError.ToString(),
                    Message = e.Message
                });
            }
        }
    }
}
