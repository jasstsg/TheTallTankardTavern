using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TheTallTankardTavern.TagHelpers
{
	public abstract class InputTagHelper : TagHelper
	{
        public ModelExpression AspFor { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
            output.Attributes.SetAttribute("name", AspFor.Name);
            output.Attributes.SetAttribute("id", AspFor.Name);
            output.Attributes.SetAttribute("value", AspFor.Model);

            base.Process(context, output);
		}
	}
}
