using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class SelectEntity
    {
        [CommandMethod(nameof(SelectEntity), CommandFlags.UsePickSet | CommandFlags.NoPaperSpace | CommandFlags.NoBlockEditor)]
        public static void Command()
        {
            Document document = CADApp.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            Database database = document.Database;

            PromptSelectionOptions options = new()
            {
                MessageForAdding = "请选择直线"
            };
            TypedValue[] values =
            [
                new((int)DxfCode.Operator,"<or"),
                new((int)DxfCode.Operator,"<and"),
                new((int)DxfCode.Start,"line"),
                new((int)DxfCode.Color,"1"),
                //new TypedValue((int)DxfCode.LayerName,"Li"),
                new((int)DxfCode.Operator,"and>"),
                new((int)DxfCode.Start,"lwpolyline"),

                new((int)DxfCode.Operator,"<and"),
                new((int)DxfCode.Start,"text"),
                new((int)DxfCode.Text,"`*Li*"),
                new((int)DxfCode.Operator,"and>"),

                new((int)DxfCode.Operator,"or>"),
            ];
            SelectionFilter filter = new(values);
            PromptSelectionResult result = editor.GetSelection(options, filter);
            if (result.Status != PromptStatus.OK)
            {
                return;
            }
            foreach (SelectedObject item in result.Value)
            {
                using Transaction tr = database.TransactionManager.StartTransaction();
                DBObject dBObject = item.ObjectId.GetObject(OpenMode.ForRead);
                editor.WriteMessage("\n" + dBObject.GetType().ToString());
                tr.Commit();
            }
            //editor.SelectAll();

        }
    }
}