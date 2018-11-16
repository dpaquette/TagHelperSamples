using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace TagHelperSamples.Authorization
{
    [HtmlTargetElement(Attributes = "asp-authorize")]
    [HtmlTargetElement(Attributes = "asp-authorize,asp-policy")]
    [HtmlTargetElement(Attributes = "asp-authorize,asp-roles")]
    [HtmlTargetElement(Attributes = "asp-authorize,asp-authentication-schemes")]
    public class AuthorizeTagHelper : TagHelper, IAuthorizeData
    {
        private readonly IAuthorizationPolicyProvider _policyProvider;
        private readonly IPolicyEvaluator _policyEvaluator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizeTagHelper(IHttpContextAccessor httpContextAccessor, IAuthorizationPolicyProvider policyProvider, IPolicyEvaluator policyEvaluator)
        {
            _httpContextAccessor = httpContextAccessor;
            _policyProvider = policyProvider;
            _policyEvaluator = policyEvaluator;
        }

        /// <summary>
        /// Gets or sets the policy name that determines access to the HTML block.
        /// </summary>
        [HtmlAttributeName("asp-policy")]
        public string Policy { get; set; }

        /// <summary>
        /// Gets or sets a comma delimited list of roles that are allowed to access the HTML  block.
        /// </summary>
        [HtmlAttributeName("asp-roles")]
        public string Roles { get; set; }

        /// <summary>
        /// Gets or sets a comma delimited list of schemes from which user information is constructed.
        /// </summary>
        [HtmlAttributeName("asp-authentication-schemes")]
        public string AuthenticationSchemes { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var policy = await AuthorizationPolicy.CombineAsync(_policyProvider, new[] { this });

            var authenticateResult = await _policyEvaluator.AuthenticateAsync(policy, _httpContextAccessor.HttpContext);

            var authorizeResult = await _policyEvaluator.AuthorizeAsync(policy, authenticateResult, _httpContextAccessor.HttpContext, null);

            if (!authorizeResult.Succeeded)
            {
                output.SuppressOutput();
            }
            
			if (output.Attributes.TryGetAttribute("asp-authorize", out TagHelperAttribute attribute))
				output.Attributes.Remove(attribute);
        }
    }
}
