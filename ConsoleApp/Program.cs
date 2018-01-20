using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orienteering;
using System.Windows.Input;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleView view = new ConsoleView(ConsoleView.DEFAULT_MAP_OFFSET);
            SuperController sc = new SuperController(view);
            EndGameEventArgs tmp = new EndGameEventArgs() {StartNew = true}; 
            do
            {
                
                sc.ProcessNewRound(null, ref tmp); 
                //sc.CreateNewGame();
                //sc.PlayTheGame();
            }
            while (sc.Active /*view.CurrentGame != null*/);
            //Console.ReadLine();
        }
    }
}
