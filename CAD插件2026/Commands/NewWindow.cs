using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class NewWindow
    {
        static Window1 window1;
        [CommandMethod(nameof(NewWindow))]
        public static void Command()
        {
            if (window1 == null)
            {
                window1 = new();
                window1.Show();
            }
        }
    }
}