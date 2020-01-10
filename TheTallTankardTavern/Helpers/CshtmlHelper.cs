using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq.Expressions;

namespace TheTallTankardTavern.Helpers
{
	public static class CshtmlHelper
	{
		public static IHtmlContent EnumDropdownListFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, Type enumtype)
		{
			return htmlHelper.DropDownListFor(expression, htmlHelper.GetEnumSelectList(enumtype));
		}

		public static IHtmlContent EnumDropdownListFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, Type enumtype, object htmlAttributes)
		{
			return htmlHelper.DropDownListFor(expression, htmlHelper.GetEnumSelectList(enumtype), htmlAttributes);
		}

		public static IHtmlContent EnumDropdownListFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, Type enumtype, string optionLabel)
		{
			return htmlHelper.DropDownListFor(expression, htmlHelper.GetEnumSelectList(enumtype), optionLabel);
		}

		public static IHtmlContent NumericUpDownBoxFor<TModel, TResult>(this IHtmlHelper<TModel> htmlhelper, Expression<Func<TModel, TResult>> expression, int min, int max, int defaultvalue)
		{
			return new HtmlString($"<input type='number' name='{expression.GetMemberNameFromExpression()}' min='{min}' max='{max}' value='{defaultvalue}' />");
		}

		public static string GetMemberNameFromExpression(this Expression expression)
		{
			return ((MemberExpression)expression).Member.Name;
		}
	}
}
