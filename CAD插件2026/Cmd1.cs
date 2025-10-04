using Autodesk.AutoCAD.Runtime;

namespace CAD插件2026
{
    public class Cmd1
    {
        [CommandMethod("Cmd1")]
        public static void Hello()
        {
            MessageBox.Show("Hello,LighntingCAD!");
        }
    }
}