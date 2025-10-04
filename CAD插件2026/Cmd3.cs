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
    /// <summary>
    /// 背景颜色及主题颜色切换（黑/白）
    /// </summary>
    public class Cmd3
    {
        [CommandMethod("Cmd3")]
        public static void Command()
        {
            // 需要添加对应版本的包引用（AutoCAD.NET.Interop）
            // CAD2018 R22.0
            // CAD2019 R23.0
            // CAD2020 R23.1
            // CAD2021 R24.0
            // CAD2022 R24.1
            // CAD2023 R24.2
            // CAD2024 R24.3
            // CAD2025 R25.0
            // CAD2026 R25.1
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
