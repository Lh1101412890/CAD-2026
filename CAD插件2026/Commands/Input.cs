using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class Input
    {
        [CommandMethod(nameof(Input))]
        public static void Command()
        {
            Document document = CADApp.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            //PromptPointResult promptPointResult = editor.GetPoint("\n请输入点");
            //if (promptPointResult.Status != PromptStatus.OK)
            //{
            //    return;
            //}
            //Autodesk.AutoCAD.Geometry.Point3d value = promptPointResult.Value;
            //editor.WriteMessage("\n" + value.ToString());

            //PromptPointOptions options = new PromptPointOptions("\n请输入点")
            //{
            //    LimitsChecked = true
            //};
            //PromptPointResult promptPointResult = editor.GetPoint(options);
            //if (promptPointResult.Status != PromptStatus.OK)
            //{
            //    return;
            //}
            //editor.WriteMessage(promptPointResult.Value.ToString());

            PromptKeywordOptions options = new("请选择模式[模式1(1)/模式2(2)/取消(U)]")
            {
                AppendKeywordsToMessage = true
            };
            options.Keywords.Add("1");
            options.Keywords.Add("2");
            options.Keywords.Add("U");
            PromptResult promptResult = editor.GetKeywords(options);
            if (promptResult.Status != PromptStatus.OK)
            {
                return;
            }
            editor.WriteMessage(promptResult.StringResult);
        }
    }
}