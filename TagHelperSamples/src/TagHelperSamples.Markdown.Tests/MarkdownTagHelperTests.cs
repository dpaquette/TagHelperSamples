using System.Linq;
using System.Threading.Tasks;
using TagHelperSamples.Markdown.Tests.Helpers;
using Xunit;

namespace TagHelperSamples.Markdown.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class MarkdownTagHelperTests
    {
        private readonly MarkdownHelperToTheTagHelpers _helper;

        public MarkdownTagHelperTests()
        {
            _helper = new MarkdownHelperToTheTagHelpers();
        }

        /// <summary>
        /// <markdown>## Hello</markdown>
        /// Should return
        /// <h2>Hello</h2>
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task MarkdownTagWithChildContentShouldReturnRenderedMarkdown()
        {
            // arrange
            var sut = new MarkdownTagHelper();
            var hasMarkdownAttribute = false;
            var tagName = "markdown";

            var context = _helper.CreateContext("## Hello", hasMarkdownAttribute);
            var output = _helper.CreateOutput(tagName, hasMarkdownAttribute);

            // act
            await sut.ProcessAsync(context, output);

            //assert
            Assert.Equal(null, output.TagName);
            Assert.StartsWith("<h2>Hello</h2>", output.Content.GetContent());
        }


        [Fact]
        public async Task ArticleTagWithChildContentWithMarkdownAttributeShouldReturnRenderedMarkdown()
        {
            // arrange
            var sut = new MarkdownTagHelper();
            var hasMarkdownAttribute = true;
            var tagName = "article";

            var context = _helper.CreateContext("# Hello", hasMarkdownAttribute);
            var output = _helper.CreateOutput(tagName, hasMarkdownAttribute);

            // act
            await sut.ProcessAsync(context, output);
            
            //assert
            Assert.Equal(tagName, output.TagName);
            Assert.StartsWith("<h1>Hello</h1>", output.Content.GetContent());
        }
        
    }
}
