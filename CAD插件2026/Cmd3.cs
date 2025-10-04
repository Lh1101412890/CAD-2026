using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Runtime;

using CADApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CAD插件2026
{
    public class Cmd3
    {
        [CommandMethod("Cmd3")]
        public static void Command()
        {
            AcadPreferences preferences = (AcadPreferences)CADApp.Preferences;
            uint now = preferences.Display.GraphicsWinModelBackgrndColor;
            
            //16进制的RGB颜色值，前两位为00，然后是R、G、B值
            if (now == 0x00000000)
            {
                CADApp.SetSystemVariable("COLORTHEME", 1);
                preferences.Display.GraphicsWinModelBackgrndColor = 0x00ffffff; //白色
                preferences.Display.GraphicsWinLayoutBackgrndColor = 0x00ffffff; //白色
            }
            else
            {
                CADApp.SetSystemVariable("COLORTHEME", 0);
                preferences.Display.GraphicsWinModelBackgrndColor = 0x00000000; //黑色
                preferences.Display.GraphicsWinLayoutBackgrndColor = 0x00000000; //黑色
            }

        }
    }
}
