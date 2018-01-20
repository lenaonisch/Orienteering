using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public partial class Map
    {
        #region checkpoints

        public bool AreCheckpointsClose(Coord c, out Checkpoint[] checkpoints, bool taken = false, uint r = 1)
        {
            int Ystart = (int)c.y - (int)r;
            int Xstart = (int)c.x - (int)r;
            if (Ystart < 0)
            {
                Ystart = 0;
            }
            if (Xstart < 0)
            {
                Xstart = 0;
            }

            int Ystop = (int)c.y + (int)r;
            int Xstop = (int)c.x + (int)r;
            if (Ystop > Height)
            {
                Ystop = (int)Height;
            }
            if (Xstop > Width)
            {
                Xstop = (int)Width;
            }

            checkpoints = new Checkpoint[(2 * r + 1) * (2 * r + 1) - 1];
            int found = 0;
            for (uint y = (uint)Ystart; y < Ystop; y++)
            {
                for (uint x = (uint)Xstart; x < Xstop; x++)
                {
                    if (!IsCheckpoint(y, x, out checkpoints[found]) || (y == c.y && x == c.x))
                    {
                        continue;
                    }
                    else
                    {
                        if (checkpoints[found].Taken == taken)
                        {
                            ++found;
                        }
                    }
                }
            }
            Array.Resize(ref checkpoints, found);
            return found != 0;
        }

        public bool AreAllCheckpointsTaken()
        {
            return _takenCheckpoints == Checkpoints.Length;
        }

        public bool TakeCheckpoint(Coord coord)
        {
            Checkpoint chkp;
            if (IsCheckpoint(coord, out chkp))
            {
                return TakeCheckpoint(chkp);
            }
            return false;
        }
        public bool TakeCheckpoint(Checkpoint chkp)
        {
            bool ret = false;
            if (chkp != null && !chkp.Taken)
            {
                _takenCheckpoints++;
                chkp.Taken = true;
                chkp.Visible = false;
                ret = true;
            }
            return ret;
        }

        private byte _takenCheckpoints = 0;
        #endregion
    }
}
