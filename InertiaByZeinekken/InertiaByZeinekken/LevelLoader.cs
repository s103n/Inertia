using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace InertiaByZeinekken
{
    class LevelLoader
    {
        private string[] field;

        public LevelLoader(int level)
        {
            field = new FileStorer($"../../Levels/{level}.txt").Result;
        }

        public void LoadField(out Field cellField, out Player player, out Enemy enemy)
        {
            cellField = new Field(field[0].Length, field.Length);
            player = new Player(0, 0);
            enemy = new Enemy(0, 0, cellField);
            for (int y = 0; y < field.Length; y++)
            {
                for (int x = 0; x < field[y].Length; x++)
                {
                    switch (field[y][x])
                    {
                        case '#':
                            cellField[x, y] = new Wall(x, y);
                            break;
                        case ' ':
                            cellField[x, y] = new Empty(x, y);
                            break;
                        case 'K':
                            cellField[x, y] = new DoorKey(x, y);
                            break;
                        case 'D':
                            cellField[x, y] = new Door(x, y);
                            break;
                        case '@':
                            cellField[x, y] = new Prize(x, y);
                            break;
                        case '%':
                            cellField[x, y] = new Trap(x, y);
                            break;
                        case '.':
                            cellField[x, y] = new Stop(x, y);
                            break;
                        case 'P':
                            player = new Player(x, y);
                            goto case ' ';
                        case 'X':
                            enemy = new Enemy(x, y, cellField);
                            break;
                        case 'o':
                            cellField[x, y] = new Coin(x, y);
                            break;
                        case 'C':
                            cellField[x, y] = new Chest(x, y);
                            break;
                        case 'T':
                            cellField[x, y] = new Teleport(x, y);
                            break;
                        case '+':
                            cellField[x, y] = new TimePlus(x, y);
                            break;
                        case '-':
                            cellField[x, y] = new TimeMinus(x, y);
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }



}
