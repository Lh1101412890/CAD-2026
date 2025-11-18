using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public static class NewLayer
    {
        [CommandMethod(nameof(NewLayer))]
        public static void Command()
        {
            Document doc = CADApp.DocumentManager.MdiActiveDocument;
            Database database = doc.Database;
            using Transaction transaction = database.TransactionManager.StartTransaction();
            //读取线型
            LinetypeTable linetypeTable = transaction.GetObject(database.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
            if (!linetypeTable.Has("Center"))
            {
                database.LoadLineTypeFile("Center", "acad.lin");
            }
            //读取图层
            LayerTable layerTable = transaction.GetObject(database.LayerTableId, OpenMode.ForRead) as LayerTable;
            //新建图层
            if (!layerTable.Has("Lightning"))
            {
                Autodesk.AutoCAD.Colors.Color color;
                color = Autodesk.AutoCAD.Colors.Color.FromRgb(255, 255, 0); // 黄色RGB
                color = Autodesk.AutoCAD.Colors.Color.FromNames("DIC1", "DIC COLOR GUIDE(R)"); // 配色系统
                color = Autodesk.AutoCAD.Colors.Color.FromColor(System.Drawing.Color.Red); // 系统颜色
                color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.ByAci, 1);// 红色   0 ByBlock   256 ByLayer
                LayerTableRecord lightning = new()
                {
                    Name = "Lightning",
                    IsOff = false, // 图层开关
                    IsFrozen = false, // 图层冻结
                    IsLocked = false, // 图层锁定
                    IsHidden = false, // 图层隐藏
                    IsPlottable = false, // 图层可打印
                    LineWeight = LineWeight.ByLineWeightDefault, // 线宽
                    Color = color, // 颜色
                    LinetypeObjectId = linetypeTable["Center"], // 线型
                };
                layerTable.UpgradeOpen();
                layerTable.Add(lightning);
                transaction.AddNewlyCreatedDBObject(lightning, true);

                // 必须添加到数据库中后才能设置
                lightning.UpgradeOpen();
                double t = 35;
                byte b = (byte)((100 - t) / 100.0 * 255.0);
                lightning.Transparency = new Transparency(b);
                lightning.Description = "This is a lightning layer";
                lightning.Dispose();

                Line line = new()
                {
                    StartPoint = new Autodesk.AutoCAD.Geometry.Point3d(0, 0, 0),
                    EndPoint = new Autodesk.AutoCAD.Geometry.Point3d(100, 100, 0),
                    Layer = "0",
                    Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.ByAci, 3),
                    //ColorIndex = 3,
                    Linetype = "Center",
                };
                BlockTable blockTable = transaction.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord modelSpace = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                modelSpace.AppendEntity(line);
                transaction.AddNewlyCreatedDBObject(line, true);

                //database.Clayer = layerTable["0"];
                //CADApp.SetSystemVariable("CLayer", "0");
                //database.Cecolor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.ByAci, 3);
                //database.Celtype = linetypeTable["Center"];
            }
            else
            {
                DBObject dBObject = layerTable["Lightning"].GetObject(OpenMode.ForWrite);
                dBObject.Erase();
                dBObject.Dispose();
            }
            layerTable.Dispose();
            transaction.Commit();
        }
    }
}