namespace Presentation.API.Auth
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Models.Domain.Enums;
    using System;
    using System.Linq;
    using System.Security.Claims;

    public class ScopeAndRoleAuthorizationFilter : IAuthorizationFilter
    {
        private string scope;
        private ERole role;

        public ScopeAndRoleAuthorizationFilter(string scope = "", ERole role = 0)
        {
            this.scope = scope;
            this.role = role;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!IsAuthenticated(context))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var isUser = context.HttpContext.User.Claims.Any(c => c.Type == CustomClaims.User_Id);
            if (isUser)
                this.AuthenticateUser(context);
            else
                this.AuthenticateClient(context);
        }

        private bool IsAuthenticated(AuthorizationFilterContext context) =>
            context.HttpContext.User.Identity.IsAuthenticated;

        private void AuthenticateClient(AuthorizationFilterContext context)
        {
            if (!String.IsNullOrEmpty(this.scope))
            {
                var hasScope = context.HttpContext.User.Claims.Any(c => c.Type == "scope" && c.Value == this.scope);
                if (!hasScope)
                    context.Result = new ForbidResult();
            }
        }

        private void AuthenticateUser(AuthorizationFilterContext context)
        {
            var role = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role) ?? throw new Exception($"Role {this.role} not found in user");
            if (this.role == 0 || this.role.ToString() == role.Value)
                return;
            else
                context.Result = new ForbidResult();
        }
    }

}
