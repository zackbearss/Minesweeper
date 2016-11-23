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

            tiles = new List<List<Tile>>();
            for (int i = 0; i < height; i++)
            {
                List<Tile> tilesWidth = new List<Tile>();
                for (int j = 0; j < width; j++)
                {
                    Tile tile = new Tile();
                    tile.MouseClick += Tile_Click;
                    tilesWidth.Add(tile);
                }
                tiles.Add(tilesWidth);
            }

            //add mines
            List<Mine> tempMineList = DetermineMinesLocation();
            foreach(Mine point in tempMineList)
            {
                tiles[point.X][point.Y].Type = Tile.TileType.Mine;
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
        List<Mine> DetermineMinesLocation()
        {
            List<Mine> mineLocation = new List<Mine>();

            while(mineLocation.Count != mineCount)
            {
                Mine mine = new Mine();
                mine.X = random.Next(0, tileWidth);
                mine.Y = random.Next(0, tileHeight);
                if (!mineLocation.Contains(mine))
                    mineLocation.Add(mine);
            }

            return mineLocation;
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
            int Xlocation;
            int YLocation;

            for (int i = 0; i < tileHeight; i++)
            {
                if(tiles[i].Contains(tile))
                {
                    YLocation = i;
                    Xlocation = tiles[i].IndexOf(tile);
                }
            }


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
