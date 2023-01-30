using Microsoft.AspNetCore.Mvc.Razor;

namespace resgistration.App.Extensions
{
    public static class RazorExtensions
    {
        public static string DocumentFormart(this RazorPage page,int personType, string document)
        {
            return personType == 1 ? Convert.ToUInt64(document).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(document).ToString(@"00\.000\.000\/000\-00");
        }
    }
}
