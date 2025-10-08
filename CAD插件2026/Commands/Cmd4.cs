using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class Cmd4
    {
        [CommandMethod(nameof(Cmd4))]
        public static void Command()
        {
            CADApp.MainWindow.WindowState = Window.State.Normal;
            //CADApp.MainWindow.WindowState = Window.State.Maximized;
            //CADApp.MainWindow.WindowState = Window.State.Minimized;

            // 设置应用程序窗口的位置
            CADApp.MainWindow.DeviceIndependentLocation = new System.Windows.Point(100, 100);
            // 设置应用程序窗口的大小
            CADApp.MainWindow.DeviceIndependentSize = new System.Windows.Size(1400, 800);

            MessageBox.Show("改变尺寸");

            CADApp.MainWindow.Visible = false;
            MessageBox.Show("CAD隐藏了");
            CADApp.MainWindow.Visible = true;

            Size size = CADApp.MainWindow.GetSize();
            CADApp.DocumentManager.MdiActiveDocument.Editor.WriteMessage(size.ToString());
        }
    }
}