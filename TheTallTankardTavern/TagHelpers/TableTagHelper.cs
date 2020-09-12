using Microsoft.AspNetCore.Razor.TagHelpers;
using TheTallTankardTavern.Helpers;

namespace TheTallTankardTavern.TagHelpers
{
	[HtmlTargetElement("table")]
	public class TableTagHelper : TagHelper
	{
		public bool TableHover { get; set; } = false;
		public bool TableStriped { get; set; } = true;

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.Attributes.AppendToAttribute("class", 
				$"table table-sm {(TableStriped ? "table-striped" : "")} {(TableHover ? " table-hover" : "")}");
			base.Process(context, output);
		}
	}
}
