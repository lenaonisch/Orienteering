using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    [Flags]
    public enum CrossingType
    {
        None = 0, // 0x00000
        Rope = 1, // 0x00001
        Wood = 2  // 0x00010 // not used anywhere yet
    }
}
