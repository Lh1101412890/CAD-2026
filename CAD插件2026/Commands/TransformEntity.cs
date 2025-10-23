using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class TransformEntity
    {
        [CommandMethod(nameof(TransformEntity))]
        public static void Command()
        {
            Document document = CADApp.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            Database database = document.Database;

            PromptSelectionResult result = editor.GetSelection();
            if (result.Status != PromptStatus.OK)
            {
                editor.WriteMessage("\n未选择任何对象。");
                return;
            }
            using Transaction tr = database.TransactionManager.StartTransaction();
            var bt1 = tr.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;
            BlockTableRecord model = tr.GetObject(bt1[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

            // 镜像
            Plane plane = new(Point3d.Origin, Vector3d.XAxis);
            Matrix3d mirroring1 = Matrix3d.Mirroring(plane);

            Line3d line = new(Point3d.Origin, new Vector3d(1, 1, 1));
            Line3d _ = new(Point3d.Origin, new Point3d(10, 10, 0));
            Matrix3d mirroring2 = Matrix3d.Mirroring(line);

            Matrix3d mirroring3 = Matrix3d.Mirroring(Point3d.Origin);

            // 旋转
            double angle = (45 / 360.0) * 2 * Math.PI;
            Matrix3d rotation = Matrix3d.Rotation(angle, Vector3d.ZAxis, Point3d.Origin);

            // 缩放
            Matrix3d scale = Matrix3d.Scaling(5, Point3d.Origin);

            // 移动
            Matrix3d move = Matrix3d.Displacement(new Vector3d(100, 0, 0));

            foreach (ObjectId item in result.Value.GetObjectIds())
            {
                Entity entity = (Entity)item.GetObject(OpenMode.ForWrite);
                Entity newEnt = entity.Clone() as Entity;
                newEnt.TransformBy(mirroring3);
                newEnt.GetTransformedCopy(mirroring3);
                model.AppendEntity(newEnt);
                tr.AddNewlyCreatedDBObject(newEnt, true);
            }

            tr.Commit();
        }
    }
}