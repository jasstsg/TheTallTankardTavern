using Microsoft.AspNetCore.Razor.TagHelpers;
using TheTallTankardTavern.Helpers;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.TagHelpers
{
	[HtmlTargetElement("submit")]
	public class SubmitTagHelper : TagHelper
	{
        public string Text { get; set; } = "Submit";
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "input";
			output.Attributes.SetAttribute("type", "submit");
			output.Attributes.AppendToAttribute("class", TAGHELPER.CSS_CLASSES.BUTTON);
            output.Attributes.SetAttribute("name", "submit");
            output.Attributes.SetAttribute("value", Text);

            base.Process(context, output);
		}
	}
}
