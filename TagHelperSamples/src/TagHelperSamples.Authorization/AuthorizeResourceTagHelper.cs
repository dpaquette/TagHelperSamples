using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace TagHelperSamples.Authorization
{
    [HtmlTargetElement(Attributes = "asp-authorize-resource,asp-policy")]
    [HtmlTargetElement(Attributes = "asp-authorize-resource,asp-requirement")]
    public class AuthorizeResourceTagHelper : TagHelper
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizeResourceTagHelper(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Gets or sets the policy name that determines access to the HTML block.
        /// </summary>
        [HtmlAttributeName("asp-policy")]
        public string Policy { get; set; }

        /// <summary>
        /// Gets or sets a comma delimited list of roles that are allowed to access the HTML  block.
        /// </summary>
        [HtmlAttributeName("asp-requirement")]
        public IAuthorizationRequirement Requirement { get; set; }


        /// <summary>
        /// Gets or sets the resource to be authorized against a particular policy
        /// </summary>
        [HtmlAttributeName("asp-authorize-resource")]
        public object Resource { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (Resource == null)
            {
                throw new ArgumentException("Resource cannot be null");                
            }
            if (string.IsNullOrWhiteSpace(Policy) && Requirement == null)
            {
                throw new ArgumentException("Either Policy or Requirement must be specified");
                
            }
            if (!string.IsNullOrWhiteSpace(Policy) && Requirement != null)
            {
                throw new ArgumentException("Policy and Requirement cannot be specified at the same time");
            }

            AuthorizationResult authorizeResult;

            if (!string.IsNullOrWhiteSpace(Policy))
            {
                authorizeResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, Resource, Policy);
            }
            else if (Requirement != null)
            {
                authorizeResult =
                    await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, Resource,
                        Requirement);
            }
            else
            {
                throw new ArgumentException("Either Policy or Requirement must be specified");
            }

            if (!authorizeResult.Succeeded)
            {
                output.SuppressOutput();
            }
        }
    }
}
