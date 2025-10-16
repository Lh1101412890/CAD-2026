using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

using CAD插件2026.Extension;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class CreatLine
    {
        private static Point3d start = default;
        private static Point3d last = default;

        [CommandMethod(nameof(CreatLine))]
        public static void Command()
        {
            Document document = CADApp.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            PromptPointOptions options1 = new("\n指定第一个点");
            if (last == default)
            {
                options1.AllowNone = false;
            }
            else
            {
                options1.AllowNone = true;
            }
            PromptPointResult result1 = editor.GetPoint(options1);
            if (result1.Status != PromptStatus.OK && result1.Status != PromptStatus.None)
            {
                return;
            }
            if (result1.Status == PromptStatus.None)
            {
                start = last;
            }
            start = result1.Value;

            JigPromptPointOptions jigPromptPoint1 = new("\n指定下一点或[放弃(U)]：")
            {
                AppendKeywordsToMessage = false,
            };
            jigPromptPoint1.Keywords.Add("U");
            JigPromptPointOptions jigPromptPoint2 = new("\n指定下一点或[闭合(C)/放弃(U)]：")
            {
                AppendKeywordsToMessage = false,
            };
            jigPromptPoint2.Keywords.Add("C");
            jigPromptPoint2.Keywords.Add("U");
            int i = 2;
            Point3d currentStart = start;
            while (true)
            {
                LineJig lineJig;
                if (i < 4)
                {
                    lineJig = new LineJig(currentStart, jigPromptPoint1);
                }
                else
                {
                    lineJig = new LineJig(currentStart, jigPromptPoint2);
                }
                PromptResult promptResult = editor.Drag(lineJig);
                if (promptResult.Status != PromptStatus.OK && promptResult.Status != PromptStatus.Keyword)
                {
                    return;
                }
                if (promptResult.StringResult == "U")
                {
                    document.SendStringToExecute("\x03", true, false, true);
                    return;
                }
                if (promptResult.StringResult == "C")
                {
                    Line lastLine = new(currentStart, start);
                    document.Drawing(lastLine);
                    lastLine.Dispose();
                    last = start;
                    return;
                }
                Line line = new(currentStart, lineJig.end);
                document.Drawing(line);
                line.Dispose();
                last = lineJig.end;
                currentStart = lineJig.end;
                i++;
            }
        }
    }

    public class LineJig : EntityJig
    {
        private readonly JigPromptPointOptions options;
        public Point3d end;
        public LineJig(Point3d start, JigPromptPointOptions options) : base(new Line())
        {
            (this.Entity as Line).StartPoint = start;
            (this.Entity as Line).EndPoint = start;
            this.options = options;
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            PromptPointResult promptPointResult = prompts.AcquirePoint(options);
            if (promptPointResult.Status != PromptStatus.OK)
            {
                return SamplerStatus.NoChange;
            }
           (this.Entity as Line).EndPoint = promptPointResult.Value;
            end = promptPointResult.Value;
            return SamplerStatus.OK;
        }

        protected override bool Update()
        {
            return true;
        }
    }
}