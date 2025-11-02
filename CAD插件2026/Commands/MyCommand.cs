using System.Windows.Input;

using Autodesk.Windows;

namespace CAD插件2026.Commands
{
    public class MyCommand : ICommand
    {
#pragma warning disable CS0067 //从不使用警告
        public event EventHandler? CanExecuteChanged;
#pragma warning disable CS0067

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            RibbonButton button = parameter as RibbonButton;
            MessageBox.Show(button.CommandParameter.ToString());
        }
    }
}
