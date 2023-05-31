using Microsoft.AspNetCore.Razor.TagHelpers;

namespace resgistration.App.Extensions
{
    public class EmailTagHelper : TagHelper
    {

        public string EamilDomain { get; set; } = "testParameterDomain.com";
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
          output.TagName= "a";
          var content = await output.GetChildContentAsync();
          var target = content.GetContent() + "@" + EamilDomain;
          output.Attributes.SetAttribute("href", "mailto" + target);
          output.Content.SetContent(target);

        }
    }
}
