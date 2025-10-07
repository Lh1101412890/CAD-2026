using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class DeleteLine
    {
        // 删除模型空间中的所有直线
        [CommandMethod("DeleteLine")]
        public static void DeleteALine()
        {
            Document document = CADApp.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            using Transaction transaction = database.TransactionManager.StartTransaction();
            BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
            BlockTableRecord modelSpace = (BlockTableRecord)transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
            foreach (ObjectId objId in modelSpace)
            {
                DBObject @object = transaction.GetObject(objId, OpenMode.ForWrite);
                if (@object is Line line)
                {
                    line.Erase();
                }
            }
            transaction.Commit();
        }
    }
}