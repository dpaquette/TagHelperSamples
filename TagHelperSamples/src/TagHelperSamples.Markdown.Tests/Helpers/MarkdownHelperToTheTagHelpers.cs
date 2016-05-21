using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace TagHelperSamples.Markdown.Tests.Helpers
{
    public class MarkdownHelperToTheTagHelpers
    {
        private readonly TagHelperAttributeList _inputAttributes;
        private readonly Dictionary<object, object> _items = new Dictionary<object, object>();
        private readonly TagHelperAttributeList _outputAttributes;

        public MarkdownHelperToTheTagHelpers()
        {
            _inputAttributes = new TagHelperAttributeList();
            _outputAttributes = new TagHelperAttributeList();
        }

        private Func<bool, HtmlEncoder, Task<TagHelperContent>> GetChildContent(string childContent)
        {
            var content = new DefaultTagHelperContent();
            var tagHelperContent = content.SetContent(childContent);
            return (b, encoder) => Task.FromResult(tagHelperContent);
        }

        public TagHelperContext CreateContext(bool hasMarkdownAttribute)
        {
            
            if (hasMarkdownAttribute)
                _inputAttributes.Add(new TagHelperAttribute("markdown", ""));
            var context = new TagHelperContext(_inputAttributes, _items, Guid.NewGuid().ToString());
            return context;
        }

        public TagHelperOutput CreateOutput(string tagName, bool hasMarkdownAttribute, string tagContent)
        {
            var childContent = GetChildContent(tagContent);
            if (hasMarkdownAttribute)
                _outputAttributes.Add(new TagHelperAttribute("markdown", ""));
                       
            return new TagHelperOutput(tagName, 
                _outputAttributes, GetChildContent(tagContent));
        }
    }
}