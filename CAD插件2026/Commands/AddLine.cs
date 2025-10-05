using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

using CAD插件2026.Extension;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class AddLine
    {
        // 在模型空间绘制一条直线
        [CommandMethod("AddLine")]
        public static void AddALine()
        {
            Line line = new()
            {
                StartPoint = new Point3d(0, 0, 0),
                EndPoint = new Point3d(100, 100, 0)
            };

            Document document = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;

            document.Drawing(line);
        }
    }
}