using System;
using System.Collections.Generic;
using System.Linq;

namespace Labirint
{
    internal class GenForm
    {
        int width;
        int height;
        int[,] mCells;

        /// <summary>
        /// generira labirint pomoću DFS algoritma
        /// </summary>
        /// <param name="sizeMX"></param>
        /// <param name="sizeMY"></param>
        /// <param name="sizeRX"></param>
        /// <param name="sizeRY"></param>
        /// <returns></returns>
        public int[,] generateMaze(int sizeMX, int sizeMY, int sizeRX, int sizeRY)
        {
            width = sizeMX /sizeRX;
            height = sizeMY/sizeRY;
            mCells = new int[width, height];
            /*
            Random rand = new Random();
            // r for row、c for column
            // Generate random r
            int r = rand.Next(height);
            while (r % 2 == 0)
            {
                r = rand.Next(height);
            }
             //Generate random c
            int c = rand.Next(width);
            while (c % 2 == 0)
            {
                c = rand.Next(width);
            } */
            int r = 0;
            int c = 0;

            mCells[r, c] = 1;

            recursion(r, c);

            return mCells;
        }

        private void recursion(int r, int c)
        {
            // 4 random directions
            int[] randDirs = new int[4];
            randDirs = generateRandomDirections();
            // Examine each direction
            for (int i = 0; i < randDirs.Length; i++)
            {

                switch (randDirs[i])
                {
                    case 1: // Up
                            //　Whether 2 cells up is out or not
                        if (r - 2 <= 0)
                            continue;
                        if (mCells[r - 2, c] == 0)
                        {
                            mCells[r - 2, c] = 1;
                            mCells[r - 1, c] = 1;
                            recursion(r - 2, c);
                        }
                        break;
                    case 2: // Right
                            // Whether 2 cells to the right is out or not
                        if (c + 2 >= width - 1)
                            continue;
                        if (mCells[r, c + 2] == 0)
                        {
                            mCells[r, c + 2] = 1;
                            mCells[r, c + 1] = 1;
                            recursion(r, c + 2);
                        }
                        break;
                    case 3: // Down
                            // Whether 2 cells down is out or not
                        if (r + 2 >= height - 1)
                            continue;
                        if (mCells[r + 2, c] == 0)
                        {
                            mCells[r + 2, c] = 1;
                            mCells[r + 1, c] = 1;
                            recursion(r + 2, c);
                        }
                        break;
                    case 4: // Left
                            // Whether 2 cells to the left is out or not
                        if (c - 2 <= 0)
                            continue;
                        if (mCells[r, c - 2] == 0)
                        {
                            mCells[r, c - 2] = 1;
                            mCells[r, c - 1] = 1;
                            recursion(r, c - 2);
                        }
                        break;
                }
            }
        }

        private int[] generateRandomDirections()
        {
            List<int> randoms = new List<int>();
            for (int i = 0; i < 4; i++)
                randoms.Add(i + 1);

            IEnumerable<int> shuffled = randoms.OrderBy(a => Guid.NewGuid());

            return shuffled.ToArray();
        }
    }
}
