using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Autodesk.Windows;

namespace CAD插件2026.Commands
{
    public class MyCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

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
