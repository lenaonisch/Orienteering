﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public class GameOrienteering : Game
    {
        // return true if player changed his position
        public override void MakeMove(object sender, ChangePositionEventArgs args)
        {
            if (_timer == null) // first step
            {
                _timer = new System.Diagnostics.Stopwatch();
                _timer.Start();
            }

            Coord newCoord = new Coord();
            bool moved = _player.CanMove(args.Offset, ref newCoord);
            if (moved) // move player if possible
            {
                if (_map.TakeCheckpoint(newCoord)) // take checkpoint on new player's position
                {
                    _chkpTaken(this, new CellsEventArgs((Checkpoint)_map[newCoord])); 
                    if (_map.AreAllCheckpointsTaken())
                    {
                        GameControlEventArgs endArgs = new GameControlEventArgs();
                        _endGame(this, ref endArgs); // no restart, no interrupt
                        return;
                    }
                }
                _player.Move(newCoord);
                
                uint R = _player.ViewRadius;
                Checkpoint[] surround = new Checkpoint[(2 * R + 1) * (2 * R + 1) - 1];
                uint chkpFound = 0;
                for (uint layer = 1; layer <= R; layer++)
                {
                    if (_map.CheckSquareForCheckpoint(_player.Position, layer, ref chkpFound, ref surround))
                    {
                        Array.Resize(ref surround, (int)chkpFound);
                        CellsEventArgs arg = new CellsEventArgs(surround);
                        arg._direct = new Direction[surround.Length];
                        arg._len = new uint[surround.Length];
                        for (int i = 0; i < surround.Length; i++)
                        {
                            arg._direct[i] = DefineDirectionToEndPoint(_player.Position, surround[i].Position, out arg._len[i]);
                            if (surround[i].SeenRadius >= layer)
                            {
                                _map[surround[i].Position].Visible = true;
                            }
                        }
                        _chkpSurrondingFound(this, arg);
                        break;
                    }
                }
            }
        }

        public override Game InitNew(MapParams parameters)
        {
            _map = Map.CreateRandom(parameters);
            _player = Map.PlacePlayer(_map);
            foreach (Checkpoint chkp in _map.Checkpoints)
            {
                chkp.Visible = false;
            }
            return this;
        }

        public override Game InitNew()
        {
            MapParams parameters = new MapParams();
            return InitNew(parameters);
        }

        #region vector operations
        //определить направляющие косинусы для вектора, заданного 2 точками
        // будет использовано для определения, в какую сторону света двигаться игроку
        public static Direction DefineDirectionToEndPoint(Coord begin, Coord end, out uint vectorLen)
        {
            Direction d = Direction.NoDirection;
            Point coord = VectorCoord(begin, end);
            double len = VectorLen(coord);
            vectorLen = (uint)len;

            double cosa = coord.x / len;
            double cosb = coord.y / len;

            ///////
            byte quarter = 1;// в какой четверти единичного круга находится конечная точка
            if (cosa >= 0)
            {
                if (cosb < 0)
                {
                    quarter = 4;
                }
            }
            else
            {
                if (cosb < 0)
                {
                    quarter = 3;
                }
                else
                {
                    quarter = 2;
                }
            }

            double anglea = Math.Acos(Math.Abs(cosa))/Math.PI*180; // rad to degree
            double angleb = Math.Acos(Math.Abs(cosb))/Math.PI*180;

            switch (quarter)
            {
                case 1:
                    if (anglea <= 22.5) // распределение сторон света на круге
                    {
                        d = Direction.East;
                    }
                    else
                    {
                        d = Direction.South;//Direction.North; // because y-axis on console is directed down!)))
                        if (angleb >= 22.5)
                        {
                            d += (byte)Direction.East;
                        }
                    }
                    break;
                case 2:
                    if (anglea <= 22.5)
                    {
                        d = Direction.West;
                    }
                    else
                    {
                        d = Direction.South;//Direction.North;
                        if (angleb >= 22.5)
                        {
                            d += (byte)Direction.West;
                        }
                    }
                    break;
                case 3:
                    if (anglea <= 22.5)
                    {
                        d = Direction.West;
                    }
                    else
                    {
                        d = Direction.North;//Direction.South;
                        if (angleb >= 22.5)
                        {
                            d += (byte)Direction.West;
                        }
                    }
                    break;
                case 4:
                    if (anglea <= 22.5)
                    {
                        d = Direction.East;
                    }
                    else
                    {
                        d = Direction.North;//Direction.South;
                        if (angleb >= 22.5)
                        {
                            d += (byte)Direction.East;
                        }
                    }
                    break;
            }
            return d;
        }

        public static double VectorLen(Point p)
        {
            return Math.Sqrt(p.x * p.x + p.y * p.y);
        }

        public static Point VectorCoord(Coord begin, Coord end)
        {
            return new Point((int)(end.y - begin.y), (int)(end.x - begin.x));
        }
        #endregion
    }
}
