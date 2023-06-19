using Clean.Frame.Apps.TargetBoard.Core.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace Clean.Frame.Apps.TargetBoard.Core.TagHelper
{
    public class DatatableTagHelper
    {
        [HtmlTargetElement("data_table")]
        public class DatatablesTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
        {
            public DatatablesTagHelper()
            {
                Pageing = true;
            }
            [HtmlAttributeName("id")]
            public string Id { get; set; }

            [HtmlAttributeName("class")]
            public string css { get; set; }
            [HtmlAttributeName("data-title")]
            public string Title { get; set; }
            [HtmlAttributeName("data-paging")]
            public bool Pageing { get; set; }
            public Type Model { get; set; }

            [HtmlAttributeName("data-ajax")]
            public string Url { get; set; }
            public List<string> ReqularColumns { get; set; }
            public bool OperationsColumns { get; set; }

            [ViewContext]
            public ViewContext ViewContext { get; set; }

           
            public override void Process(TagHelperContext context, TagHelperOutput output)
            {

                output.TagName = "DataTable";
                output.Attributes.SetAttribute("class", "table table-striped table-bordered " + " " + css + " ");

                if (ReqularColumns == null) ReqularColumns = new List<string>();

                TagBuilder head = new TagBuilder("thead");
                TagBuilder headerRow = new TagBuilder("tr");
                StringBuilder columns = new StringBuilder();

                List<PropertyInfo> properties = Model.GetProperties()
                    .Where(x => x.GetCustomAttributes(typeof(DataTableAttribute), true)
                                .OfType<DataTableAttribute>().Any(z => (ReqularColumns != null && ReqularColumns.Contains(x.Name))))
                    .ToList();

                foreach (PropertyInfo propertyInfo in properties)
                {
                    TagBuilder headerCol = new TagBuilder("th");
                    DataTableAttribute attribute = ((DataTableAttribute)(propertyInfo.GetCustomAttributes(typeof(DataTableAttribute), true).First()));

                    string columnHeader = attribute.PersianName;

                    columns.Append("{\"data\":\"" + propertyInfo.Name + "\"},");

                    headerCol.InnerHtml.Append(columnHeader);
                    headerRow.InnerHtml.AppendHtml(headerCol);
                }

                TagBuilder operationCol = new TagBuilder("th");
                string operationTitle = "";
                if (OperationsColumns)
                {
                    columns.Append("{\"data\":null}");
                }

                operationCol.InnerHtml.Append(operationTitle);
                headerRow.InnerHtml.AppendHtml(operationCol);


                output.Attributes.SetAttribute("data-columns", $"[{columns.ToString().TrimEnd(',')}]");
                head.InnerHtml.AppendHtml(headerRow);
                output.TagMode = TagMode.StartTagAndEndTag;
                output.Content.SetHtmlContent(head);
            }
        }
    }
}
