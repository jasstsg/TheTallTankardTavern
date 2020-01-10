using Microsoft.AspNetCore.Razor.TagHelpers;
using TheTallTankardTavern.Helpers;

namespace TheTallTankardTavern.TagHelpers
{
	[HtmlTargetElement("textbox")]
	public class TextBoxTagHelper : InputTagHelper
	{
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "input";
			output.Attributes.SetAttribute("type", "text");
			output.Attributes.AppendToAttribute("class", "form-control");
			base.Process(context, output);
		}
	}
}
