using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class CloseDoc
    {
        [CommandMethod(nameof(CloseDoc), CommandFlags.Session)]
        public static void Command()
        {
            //foreach (Document item in CADApp.DocumentManager)
            //{
            //    item.CloseAndSave(item.Name);
            //    item.CloseAndDiscard();
            //}

            // 2004: DwgVersion.AC1800
            // 2007: DwgVersion.AC1021
            // 2010: DwgVersion.AC1024
            // 2013: DwgVersion.AC1027
            // 2018: DwgVersion.AC1032
            Document document = CADApp.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            database.SaveAs("newName", DwgVersion.Current);
        }
    }
}
