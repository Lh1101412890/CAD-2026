using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class CopyEntity
    {
        [CommandMethod(nameof(CopyEntity))]
        public static void Command()
        {
            Document document = CADApp.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            Database currentDatabase = document.Database;
            using Transaction tr1 = currentDatabase.TransactionManager.StartTransaction();
            var bt1 = tr1.GetObject(currentDatabase.BlockTableId, OpenMode.ForRead) as BlockTable;
            BlockTableRecord currentModel = tr1.GetObject(bt1[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

#if true
            // 跨文件复制对象
            ObjectIdCollection collection;
            using Database d1_DB = new(false, true);
            d1_DB.ReadDwgFile("D:\\OneDrive\\编程\\C#CAD二次开发\\Drawing1.dwg", FileOpenMode.OpenForReadAndReadShare, true, "");
            d1_DB.CloseInput(true);
            using Transaction tr2 = d1_DB.TransactionManager.StartTransaction();
            {
                var blockTable = tr2.GetObject(d1_DB.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord d1_Model = tr2.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;
                collection = [.. d1_Model];
                tr2.Commit();
            }

            IdMapping mapping = [];
            currentDatabase.WblockCloneObjects(collection, currentModel.ObjectId, mapping, DuplicateRecordCloning.Ignore, false);
            tr1.Commit();
#else
            // 当前数据库中复制对象
            List<Entity> entities = new List<Entity>();
            foreach (ObjectId item in currentModel)
            {
                Entity @object = (Entity)item.GetObject(OpenMode.ForRead);
                Entity newObj = (Entity)@object.Clone();
                entities.Add(newObj);
            }
            foreach (Entity item in entities)
            {
                currentModel.AppendEntity(item);
                tr1.AddNewlyCreatedDBObject(item, true);
            }
            tr1.Commit();
#endif
        }
    }
}