using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.TagHelpers;

namespace TagHelperSamples.Markdown.Tests.Helpers
{
    public class MarkdownHelperToTheTagHelpers
    {
        private readonly List<IReadOnlyTagHelperAttribute> _inputAttributes;
        private readonly Dictionary<object, object> _items = new Dictionary<object, object>();
        private readonly TagHelperAttributeList _outputAttributes;

        public MarkdownHelperToTheTagHelpers()
        {
            _inputAttributes = new List<IReadOnlyTagHelperAttribute>();
            _outputAttributes = new TagHelperAttributeList();
        }

        private Func<bool, Task<TagHelperContent>> GetChildContent(string childContent)
        {
            var content = new DefaultTagHelperContent();
            var tagHelperContent = content.SetContent(childContent);
            return b => Task.FromResult(tagHelperContent);
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