using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TagHelperSamples.Web.Model;

namespace TagHelperSamples.Web.Authorization
{
    public class DocumentSameAuthorAuthorizationHandler :
        AuthorizationHandler<SameAuthorRequirement, Document>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            SameAuthorRequirement requirement,
            Document resource)
        {
            if (context.User.Identity?.Name == resource.Author)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class SameAuthorRequirement : IAuthorizationRequirement { }
}
