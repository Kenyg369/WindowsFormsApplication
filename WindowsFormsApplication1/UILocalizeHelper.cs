using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public static class UILocalizeHelper
    {
        public static void SetEN()
        {
            UILocalize.InfoTitle = "Info";
            UILocalize.SuccessTitle = "Success";
            UILocalize.WarningTitle = "Warning";
            UILocalize.ErrorTitle = "Error";
            UILocalize.AskTitle = "Query";
            UILocalize.InputTitle = "Input";
            UILocalize.CloseAll = "Close all";
            UILocalize.OK = "OK";
            UILocalize.Cancel = "Cancel";
            UILocalize.GridNoData = "[ No data ]";
            UILocalize.GridDataLoading = "Data loading ......";
            UILocalize.GridDataSourceException = "The data source must be DataTable or List";
        }
    }
}
