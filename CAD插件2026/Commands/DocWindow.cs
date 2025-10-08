using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class DocWindow
    {
        [CommandMethod(nameof(DocWindow))]
        public static void Command()
        {
            Document document = CADApp.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            //document.Window.WindowState = Window.State.Normal;
            //document.Window.DeviceIndependentSize = new System.Windows.Size(800, 600);
            //document.Window.DeviceIndependentLocation = new System.Windows.Point(200, 200);

            using (ViewTableRecord view = editor.GetCurrentView())
            {
                view.CenterPoint += new Vector2d(100, 0);
                view.Height = 400;
                view.Width = 400;
                editor.SetCurrentView(view);
            }

            CADApp.UpdateScreen();
            editor.UpdateScreen();

            editor.Regen();
        }
    }
}