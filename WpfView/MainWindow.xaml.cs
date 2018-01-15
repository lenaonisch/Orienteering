using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Orienteering;
namespace WpfView
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void PrintMap(MapContainer map)
        {
            throw new NotImplementedException();
        }

        public void PrintMap(MapContainer map, Coord canvasOffset)
        {
            throw new NotImplementedException();
        }

        public void ReprintChangedCells(params Cell[] cells)
        {
            throw new NotImplementedException();
        }

        public void PrintMessage(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void PrintError(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void PrintWarning(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public bool GetYesNoAnswer(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void ShowHint(string hint, params object[] args)
        {
            throw new NotImplementedException();
        }

        public new IGame Owner
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
            }
        }

        public Key GetUserInput()
        {
            throw new NotImplementedException();
        }

        public event ChangePositionDelegate MakeMove
        {
            add
            {
                _move += value;
            }
            remove
            {
                _move -= value;
            }
        }

        IGame _owner = null;

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.N:
                    _owner.InitNew();
                    break;
                case Key.Escape:

                    _owner.OnEnd(this, new EndGameEventArgs());
                    PrintMessage("Game is finished!");

                    if (GetYesNoAnswer("Do you want to restart with the same parameters?"))
                    {
                        _owner.InitNew();
                    }
                    break;
                case Key.Down:
                case Key.Up:
                case Key.Left:
                case Key.Right:
                    _move(this, new ChangePositionEventArgs(e.Key));
                    break;
                default:
                    break;
            }
        }

        ChangePositionDelegate _move;
    }
}
