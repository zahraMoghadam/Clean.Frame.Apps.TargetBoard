using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Core.TagHelper
{
    public class DataTableInfo
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public bool LoadDefaultSetting { get; set; }
        public bool LoadAjaxData { get; set; }
        public string ManagmentComponent { get; set; }
        public string ExtraScripts { get; set; }
        public Type ModelType { get; set; }
        public dynamic ModelData { get; set; }
        public dynamic FilterData { get; set; }
        public object Key { get; set; }
        public string HomePage { get; set; }
        public string GetDataUrl { get; set; }
        public bool OperationColumns { get; set; }
        public bool HasManagmentForm { get; set; }
        public bool HasExportExcel { get; set; }
    }
}
