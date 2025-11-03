using System.Windows.Media.Imaging;

using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;

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
                ribbon.ActiveTab = ribbonTab;
                return;
            }
            RibbonTab tab = new()
            {
                Title = "我的插件",
                Id = "MyPluginTab",
            };
            RibbonTab tab2 = new()
            {
                Title = "我的插件2",
                Id = "MyPluginTab2",
                IsContextualTab = true,
                IsVisible = false
            };
            ribbon.Tabs.Add(tab);
            ribbon.Tabs.Add(tab2);
            RibbonPanel panel = new();
            tab.Panels.Add(panel);
            tab2.Panels.Add(panel);
            ribbon.Tabs[0].Panels.Add(panel);

            RibbonPanelSource panelSource = new()
            {
                Title = "功能区面板",
                Id = "MyPluginPanel",
            };
            panel.Source = panelSource;

            Uri uri1 = new(@"D:\OneDrive\编程\C#CAD二次开发\灯泡_小.png");
            Uri uri2 = new(@"D:\OneDrive\编程\C#CAD二次开发\灯泡_大.png");
            RibbonToolTip toolTip = new()
            {
                Command = "Command",
                Content = "这是一个命令",
                Image = new BitmapImage(uri1),
                Title = "命令1",
                Shortcut = "Cmd"
            };
            RibbonButton button1 = new()
            {
                Text = "button1",
                Id = "MyPluginButton",
                ShowText = true,
                Size = RibbonItemSize.Standard,
                Orientation = System.Windows.Controls.Orientation.Horizontal,
                Image = new BitmapImage(uri1),
                LargeImage = new BitmapImage(uri2),
                ShowImage = true,
                CommandHandler = new MyCommand(),
                CommandParameter = "pa",
                ToolTip = toolTip
            };
            RibbonButton button2 = new()
            {
                Text = "button2",
                Id = "MyPluginButton",
                ShowText = true,
                Size = RibbonItemSize.Large,
                Orientation = System.Windows.Controls.Orientation.Horizontal,
                Image = new BitmapImage(uri1),
                LargeImage = new BitmapImage(uri2),
            };

            RibbonRowPanel rowPanel = new RibbonRowPanel();
            rowPanel.Items.Add(button1);
            rowPanel.Items.Add(new RibbonRowBreak());
            rowPanel.Items.Add(button1);
            rowPanel.Items.Add(new RibbonRowBreak());
            rowPanel.Items.Add(button1);
            panelSource.Items.Add(rowPanel);
            panelSource.Items.Add(new RibbonPanelBreak());
            panelSource.Items.Add(button1);
            panelSource.Items.Add(button2);
        }
    }
}
