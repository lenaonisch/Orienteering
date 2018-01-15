using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Orienteering;

namespace WFApp
{
    public partial class frmMain : Form, IView 
    {
        public frmMain()
        {
            InitializeComponent();
            
        }
        public void PrintMap(Map map)
        {
            throw new NotImplementedException();
        }

        public void PrintMap(Map map, Coord canvasOffset)
        {
            throw new NotImplementedException();
        }

        public void ReprintChangedCells(params Cell[] cells)
        {
            throw new NotImplementedException();
        }

        public void PrintMessage(string format, params object[] args)
        {
            MessageBox.Show(String.Format(format, args), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void PrintError(string format, params object[] args)
        {
            MessageBox.Show(String.Format(format, args),"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void PrintWarning(string format, params object[] args)
        {
            MessageBox.Show(String.Format(format, args), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public System.Windows.Input.Key GetUserInput()
        {
            throw new NotImplementedException();
        }

        public bool GetYesNoAnswer(string format, params object[] args)
        {
            if (MessageBox.Show(String.Format(format, args), "Game finished", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.N:
                    _owner.InitNew();
                    break;
                case Keys.Escape:
                
                    _owner.OnEnd(this, new EndGameEventArgs());
                    PrintMessage("Game is finished!");

                    if (GetYesNoAnswer("Do you want to restart with the same parameters?"))
                    {
                        _owner.InitNew();
                    }
                    break;
                case Keys.Down:
                    _move(this, new ChangePositionEventArgs(System.Windows.Input.Key.Down));
                    break;
                case Keys.Up:
                    _move(this, new ChangePositionEventArgs(System.Windows.Input.Key.Up));
                    break;
                case Keys.Left:
                    _move(this, new ChangePositionEventArgs(System.Windows.Input.Key.Left));
                    break;
                case Keys.Right:
                    _move(this, new ChangePositionEventArgs(System.Windows.Input.Key.Right));
                    break;
                default:
                    break;
            }
        }

        #region events  
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

        ChangePositionDelegate _move;
        #endregion

        Game _owner = null;


        public void ShowHint(string hint, params object[] args)
        {
            lblHint.Text = hint;
            lblHint.Refresh();
        }


        public Game Owner
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




        public MapParams MapParameters
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
