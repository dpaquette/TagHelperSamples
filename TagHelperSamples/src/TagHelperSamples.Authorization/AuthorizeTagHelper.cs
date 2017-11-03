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
    public class AuthorizationPolicyTagHelper : TagHelper, IAuthorizeData
    {
        private readonly IAuthorizationPolicyProvider _policyProvider;
        private readonly IPolicyEvaluator _policyEvaluator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationPolicyTagHelper(IHttpContextAccessor httpContextAccessor, IAuthorizationPolicyProvider policyProvider, IPolicyEvaluator policyEvaluator)
        {
            _httpContextAccessor = httpContextAccessor;
            _policyProvider = policyProvider;
            _policyEvaluator = policyEvaluator;
        }
        
        [HtmlAttributeName("asp-policy")]
        public string Policy { get; set; }

        [HtmlAttributeName("asp-roles")]
        public string Roles { get; set; }

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
        }
    }
}
