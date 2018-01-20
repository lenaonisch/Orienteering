using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Orienteering;
namespace WFApp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            frmMain view = new frmMain();
            Game game = new GameOrienteering(view);
            view.CurrentGame = (Game)game;
            
            Application.Run(view);
        }
    }
}
