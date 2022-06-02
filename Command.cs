using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using E38EDM.TTD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBIM
{

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class MyCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            FrmListElements form = new FrmListElements(uiapp, doc);
            form.ShowDialog();

            return Result.Succeeded;
        }
    }
}
