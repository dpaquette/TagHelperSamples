using Microsoft.AspNetCore.Html;

namespace TagHelperSamples.Bootstrap
{
    public class PanelContext
    {
        public IHtmlContent Title { get; set; }
        public IHtmlContent Body { get; set; }
        public IHtmlContent Footer { get; set; }
    }
}
