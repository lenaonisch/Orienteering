using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public class MapParams
    {
        public const byte DEFAULT_OBSTACLE_COUNT = 10;
        public const byte DEFAULT_CHECKPOINT_COUNT = 5;
        public const byte DEFAULT_WIDTH = 10;
        public const byte DEFAULT_HEIGHT = 10;
        public const byte DEFAULT_WATER_AREA = 5;

        public Coord MapSize
        {
            get
            {
                return _mapSize;
            }
            set
            {
                _mapSize = value;
            }
        }
        public byte CheckpointCount
        {
            get
            {
                return _checkpointCount;
            }
            set
            {
                _checkpointCount = value;
            }
        }
        public byte ObstacleCount
        {
            get
            {
                return _obstacleCount;
            }
            set
            {
                _obstacleCount = value;
            }
        }

        public MapParams(uint width = DEFAULT_WIDTH, uint height = DEFAULT_HEIGHT, byte checkpointCount = DEFAULT_CHECKPOINT_COUNT, byte obstacleCount = DEFAULT_OBSTACLE_COUNT)
        {
            _mapSize = new Coord(width, height);
            _checkpointCount = checkpointCount;
            _obstacleCount = obstacleCount;
        }

        public MapParams(Coord size, byte checkpointCount = DEFAULT_CHECKPOINT_COUNT, byte obstacleCount = DEFAULT_OBSTACLE_COUNT)
        {
            _mapSize = size;
            _checkpointCount = checkpointCount;
            _obstacleCount = obstacleCount;
        }

        byte _obstacleCount;
        byte _checkpointCount;
        Coord _mapSize;
    }
}
