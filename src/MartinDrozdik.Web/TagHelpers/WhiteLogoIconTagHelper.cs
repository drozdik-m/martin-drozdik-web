using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MartinDrozdik.Web.TagHelpers
{
    [HtmlTargetElement("white-logo-icon", TagStructure = TagStructure.WithoutEndTag)]
    public class WhiteLogoIconTagHelper : TagHelper
    {
        const string SVG_LOGO_ICON =
                    "<path fill-rule=\"evenodd\" clip-rule=\"evenodd\" d=\"M0 387.944C0 387.944 172.027 139.5 231.627 62.4425C291.227 -14.615 396.694 11.9565 396.694 62.4425V67.7568L443.285 100.971L394.032 128.871C307.504 334.801 115.813 394.587 0 387.944ZM51.9163 360.044C51.9163 360.044 218.315 123.557 258.25 71.7425C300.848 26.5709 371.401 37.1995 370.07 67.7541V82.3711L394.032 98.3141L372.733 111.6C296.855 297.601 138.444 352.072 51.9163 360.044Z\" fill=\"white\"/>" +
                    "<path d=\"M129.126 162.086C57.2414 148.8 26.6241 96.9856 26.6241 96.9856L5.32507 107.614C5.32507 107.614 39.936 170.057 125.132 186L129.126 162.086Z\" fill=\"white\"/>" +
                    "<path d=\"M157.08 127.544C77.209 106.287 54.5789 43.8435 54.5789 43.8435L29.2863 53.1435C29.2863 53.1435 54.5789 127.544 150.424 154.115L157.08 127.544Z\" fill=\"white\"/>" +
                    "<path d=\"M182.373 87.6861C114.482 69.086 91.8521 0 91.8521 0L63.8971 7.97146C63.8971 7.97146 86.5274 91.6718 174.386 115.586L182.373 87.6861Z\" fill=\"white\"/>" +
                    "<path d=\"M326.176 105.154C334.644 105.154 341.509 98.8297 341.509 91.0281C341.509 83.2265 334.644 76.9021 326.176 76.9021C317.707 76.9021 310.842 83.2265 310.842 91.0281C310.842 98.8297 317.707 105.154 326.176 105.154Z\" fill=\"white\"/>";



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "svg";
            output.Attributes.Add("xmlns", "http://www.w3.org/2000/svg");
            output.Attributes.Add("width", "444");
            output.Attributes.Add("height", "389");
            output.Attributes.Add("fill", "none");
            output.Attributes.Add("viewBox", "0 0 444 389");
            output.Attributes.Add("class", "whiteLogoIcon");
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(SVG_LOGO_ICON);

        }
    }
}
