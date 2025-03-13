using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ILS_BE.Application.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var controller = context.RouteData.Values["controller"]?.ToString();
            var action = context.RouteData.Values["action"]?.ToString();

            if (string.IsNullOrEmpty(controller) || string.IsNullOrEmpty(action))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var permission = $"{controller.Replace("Controller", "")}.{action}";

            var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            var user = context.HttpContext.User;
            var authResult = authorizationService.AuthorizeAsync(user, permission).Result;

            if (!authResult.Succeeded)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
