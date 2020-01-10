using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TheTallTankardTavern.TagHelpers
{
	[HtmlTargetElement("label")]
	public class LabelTagHelper : TagHelper
	{
		public bool Capitalize
		{
			get;
			set;
		}

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.Attributes.SetAttribute("class", "control-label");
			if (Capitalize)
			{
				output.Content.SetContent(output.Content.GetContent().ToUpper());
			}
			base.Process(context, output);
		}
	}
}
