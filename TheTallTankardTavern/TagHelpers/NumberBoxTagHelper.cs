using Microsoft.AspNetCore.Razor.TagHelpers;
using TheTallTankardTavern.Helpers;

namespace TheTallTankardTavern.TagHelpers
{
	[HtmlTargetElement("numberbox")]
	public class NumberBoxTagHelper : InputTagHelper
	{
		public int Min
		{
			get;
			set;
		}

		public int Max
		{
			get;
			set;
		} = int.MaxValue;


		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "input";
			output.Attributes.SetAttribute("type", "number");
			output.Attributes.SetAttribute("min", Min);
			output.Attributes.SetAttribute("max", Max);
			output.Attributes.AppendToAttribute("class", "form-control");
			base.Process(context, output);
		}
	}
}
