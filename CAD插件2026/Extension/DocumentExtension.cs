using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace CAD插件2026.Extension
{
    public static class DocumentExtension
    {
        /// <summary>
        /// 在document的模型空间绘制实体
        /// </summary>
        /// <param name="document"></param>
        /// <param name="entity"></param>
        public static void Drawing(this Document document, Entity entity)
        {
            Database database = document.Database;
            using Transaction transaction = database.TransactionManager.StartTransaction();
            BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
            BlockTableRecord modelSpace = (BlockTableRecord)transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

            // 将实体添加到模型空间
            modelSpace.AppendEntity(entity);
            // 通知事务管理器有一个新对象被创建
            transaction.AddNewlyCreatedDBObject(entity, true);
            entity.Dispose();
            transaction.Commit();
        }

        /// <summary>
        /// 在document的模型空间绘制实体列表
        /// </summary>
        /// <param name="document"></param>
        /// <param name="entities"></param>
        public static void Drawing(this Document document, List<Entity> entities)
        {
            Database database = document.Database;
            using Transaction transaction = database.TransactionManager.StartTransaction();
            BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
            BlockTableRecord modelSpace = (BlockTableRecord)transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
            foreach (var item in entities)
            {
                modelSpace.AppendEntity(item);
                transaction.AddNewlyCreatedDBObject(item, true);
                item.Dispose();
            }
            transaction.Commit();
        }

        /// <summary>
        /// 从document的模型空间删除实体
        /// </summary>
        /// <param name="document"></param>
        /// <param name="entity"></param>
        public static void Delete(this Document document, Entity entity)
        {
            Database database = document.Database;
            using Transaction transaction = database.TransactionManager.StartTransaction();

            // 必须先获取实体的写入权限
            entity.ObjectId.GetObject(OpenMode.ForWrite);
            // 删除实体
            entity.Erase();

            transaction.Commit();
        }

        /// <summary>
        /// 从document的模型空间删除实体列表
        /// </summary>
        /// <param name="document"></param>
        /// <param name="entities"></param>
        public static void Delete(this Document document, List<Entity> entities)
        {
            Database database = document.Database;
            using Transaction transaction = database.TransactionManager.StartTransaction();
            foreach (var item in entities)
            {
                item.ObjectId.GetObject(OpenMode.ForWrite);
                item.Erase();
            }
            transaction.Commit();
        }
    }
}