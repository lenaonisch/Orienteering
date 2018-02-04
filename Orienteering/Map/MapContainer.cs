using System;
using System.Collections.Generic;
using System.Text;

namespace Orienteering
{
    public partial class Map
    {
        public Map(Coord size)
        {
            Field = new Cell[size.y, size.x];
            this.Size = size;
        }

        public Checkpoint[] Checkpoints; // just references on cells!
        public Cell[,] Field;
        

        public Coord Size { get; private set; }

        public uint Height
        {
            get
            {
                return Size.y;
            }
        }

        public uint Width
        {
            get
            {
                return Size.x;
            }
        }

        public Cell this[uint y, uint x]
        {
            get
            {
                return Field[y, x];
            }
            set
            {
                Field[y, x] = value;
            }
        }

        public Cell this[Coord c]
        {
            get
            {
                return this[c.y, c.x];
            }
            set 
            {
                this[c.y, c.x] = value;
            }
        }

        public Coord GetRandomEmptyCell()
        {
            uint x, y;

            do
            {
                x = Randomizer.Next(0, Size.x);
                y = Randomizer.Next(0, Size.y);
            }
            while (Field[y, x] != null);
            return new Coord(y, x);
        }

        #region IsCell...
        public bool IsCheckpoint(int y, int x, out Checkpoint chkp)
        {
            if (y < 0 || x < 0)
            {
                throw new CoordOutOfMapException(String.Format("y and x should be non-negative. y = {0}, x = {1}", y, x), y, x);
            }
            return IsCheckpoint((uint)y, (uint)x, out chkp);
        }
        public bool IsCheckpoint(uint y, uint x, out Checkpoint chkp)
        {
            chkp = Field[y, x] as Checkpoint;
            return chkp != null;
        }
        public bool IsCheckpoint(Coord position, out Checkpoint chkp)
        {
            return IsCheckpoint(position.y, position.x, out chkp);
        }

        public bool IsEmptyCell(uint y, uint x)
        {
            return (Field[y, x] == null);
        }

        public bool IsEmptyCell(Coord position)
        {
            return IsEmptyCell(position.y, position.x);
        }

        protected bool IsEmptyCell(Cell c)
        {
            return (c == null) || IsEmptyCell(c.Position);
        }
        #endregion
        
        #region Init random maze
        // Присвойте ячейкам, не входящим в множество, свое уникальное множество.
        private static void CreateFirstMazeLine(int[,] Field)
        {
            for (int i = 0; i < Field.GetLength(1); i+=2)
            {
                Field[0, i] = i + 1;
                Field[0, i + 1] = i + 1;
            }
            CreateRightBounds(Field);
        }

        //private static bool CreateBound(int[,] Field, int row, int col)
        //{
        //    bool ret = true;

        //    return ret;
        //}
        
        //    Создайте правые границы, двигаясь слева направо:
        //      Случайно решите добавлять границу или нет
        //          Если текущая ячейка и ячейка справа принадлежат одному множеству, то создайте границу между ними (для предотвращения зацикливаний)
        //          Если вы решили не добавлять границу, то объедините два множества в которых находится текущая ячейка и ячейка справа.
        private static void CreateRightBounds(int[,] Field, int line = 0)
        {
            int stop = (Field.GetLength(1) - 3);
            //Создайте правые границы, двигаясь слева направо:
            for (int i = 0; i < stop; i += 2)   
            {
                // Если текущая ячейка и ячейка справа принадлежат одному множеству, то создайте границу между ними (для предотвращения зацикливаний)
                if (Field[line, i] == Field[line, i + 2])
                {
                    Field[line, i + 1] = -1;
                    continue;
                }

                // Случайно решите добавлять границу или нет. -1 - граница
                int r = Randomizer.Next(0, 10);
                if (r < 6)
                {
                    Field[line, i + 1] = -1;
                }
                else  //Если вы решили не добавлять границу, то объедините два множества в которых находится текущая ячейка и ячейка справа.
                {
                    Field[line, i + 2] = Field[line, i];
                    Field[line, i + 3] = Field[line, i];
                } 
            }           
        }

        //private static void CreateRightBounds(int[,] Field, int line)
        //{
        //    //Создайте правые границы, двигаясь слева направо:
        //    for (int i = (Field.GetLength(1) - 2); i > 0; i--)
        //    {
        //        //if (Field[line, i] == Field[line, i - 1] && Field[line - 1, i] == -1)
        //        //{
        //        //    Field[line, i] = -1;
        //        //    i--;
        //        //    continue;
        //        //}
        //        if ((Field[line - 1, i - 1] != -1 || Field[line - 1, i + 1] != -1) && Randomizer.Next(2) == 0) // Случайно решите добавлять границу или нет. -1 - граница
        //        {
        //            Field[line, i] = -1;
        //            //if (Field[line - 1, i - 1] != -1)
        //            //{
        //            //    Field[line, --i] = -1;
        //            //}
        //            //if (Field[line - 1, i + 1] != -1)
        //            //{
        //            //    Field[line, i+1] = -1;
        //            //}
        //            i--;
        //        }
        //        else  //Если вы решили не добавлять границу, то объедините два множества в которых находится текущая ячейка и ячейка справа.
        //        {
        //            Field[line, i] = Field[line, i + 1];
        //        }
        //        //i--;
        //    }
        //}

        //Создайте границы снизу, двигаясь слева направо:
        //    Случайно решите добавлять границу или нет. Убедитесь что каждое множество имеет хотя бы одну ячейку без нижней границы (для предотвращения изолирования областей)
        //        Если ячейка в своем множестве одна, то не создавайте границу снизу
        //        Если ячейка одна в своем множестве без нижней границы, то не создавайте нижнюю границу

        private static void CreateLowerBound(int[,] Field, int line)
        {
            int curArea = Field[line - 1, 0];
            uint curStartAreaInd = 0;
            int lastInd = Field.GetLength(1) - 2;
            // Создайте границы снизу, двигаясь слева направо:
            for (uint i = 0; i < lastInd; i += 2)
            {
                //if (Field[line - 1, i] == -1)
                //{
                //    Field[line, i] = -1;
                //    curArea = Field[line - 1, i + 1];
                //    curStartAreaInd = i + 1;
                //    continue;
                //}

                if (Randomizer.Next(0, 10) < 6) // Случайно решите добавлять границу или нет. 
                {
                    //Если ячейка в своем множестве одна, 
                    //Если ячейка одна в своем множестве без нижней границы, то не создавайте нижнюю границу
                    uint k = 0;
                    curArea = Field[line - 1, i];
                    while (k <= lastInd && Field[line - 1, k] < curArea)
                    {
                        k++;
                    }
                    uint count = 0; // кол-во ячеек без нижних границ
                    while (k <= lastInd && Field[line - 1, k] <= curArea)
                    {
                        if (Field[line, k] != -1)
                        {
                            count++;
                        }
                        k += 2;
                    }
                    if (count > 1)
                    {
                        Field[line, i] = -1;
                        //curArea = Field[line - 1, i + 2];
                    }
                    else
                    {
                        Field[line, i] = Field[line - 1, i];
                        //curArea = Field[line - 1, i + 2];
                    }
                }
                else
                {
                    Field[line, i] = Field[line - 1, i];
                    //curArea = Field[line, i];
                }
                
            }
            //Field[line, lastInd] = Field[line - 1, lastInd];
        }

        private static void Step5a(int[,] Field, int line) // line = 2, 4, ...
        {
            //Выведите текущую строку. Удалите все правые границы
            for (int i = 0; i < Field.GetLength(1); i++)
            {
                if (Field[line - 1, i] != -1)
                {
                    Field[line, i] = Field[line - 1, i];
                    Field[line + 1, i] = Field[line - 1, i];
                }
                else
                {
                    Field[line, i] = i + 1;
                    Field[line + 1, i] = i + 1;
                }
            }
        }

        //private static void InitRandomRoute(Map map)
        //{
        //    // source: https://habrahabr.ru/post/176671/
        //    int[,] field = new int[map.size.y, map.size.x];
        //    CreateFirstMazeLine(field);
        //    for (int line = 1; line < field.GetLength(0) - 3; line += 2)
        //    {
        //        CreateLowerBound(field, line);
        //        Step5a(field, line + 1);
        //        CreateRightBounds(field, line + 2);
        //    }
        //    //CreateLowerBound(field, field.GetLength(0) - 1);

        //    for (int i = 0; i < field.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < field.GetLength(1); j++)
        //        {
        //            if (field[i, j] != -1)
        //            {
        //                map.Field[i, j] = null;
        //            }
        //            else
        //            {
        //                map.Field[i, j] = new Obstacle(map); //new Cell(map, MapCellType.Obstacle);
        //            }
        //        }
        //    }
        //}

        #endregion

        #region Random creation
        public static Map CreateRandom(MapParams param)
        {
            Map map = new Map(param.MapSize)
            {
                Checkpoints = new Checkpoint[param.CheckpointCount],
            };

            map._mapParam = param;
            InitRandomObstacles(map);

            InitRandomCheckpoints(map);

            return map;
        }

        protected static void InitRandomCheckpoints(Map map)
        {
            for (int i = 0; i < map.Checkpoints.Length; i++)
            {
                Coord randCoord = map.GetRandomEmptyCell();
                map.Checkpoints[i] = new Checkpoint(map, randCoord);
                map.Field[randCoord.y, randCoord.x] = map.Checkpoints[i];
            }
        }

        protected static void InitRandomObstacles(Map map)
        {
            string[] enumVal = Enum.GetNames(typeof(ObstacleType));
            for (int i = 0; i < map._mapParam.ObstacleCount; i++)
            {
                int itype = Randomizer.Next(0, enumVal.Length);
                ObstacleType type = (ObstacleType)Enum.Parse(typeof(ObstacleType), enumVal[itype]);
                switch (type)
                {
                    case ObstacleType.Tree:
                        Tree.CreateRandom(map);
                        break;
                    case ObstacleType.River:
                    case ObstacleType.Swamp:
                        Water.CreateRandom(map, MapParams.DEFAULT_WATER_AREA, type);
                        break;
                    default:
                        break;
                }
            }
        }

        public static Person PlacePlayer(Map map)
        {
            Person p = new Person(map, map.GetRandomEmptyCell());
            map[p.Position] = p;
            return p;
        }
        #endregion

        public Checkpoint FindCheckpoint(Coord position)
        {
            if (Field[position.y, position.x] is Checkpoint)
            {
                return Array.Find(Checkpoints, t => t.Position.x == position.x && t.Position.y == position.y);
            }
            return null;
        }

        protected MapParams _mapParam;
    }
}
