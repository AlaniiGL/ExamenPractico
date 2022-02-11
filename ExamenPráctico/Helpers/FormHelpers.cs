using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ExamenPráctico.Helpers
{
    public static class FormHelpers
    {
        public const string EmailType = "email";
        public const string TextType = "text";
        public const string PasswordType = "password";
        public const string UrlType = "url";
        public const string NumberType = "number";

        public static MvcHtmlString TextInput<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string inputType = TextType)
        {

            string inputHtml = "   <div class='form-group'>" +
                               helper.LabelFor(expression, new { @class = "col-md-2 control-label" }).ToHtmlString() +
                               " <div class='col-md-4'>" +
                               helper.TextBoxFor(expression, new { @class = "form-control", type = inputType, style = "max-width: 100%;" })
                                   .ToHtmlString() +
                               helper.ValidationMessageFor(expression).ToHtmlString() +
                               " </div>" +
                               "</div>";
            return new MvcHtmlString(inputHtml);
        }
        public static MvcHtmlString SelectInput<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, SelectList selectList)
        {

            string inputHtml = "   <div class='form-group'>" +
                               helper.LabelFor(expression, new { @class = "col-md-2 control-label" }).ToHtmlString() +
                               " <div class='col-md-4'>" +
                                helper.DropDownListFor(expression, selectList, "Selecciona", new { @class = "form-control", type = "Select", style = "max-width: 100%;" }).ToHtmlString() +
                               helper.ValidationMessageFor(expression).ToHtmlString() +
                               " </div>" +
                               "</div>";
            return new MvcHtmlString(inputHtml);
        }

        public static IHtmlString DateTimeInput<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var fullHtmlFieldName = helper
                .ViewContext
                .ViewData
                .TemplateInfo
                .GetFullHtmlFieldName(name);
            Func<TModel, TValue> method = expression.Compile();
            TValue value = method(helper.ViewData.Model);
            string input = (value.ToString()) != new DateTime().ToString() ?
                helper.TextBoxFor(expression, new { @class = "form-control", type = TextType, @readonly = "readonly", style = "max-width: 100%;" }).ToHtmlString()
                :
                "<input class='form-control' id='" + fullHtmlFieldName + "' name='" + fullHtmlFieldName + "' style='max-width: 100%;' type='text' value='' readonly>";

            string htmlString =
                string.Format(
                    "   <div class='form-group'>" +
                    helper.LabelFor(expression, new { @class = "col-md-2 control-label" }).ToHtmlString() +
                    " <div class='col-md-4'>" +
                    " <div id='{0}Picker' class='input-group date form_datetime' style='max-width: 100%;'>" +
                    input
                    + "<span class='input-group-addon'><span class='glyphicon glyphicon-remove'></span></span>"
                    + "<span class='input-group-addon'><span class='glyphicon glyphicon-time'></span></span>"
                    + "</div>"
                    + helper.ValidationMessageFor(expression).ToHtmlString() +
                    " </div>" +
                    "</div>",
                    fullHtmlFieldName
                );
            return new HtmlString(htmlString);
        }
    }
}