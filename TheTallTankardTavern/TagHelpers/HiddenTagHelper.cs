using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TheTallTankardTavern.TagHelpers
{
	[HtmlTargetElement("hidden")]
	public class MetaTagHelper : InputTagHelper
	{
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "input";
			output.Attributes.SetAttribute("type", "hidden");
			output.Attributes.SetAttribute("class", "metadata");
			base.Process(context, output);
		}
	}
}
