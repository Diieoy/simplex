using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

namespace ConsoleAppHost
{
    public class AuthorizationPolicy : IAuthorizationPolicy
    {
        static AuthorizationPolicy(){}

        public ClaimSet Issuer => ClaimSet.System;

        public string Id => Guid.NewGuid().ToString();

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            var identity = (evaluationContext.Properties["Identities"] as List<IIdentity>)?.Single();
            var claimsIdentity = identity is null ? new ClaimsIdentity() : new ClaimsIdentity(identity);
            if (identity != null)
            {
                if(UserPasswordValidator.Role != null)
                {
                    claimsIdentity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, UserPasswordValidator.Role));
                }                
            }

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            evaluationContext.Properties["Principal"] = claimsPrincipal;

            Thread.CurrentPrincipal = claimsPrincipal;
            return true;
        }
    }
}
