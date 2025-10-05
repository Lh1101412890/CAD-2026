using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace CAD插件2026.Commands
{
    // 读取“模型空间”块表记录
    public class Cmd2
    {
        [CommandMethod("Cmd2")]
        public static void Command()
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;

            // 读取和写入数据都需要开启事务
            using Transaction transaction = database.TransactionManager.StartTransaction();
            BlockTable blockTable = transaction.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;
            BlockTableRecord modelSpace = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;

            // 开启事务后立即加一句提交，避免bug
            // 只读取数据可以不提交
            transaction.Commit();
        }
    }
}