using Microsoft.AspNetCore.Razor.TagHelpers;
using NuGet.Protocol;

namespace resgistration.App.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-claim-name")]
    [HtmlTargetElement("*", Attributes = "supress-by-claim-value")]
    public class HideElementByClaimTagHelper : TagHelper
    {

        private readonly IHttpContextAccessor _contextAccessor;

        public HideElementByClaimTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor= httpContextAccessor;
        }


        [HtmlAttributeName("supress-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("supress-by-claim-value")]
        public string IdentityClaimValue { get; set;}


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if(context == null)
                throw new ArgumentNullException(nameof(context));
            if(output == null)
                throw new ArgumentNullException(nameof(output));

            var hasAccess = CustomAuthorization.UserValidateClaim(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (hasAccess) return;

            output.SuppressOutput();
            
        }
    }



    [HtmlTargetElement("a", Attributes = "disable-by-claim-name")]
    [HtmlTargetElement("a", Attributes = "disable-by-claim-value")]
    public class DisableLinkByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public DisableLinkByClaimTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        [HtmlAttributeName("disable-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("disable-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var hasAccess = CustomAuthorization.UserValidateClaim(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (hasAccess) return;


            output.Attributes.RemoveAll("href");
            output.Attributes.Add(new TagHelperAttribute("style", "cursor: not-allowed"));
            output.Attributes.Add(new TagHelperAttribute("title", "no permission"));

        }
    }

        [HtmlTargetElement("*", Attributes = "supress-by-action")]
        public class HideElementByActionTagHelper : TagHelper
        {
            private readonly IHttpContextAccessor _contextAccessor;

            public HideElementByActionTagHelper(IHttpContextAccessor contextAccessor)
            {
                _contextAccessor = contextAccessor;
            }

            [HtmlAttributeName("supress-by-action")]
            public string ActionName { get; set; }

            public override void Process(TagHelperContext context, TagHelperOutput output)
            {
                if (context == null)
                    throw new ArgumentNullException(nameof(context));
                if (output == null)
                    throw new ArgumentNullException(nameof(output));

                var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString();

                if (ActionName.Contains(action)) return;

                output.SuppressOutput();
            }
        }



    }
     

   
