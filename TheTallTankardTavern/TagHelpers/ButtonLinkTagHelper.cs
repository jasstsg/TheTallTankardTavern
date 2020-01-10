using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TheTallTankardTavern.Helpers;

namespace TheTallTankardTavern.TagHelpers
{
	[HtmlTargetElement("buttonlink")]
	public class ButtonLinkTagHelper : AnchorTagHelper
	{
        public string Text { get; set; } = "Button Link";
		public ButtonLinkTagHelper(IHtmlGenerator generator) : base(generator) { }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "a";
			output.Attributes.AppendToAttribute("class", "btn btn-outline-dark btn-sm");
            output.Content.SetContent(Text);

			base.Process(context, output);
		}
	}
}
