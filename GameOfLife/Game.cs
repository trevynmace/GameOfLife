// Trevyn Mace

using System;

namespace GameOfLife
{
    class Game
    {
        private int[,,] map = new int[25, 40, 2];
        private int priorMap = 1;
        private int newMap = 0;

        public Game()
        {
            CreateMap(priorMap);
            CreateMapWithCells();
        }

        private void Setup()
        {
            SetupConsole();
            ShowTitle();
            ShowBeginMap();
            ShowMap();
            ShowFooter("Press enter to start", ConsoleColor.Green);
        }

        private void CreateMap(int n)
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    map[i, j, n] = 0;
                }
            }
        }

        private void CreateMapWithCells()
        {
            CreateMap(0);

            for (int i = 8; i < 11; i++)
            {
                map[i, 12, 0] = 1;
                map[i, 20, 0] = 1;
                map[i, 28, 0] = 1;
            }

            for (int i = 14; i < 27; i++)
            {
                map[13, i, 0] = 1;
            }
        }

        public void Start()
        {
            Setup();
            Console.ReadLine();
            ShowFooter("Press any key to stop", ConsoleColor.DarkRed);

            while(!Console.KeyAvailable)
            {
                ChangeMap();
                UpdateMap();
                ShowMap();
            }

            ShowFooter("Press any key to stop", ConsoleColor.Black);
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }

        private void ChangeMap()
        {
            int n = priorMap;
            priorMap = newMap;
            newMap = n;
        }

        private void SetupConsole()
        {
            Console.WindowHeight = 45;
            Console.WindowWidth = 70;
            Console.CursorVisible = false;
        }

        private void UpdateMap()
        {
            int n = (newMap + 1) % 2;

            for(int row = 0; row < map.GetLength(0); row++)
            {
                for(int col = 0; col < map.GetLength(1); col++)
                {
                    map[row, col, newMap] = UpdateCell(row, col);
                }
            }
        }

        private int UpdateCell(int row, int col)
        {
            int livingNeighbors = GetLivingNeighbors(row, col);
            int n = livingNeighbors != 3 ? (livingNeighbors != 2 ? 0 : map[row, col, priorMap]) : 1;
            return n;
        }

        private int GetLivingNeighbors(int row, int col)
        {
            int n1 = 0;
            int row1 = row - 1;
            for (int col1 = col - 1; col1 <= col + 1; col1++)
            {
                n1 += IsAlive(row1, col1) ? 1 : 0;
            }

            int n2 = n1 + (IsAlive(row, col - 1) ? 1 : 0) + (IsAlive(row, col + 1) ? 1 : 0);
            int row2 = row + 1;
            for (int col1 = col - 1; col1 <= col + 1; col1++)
            {
                n2 += IsAlive(row2, col1) ? 1 : 0;
            }

            return n2;
        }

        private bool IsAlive(int row, int col)
        {
            if(row < 0 || col < 0 || row >= map.GetLength(0) || col >= map.GetLength(1))
            {
                return false;
            }
            else
            {
                return map[row, col, priorMap] != 0;
            }
        }

        private void ShowTitle()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkRed;

            for(int i = 0; i < 3; i++)
            {
                Console.CursorTop = 6 + i;
                Console.CursorLeft = 15;

                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(".");
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.CursorTop = 7;
            Console.CursorLeft = 25;
            Console.Write("G A M E   O F   L I F E");
        }

        private void ShowBeginMap()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            for(int i = 0; i < map.GetLength(0); i++)
            {
                Console.CursorTop = i + 10;
                Console.CursorLeft = 15;

                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(".");
                }
            }
        }

        private void ShowMap()
        {
            for(int i = 0; i < map.GetLength(0); i++)
            {
                for(int j = 0; j < map.GetLength(1); j++)
                {
                    if(map[i, j, 0] != map[i, j, 1])
                    {
                        Console.CursorTop = i + 10;
                        Console.CursorLeft = j + 15;

                        if(map[i, j, newMap] == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.Write(".");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.Write(".");
                        }
                    }
                }
            }
        }

        private void ShowFooter(string text, ConsoleColor foreground)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = ConsoleColor.Black;
            SetCursorToFooter();
            Console.WriteLine(text);
        }

        private void SetCursorToFooter()
        {
            Console.CursorTop = 37;
            Console.CursorLeft = 25;
        }
    }
}
