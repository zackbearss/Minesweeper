using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeper
{
    class Board
    {
        List<List<Tile>> tiles;
        int mineCount;
        int tileWidth;
        int tileHeight;
        public int ModifiedMineCount { get; set; }

        Random random;

        public Board(int width, int height, int mineCount, Random random)
        {
            tileWidth = width;
            tileHeight = height;
            this.mineCount = mineCount;
            this.random = random;
            ModifiedMineCount = mineCount;

            //tiles = new List<List<Tile>>(height);
            //foreach (Tile tile in tiles)
            //{
            //    tile.MouseClick += Tile_Click;
            //}
            
            //add mines
            List<List<int>> tempMineList;
            DetermineMinesLocation(out tempMineList);
            foreach(List<int> point in tempMineList)
            {
                tiles[point[0], point[1]].Type = Tile.TileType.Mine;
            }
        }

        private void Tile_Click(object sender, MouseEventArgs e)
        {
            Tile tile = sender as Tile;
            Tile.TileStatus status = new Tile.TileStatus();

            //determine the new status of the tile
            switch(e.Button) {
                case MouseButtons.Left:
                    if (tile.Status != Tile.TileStatus.Unpressed)
                        return;
                    status = Tile.TileStatus.Pressed;
                    break;
                case MouseButtons.Right:
                    if (tile.Status == Tile.TileStatus.Pressed)
                        return;
                    else if (tile.Status == Tile.TileStatus.Unpressed)
                    {
                        status = Tile.TileStatus.Flagged;
                        ModifiedMineCount--;
                    }
                    else if (tile.Status == Tile.TileStatus.Flagged)
                    {
                        status = Tile.TileStatus.Unpressed;
                        ModifiedMineCount++;
                    }
                    break;
            }

            UpdateBoard(tile, status);
        }

        /// <summary>
        /// Determines which locations will contain mines
        /// </summary>
        /// <param name="mineLocation">A container that will out the mine locations.</param>
        void DetermineMinesLocation(out List<List<int>> mineLocation)
        {
            mineLocation = new List<List<int>>();

            while(mineLocation.Count != mineCount)
            {
                List<int> place = new List<int>();
                place.Add(random.Next(0, tileWidth));
                place.Add(random.Next(0, tileHeight));
                if (!mineLocation.Contains(place))
                    mineLocation.Add(place);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="status"></param>
        void UpdateBoard(Tile tile, Tile.TileStatus status)
        {
            //find surronding mine count - if mine count is 0, press other mines around it (recursion)

            if (tile.Update(status, surrondingMineCount) == 1)
            {
                GameOver(false);
                return;
            }

            //check if user selected last box
        }

        int SurrondingMineCount(Tile tile)
        {
           
        }

        void GameOver(bool UserWon)
        {

        }

        void CheckBoxes()
        {
            //complicating recursion 
        }

    }
}
