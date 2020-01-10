using Microsoft.AspNetCore.Razor.TagHelpers;
using TheTallTankardTavern.Helpers;

namespace TheTallTankardTavern.TagHelpers
{
	[HtmlTargetElement("button")]
	public class ButtonTagHelper : TagHelper
	{
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "input";
			output.Attributes.SetAttribute("type", "button");
			output.Attributes.AppendToAttribute("class", "btn btn-outline-dark btn-sm");
			base.Process(context, output);
		}
	}
}
