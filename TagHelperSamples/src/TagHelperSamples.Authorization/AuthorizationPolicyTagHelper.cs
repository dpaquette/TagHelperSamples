using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace TagHelperSamples.Authorization
{
    [HtmlTargetElement(Attributes = "asp-policy")]
    public class AuthorizationPolicyTagHelper : TagHelper
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationPolicyTagHelper(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }

        [HtmlAttributeName("asp-policy")]
        public string Policy { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!(await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, Policy)))
            {
                output.SuppressOutput();
            }
        }
    }
}
