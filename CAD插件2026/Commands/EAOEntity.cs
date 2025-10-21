using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class EAOEntity
    {
        [CommandMethod(nameof(EAOEntity))]
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
            BlockTableRecord currentModel = tr.GetObject(bt1[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

            DBObjectCollection collection = [];
            foreach (ObjectId item in result.Value.GetObjectIds())
            {
                Entity entity = (Entity)item.GetObject(OpenMode.ForWrite);
#if true
                // 分解
                entity.Explode(collection);
                entity.Erase();
#else
                // 偏移，曲线方向（从起点到终点）的左侧为正
                if (entity is Curve curve)
                {
                    DBObjectCollection curves = curve.GetOffsetCurves(5);
                    foreach (DBObject obj in curves)
                    {
                        currentModel.AppendEntity(obj as Entity);
                        tr.AddNewlyCreatedDBObject(obj, true);
                    }
                }
#endif
            }
            foreach (DBObject obj in collection)
            {
                currentModel.AppendEntity(obj as Entity);
                tr.AddNewlyCreatedDBObject(obj, true);
            }
            tr.Commit();
        }
    }
}