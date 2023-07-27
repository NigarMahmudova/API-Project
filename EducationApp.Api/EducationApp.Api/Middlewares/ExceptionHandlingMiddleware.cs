using EducationApp.Service.Exceptions;

namespace EducationApp.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
               await _next(context);
            }
            catch (Exception e)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                List<RestExceptionErrorItem> errors = new List<RestExceptionErrorItem>();
                string message = e.Message;

                switch (e)
                {
                    case RestException re:
                        response.StatusCode = (int)re.Code;
                        errors = re.Errors;
                        message = re.Message;
                        break;
                    default:
                        break;
                }

                await response.WriteAsJsonAsync(new { message, errors = errors});
            }
        }
    }
}
