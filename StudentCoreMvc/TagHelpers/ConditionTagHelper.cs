using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StudentCoreMvc.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement(Attributes = nameof(Condition))]
    public class ConditionTagHelper : TagHelper
    {
        public bool Condition { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Condition)
            {
                output.SuppressOutput();
    
            }
        }
    }
}
