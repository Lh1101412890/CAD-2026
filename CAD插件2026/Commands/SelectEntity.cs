using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class SelectEntity
    {
        [CommandMethod(nameof(SelectEntity))]
        public static void Command()
        {
            Document document = CADApp.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            Database database = document.Database;

            PromptSelectionOptions options = new PromptSelectionOptions()
            {
                MessageForAdding = "请选择直线"
            };
            TypedValue[] values = new TypedValue[]
            {
                new TypedValue((int)DxfCode.Operator,"<or"),
                new TypedValue((int)DxfCode.Operator,"<and"),
                new TypedValue((int)DxfCode.Start,"line"),
                new TypedValue((int)DxfCode.Color,"1"),
                //new TypedValue((int)DxfCode.LayerName,"Li"),
                new TypedValue((int)DxfCode.Operator,"and>"),
                new TypedValue((int)DxfCode.Start,"lwpolyline"),

                new TypedValue((int)DxfCode.Operator,"<and"),
                new TypedValue((int)DxfCode.Start,"text"),
                new TypedValue((int)DxfCode.Text,"`*Li*"),
                new TypedValue((int)DxfCode.Operator,"and>"),

                new TypedValue((int)DxfCode.Operator,"or>"),
            };
            SelectionFilter filter = new SelectionFilter(values);
            PromptSelectionResult result = editor.GetSelection(options, filter);
            if (result.Status != PromptStatus.OK)
            {
                return;
            }
            foreach (SelectedObject item in result.Value)
            {
                using (Transaction tr = database.TransactionManager.StartTransaction())
                {
                    DBObject dBObject = item.ObjectId.GetObject(OpenMode.ForRead);
                    editor.WriteMessage("\n" + dBObject.GetType().ToString());
                    tr.Commit();
                }
            }
            editor.SelectAll();
            
        }
    }
}