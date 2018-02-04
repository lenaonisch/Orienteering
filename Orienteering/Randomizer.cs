using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public static class Randomizer
    {
        public static Random rand = new Random();

        public static int Next(int minValue = 0, int maxValue = Int32.MaxValue)
        {
            return rand.Next(minValue, maxValue);
        }

        public static uint Next(uint minValue = 0, uint maxValue = Int32.MaxValue)
        {
            return (uint)rand.Next((int)minValue, (int)maxValue);
        }

        public static Direction NextNotOppositeDirection(Direction oldDirection)
        {
            Direction newDirection;
            do
            {
                var values = Enum.GetValues(typeof(Direction));
                newDirection = (Direction)values.GetValue(Randomizer.Next(1, values.Length));
            }
            while (oldDirection == newDirection || oldDirection == Coord.GetOppositeDirection(oldDirection));
            return newDirection;
        }
    }
}
