using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFApp
{
    public partial class frmGameParams : Form
    {
        public frmGameParams()
        {
            
            InitializeComponent();
            edtHeight.Value = Orienteering.MapParams.DEFAULT_HEIGHT;
            edtWidth.Value = Orienteering.MapParams.DEFAULT_WIDTH;
        }

        public Orienteering.GameType GetGameType()
        {
            Orienteering.GameType gt = Orienteering.GameType.None;
            if (rbMaze.Checked)
            {
                gt = Orienteering.GameType.Maze;
            }
            if (rbOrienteering.Checked)
            {
                gt = Orienteering.GameType.Orienteering;
            }
            return gt;
        }

        public Orienteering.MapParams GetMapParameters()
        {

            return new Orienteering.MapParams(new Orienteering.Coord((uint)edtHeight.Value, (uint)edtWidth.Value));
        }
    }
}
