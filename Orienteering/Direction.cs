using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    [Flags]
    public enum Direction: byte
    {
        NoDirection = 0,
        North = 1,
        East = 2,
        South = 4,
        West = 8
    }
}
