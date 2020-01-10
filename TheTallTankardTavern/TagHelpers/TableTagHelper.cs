using Microsoft.AspNetCore.Razor.TagHelpers;
using TheTallTankardTavern.Helpers;

namespace TheTallTankardTavern.TagHelpers
{
	[HtmlTargetElement("table")]
	public class TableTagHelper : TagHelper
	{
		public bool TableHover
		{
			get;
			set;
		}

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.Attributes.AppendToAttribute("class", "table table-striped table-sm" + (TableHover ? " table-hover" : ""));
			base.Process(context, output);
		}
	}
}
