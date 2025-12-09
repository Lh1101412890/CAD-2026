using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class MoveJig : DrawJig
    {
        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            PromptPointResult result = prompts.AcquirePoint("新位置");
            if (result.Status != PromptStatus.OK)
            {
                return SamplerStatus.Cancel;
            }
            if (!target.IsEqualTo(result.Value, Tolerance.Global))
            {
                target = result.Value;
                return SamplerStatus.OK;
            }
            return SamplerStatus.NoChange;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            Matrix3d mat = Matrix3d.Displacement(start.GetVectorTo(target));
            WorldGeometry geometry = draw.Geometry;
            geometry.PushModelTransform(mat);
            foreach (Entity entity in entitie)
            {
                geometry.Draw(entity);
            }
            geometry.PopModelTransform();
            return true;
        }

        public MoveJig(Point3d basePt)
        {
            start = basePt;
            target = start;
        }

        public void AddEntity(Entity ent) => entitie.Add(ent);

        private Point3d start, target;

        public void Commit()
        {
            var mat = Matrix3d.Displacement(start.GetVectorTo(target));
            foreach (var e in entitie)
            {
                e.TransformBy(mat);
            }
        }

        private readonly List<Entity> entitie = [];
    }

    public class MoveEntity
    {
        [CommandMethod(nameof(MoveEntity))]
        public static void MoveJigCmd()
        {
            var document = CADApp.DocumentManager.MdiActiveDocument;
            var editor = document.Editor;
            var database = document.Database;

            var sel = editor.GetSelection();
            if (sel.Status != PromptStatus.OK) return;

            var p1 = editor.GetPoint("选择基准点");
            if (p1.Status != PromptStatus.OK) return;

            using var tr = database.TransactionManager.StartTransaction();
            var jig = new MoveJig(p1.Value);

            foreach (var id in sel.Value.GetObjectIds())
            {
                var ent = (Entity)tr.GetObject(id, OpenMode.ForWrite);
                jig.AddEntity(ent);
            }

            var pr = editor.Drag(jig);
            if (pr.Status != PromptStatus.OK)
            {
                tr.Abort();
            }
            jig.Commit();
            tr.Commit();
        }
    }
}