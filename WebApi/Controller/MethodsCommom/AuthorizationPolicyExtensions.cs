using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller.MethodsCommom
{
    public static class AuthorizationPolicyExtensions
    {
        public static void AddRoleBasedPolicy(this AuthorizationOptions options, string policyName, string role)
        {
            options.AddPolicy(policyName, policy => policy.RequireRole(role));
        }

        public static void AddRoleAndClaimBasedPolicy(this AuthorizationOptions options, string policyName, string role, string claimType, string claimValue)
        {
            options.AddPolicy(policyName, policy =>
                policy.RequireRole(role).RequireClaim(claimType, claimValue));
        }

        public static void AddCustomPolicy(this AuthorizationOptions options, string policyName, Func<AuthorizationHandlerContext, bool> requirement)
        {
            options.AddPolicy(policyName, policy => policy.RequireAssertion(requirement));
        }
    }

}
