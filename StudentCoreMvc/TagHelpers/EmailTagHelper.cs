using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StudentCoreMvc.Model;

namespace StudentCoreMvc.TagHelpers
{
    /// <summary>
    /// 自定义TagHelper
    /// 这边还有一个异步的ProcessAsync
    /// </summary>
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    //[HtmlTargetElement("bold")]
    //[HtmlTargetElement(Attributes ="bold")]
    public class EmailTagHelper : TagHelper
    {
        public string  MailTo { get; set; }

        [HtmlAttributeName("my-style")]
        public MyStyle MyStyle { get; set; }

        //public override void Process(TagHelperContent tagHelperContent, TagHelperOutput tagHelperOutput)
        //{
        //    tagHelperOutput.Attributes.RemoveAll("bold");
        //    tagHelperOutput.PreContent.SetHtmlContent("<strong>");
        //    tagHelperOutput.PostContent.SetHtmlContent("<strong>");
        //    tagHelperOutput.Attributes.SetAttribute("style", $"color:{MyStyle.Color};font-size:{MyStyle.FontSize}");
            
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">包含了当前TagHelper中的一些信息</param>
        /// <param name="output">当前要输出的一些信息(在里面写将要生成的)</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            
            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"mailto:{MailTo}");
            output.Content.SetContent(MailTo);
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            var content = await output.GetChildContentAsync();
            var target = content.GetContent();
            output.Attributes.SetAttribute("href", $"mailto:{target}");
            output.Content.SetContent(MailTo);

        }
    }
}
