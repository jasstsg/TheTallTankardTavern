using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TheTallTankardTavern.Helpers
{
	public static class TagHelperHelper
	{
		public static void AppendToAttribute(this TagHelperAttributeList AttriebuteList, string name, string value)
		{
			value += (AttriebuteList.TryGetAttribute(name, out TagHelperAttribute oldValue) ? $" {oldValue.Value}" : "");
			AttriebuteList.SetAttribute(name, value);
		}
	}
}
