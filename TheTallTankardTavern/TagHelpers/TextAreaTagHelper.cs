using Microsoft.AspNetCore.Razor.TagHelpers;
using TheTallTankardTavern.Helpers;

namespace TheTallTankardTavern.TagHelpers
{
	[HtmlTargetElement("textarea")]
	public class TextAreaTagHelper : TagHelper
	{
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.Attributes.AppendToAttribute("class", "form-control");
			output.Attributes.SetAttribute("resize", "none");
			output.Attributes.SetAttribute("background", "transparent");
			base.Process(context, output);
		}
	}
}
