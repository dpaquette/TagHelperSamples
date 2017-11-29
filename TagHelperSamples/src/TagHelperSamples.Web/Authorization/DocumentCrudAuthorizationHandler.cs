using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TagHelperSamples.Web.Model;

namespace TagHelperSamples.Web.Authorization
{
    public class DocumentAuthorizationCrudHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, Document>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Document resource)
        {
            if (context.User.Identity?.Name == resource.Author &&
                requirement.Name == Operations.Delete.Name)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
