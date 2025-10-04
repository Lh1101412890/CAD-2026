using Autodesk.AutoCAD.Runtime;

namespace CAD插件2026
{
    public class Cmd1
    {
        // 在CAD中执行此命令的名称为“Cmd1”
        [CommandMethod("Cmd1")]
        public static void Hello()
        {
            MessageBox.Show("Hello,LighntingCAD!");
        }
    }
}
