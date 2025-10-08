using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class NewDoc
    {
        [CommandMethod(nameof(NewDoc))]
        public static void Command()
        {
            //CADApp.DocumentManager.MdiActiveDocument = CADApp.DocumentManager.Add("acad.dwt");

            CADApp.DocumentManager.MdiActiveDocument = CADApp.DocumentManager.Open("C:\\Users\\11014\\Desktop\\Drawing4.dwg", false);
        }
    }
}
