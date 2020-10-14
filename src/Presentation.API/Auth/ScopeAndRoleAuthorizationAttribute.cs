namespace Presentation.API.Auth
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Domain.Enums;

    public class ScopeAndRoleAuthorizationAttribute : TypeFilterAttribute
    {
        public ScopeAndRoleAuthorizationAttribute(string scope = "", ERole role = 0) : base(typeof(ScopeAndRoleAuthorizationFilter))
        {
            Arguments = new object[] { scope, role };
        }
    }
}
