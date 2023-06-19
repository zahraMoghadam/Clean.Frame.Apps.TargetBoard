using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Core.TagHelper
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class DataTableAttribute : Attribute
    {
        public string PersianName { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public bool Sortable { get; set; }
        public string Width { get; set; }

        public DataTableAttribute(string title)
        {
            Title = title;
        }
    }
}
