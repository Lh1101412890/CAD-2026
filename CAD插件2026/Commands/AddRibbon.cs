using System.Windows.Media.Imaging;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026.Commands
{
    public class AddRibbon
    {
        [CommandMethod(nameof(AddRibbon))]
        public static void Command()
        {
            RibbonControl ribbon = ComponentManager.Ribbon;
            if (ribbon is null)
            {
                return;
            }
            RibbonTab ribbonTab = ribbon.FindTab("MyPluginTab");
            if (ribbonTab is not null)
            {
                return;
            }
            RibbonTab tab = new RibbonTab
            {
                Title = "我的插件",
                Id = "MyPluginTab"
            };
            ribbon.Tabs.Add(tab);
            RibbonPanel panel = new RibbonPanel();
            tab.Panels.Add(panel);

            RibbonPanelSource panelSource = new RibbonPanelSource
            {
                Title = "功能区面板",
                Id = "MyPluginPanel",
            };
            panel.Source = panelSource;

            Uri uri1 = new Uri(@"C:\Users\11014\Downloads\灯泡_小.png");
            Uri uri2 = new Uri(@"C:\Users\11014\Downloads\灯泡_大.png");
            RibbonToolTip toolTip = new RibbonToolTip()
            {
                Command = "Command",
                Content = "这是一个命令",
                Image = new BitmapImage(uri1),
                Title = "命令1",
                Shortcut = "Cmd"
            };
            RibbonButton button1 = new RibbonButton
            {
                Text = "button1",
                Id = "MyPluginButton",
                ShowText = true,
                Size = RibbonItemSize.Standard,
                Orientation = System.Windows.Controls.Orientation.Vertical,
                Image = new BitmapImage(uri1),
                LargeImage = new BitmapImage(uri2),
                ShowImage = true,
                CommandHandler = new MyCommand(),
                CommandParameter = "pa",
                ToolTip = toolTip
            };
            RibbonButton button2 = new RibbonButton
            {
                Text = "button2",
                Id = "MyPluginButton",
                ShowText = true,
                Size = RibbonItemSize.Large,
                Orientation = System.Windows.Controls.Orientation.Vertical,
                Image = new BitmapImage(uri1),
                LargeImage = new BitmapImage(uri2),
            };
            panelSource.Items.Add(button1);
            panelSource.Items.Add(button2);
        }
    }
}
