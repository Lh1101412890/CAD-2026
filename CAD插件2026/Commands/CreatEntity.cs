using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

using CAD插件2026.Extension;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class CreatEntity
    {
        [CommandMethod(nameof(CreatEntity))]
        public static void Command()
        {
            Document document = CADApp.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            Database database = document.Database;

            DBPoint dBPoint = new(new Point3d(1, 1, 0));
            document.Drawing(dBPoint);
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                dBPoint.ObjectId.GetObject(OpenMode.ForWrite);
                dBPoint.Position = new(50, 50, 0);
                transaction.Commit();
            }
            dBPoint.Dispose();

            Polyline polyline = new();
            Point2d point1 = new(0, 0);
            Point2d point2 = new(100, 0);
            Point2d point3 = new(100, 100);
            Point2d point4 = new(0, 100);
            polyline.AddVertexAt(0, point1, 0, 0, 0);
            polyline.AddVertexAt(1, point2, 0, 0, 0);
            polyline.AddVertexAt(2, point3, 0, 0, 0);
            polyline.AddVertexAt(3, point4, 0, 0, 0);
            polyline.Closed = true;
            polyline.ConstantWidth = 1;
            document.Drawing(polyline);
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                polyline.Id.GetObject(OpenMode.ForWrite);
                polyline.SetPointAt(0, new Point2d(50, 0));
                polyline.RemoveVertexAt(1);
                transaction.Commit();
            }
            double _1 = polyline.Length;
            double _2 = polyline.Area;

            if (polyline.Bounds.HasValue)
            {
                var max = polyline.Bounds.Value.MaxPoint;
                var min = polyline.Bounds.Value.MinPoint;
                editor.WriteMessage("max: " + max.ToString() + "，min: " + min.ToString() + "\n");
            }
            polyline.Dispose();
        }
    }
}