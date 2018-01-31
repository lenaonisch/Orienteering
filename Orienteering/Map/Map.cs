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

        private void CheckCpiralFromCenter(Coord center, ref Checkpoint[] result, uint R = 1)
        {
            uint start = 0;
            for (uint layer = 1; layer <= R; layer++)
            {
                CheckSquareForCheckpoint(center, layer, ref start, ref result);
            }
        }

        public bool CheckSquareForCheckpoint(Coord center, uint layer, ref uint start_index, ref Checkpoint[] result)
        {
            uint already_found = start_index;

            #region min-max coord
            bool top = true, right = true, bottom = true, left = true;
            int Ystart = (int)center.y - (int)layer;
            int Xstart = (int)center.x - (int)layer;
            if (Ystart < 0)
            {
                top = false;
                Ystart = 0;
            }
            if (Xstart < 0)
            {
                left = false;
                Xstart = 0;
            }

            int Ystop = (int)center.y + (int)layer;
            int Xstop = (int)center.x + (int)layer;
            if (Ystop >= Height)
            {
                bottom = false;
                Ystop = (int)Height - 1;
            }
            if (Xstop >= Width)
            {
                right = false;
                Xstop = (int)Width - 1;
            }
            #endregion

            if (top)
            {
                for (int j = Xstart; j <= Xstop; j++)
                {
                    if (IsCheckpoint(Ystart, j++, out result[start_index]))
                    {
                        ++start_index;
                    }
                }
            }
            if (right)
            {
                for (int i = top ? Ystart+1:Ystart; i <= Ystop; i++)
                {
                    if (IsCheckpoint(i, Xstop, out result[start_index]))
                    {
                        ++start_index;
                    }
                }
            }
            if (bottom)
            {
                for (int j = right?Xstop-1 :Xstop ; j >= Xstart; j--)
                {
                    if (IsCheckpoint(Ystop, j, out result[start_index]))
                    {
                        ++start_index;
                    }
                }
            }
            if (left)
            {
                for (int i = bottom? Ystop-1: Ystop; i > Ystart; i--)
                {
                    if (IsCheckpoint(i, Xstart, out result[start_index]))
                    {
                        ++start_index;
                    }
                }
            }

            return already_found != start_index;
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
