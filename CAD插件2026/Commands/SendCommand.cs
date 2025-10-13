using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class SendCommand
    {
        [CommandMethod(nameof(SendCommand))]
        public static void Command()
        {
            Document document = CADApp.DocumentManager.MdiActiveDocument;
            document.SendStringToExecute("\x03", true, false, true);
        }
    }
}