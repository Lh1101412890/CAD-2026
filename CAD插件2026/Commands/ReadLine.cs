using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class ReadLine
    {
        // 读取模型空间中的所有直线并打印其起点和终点坐标
        [CommandMethod("ReadLine")]
        public static void ReadALine()
        {
            Document document = CADApp.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            using Transaction transaction = database.TransactionManager.StartTransaction();
            BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
            BlockTableRecord modelSpace = (BlockTableRecord)transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForRead);
            foreach (ObjectId objId in modelSpace)
            {
                DBObject @object = transaction.GetObject(objId, OpenMode.ForRead);
                if (@object is Line line)
                {
                    Point3d startPoint = line.StartPoint;
                    Point3d endPoint = line.EndPoint;
                    document.Editor.WriteMessage($"\nLine from {startPoint} to {endPoint}");
                }
            }
            transaction.Commit();
        }
    }
}