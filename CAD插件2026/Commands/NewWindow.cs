using Autodesk.AutoCAD.Runtime;

namespace CAD插件2026.Commands
{
    public class NewWindow
    {
        private static Window1 window1;
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