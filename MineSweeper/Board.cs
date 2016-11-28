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
            int surrondingMineCount = SurrondingMineCount(tile);
            //todo: recursion
            if (tile.Update(status, surrondingMineCount) == 1)
            {
                GameOver(false);
                return;
            }

            //check if user selected last box
        }

        int SurrondingMineCount(Tile tile)
        {
            int XLocation = 0;
            int YLocation = 0;
            int MineCount = 0;

            for (int i = 0; i < tileHeight; i++)
            {
                if(tiles[i].Contains(tile))
                {
                    YLocation = i;
                    XLocation = tiles[i].IndexOf(tile);
                }
            }

            //check 8 surronding mines 
            if(XLocation - 1 >= 0)
            {
                if(YLocation - 1 >= 0)
                {
                    if (tiles[YLocation - 1][XLocation - 1].Type == Tile.TileType.Mine)
                        MineCount++;
                }
                if(YLocation + 1 < tileHeight)
                {
                    if (tiles[YLocation + 1][XLocation - 1].Type == Tile.TileType.Mine)
                        MineCount++;
                }
                if (tiles[YLocation][XLocation - 1].Type == Tile.TileType.Mine)
                        MineCount++;
            }
            if(XLocation + 1 < tileWidth)
            {
                if (YLocation - 1 >= 0)
                {
                    if (tiles[YLocation - 1][XLocation + 1].Type == Tile.TileType.Mine)
                        MineCount++;
                }
                if (YLocation + 1 < tileHeight)
                {
                    if (tiles[YLocation + 1][XLocation + 1].Type == Tile.TileType.Mine)
                        MineCount++;
                }
                if (tiles[YLocation][XLocation + 1].Type == Tile.TileType.Mine)
                    MineCount++;
            }

            if (YLocation - 1 >= 0)
            {
                if (tiles[YLocation - 1][XLocation].Type == Tile.TileType.Mine)
                    MineCount++;
            }
            if (YLocation + 1 < tileHeight)
            {
                if (tiles[YLocation + 1][XLocation].Type == Tile.TileType.Mine)
                    MineCount++;
            }

            return MineCount;
        }

        void GameOver(bool UserWon)
        {

        }

        void CheckBoxes()
        {
            //complicating recursion 
        }

        public void DrawBoard(TableLayoutPanel mineField)
        {
            //add buttons
            for (int i = 0; i < tileHeight; i++)
            {
                for (int j = 0; j < tileWidth; j++)
                {
                    mineField.Controls.Add(tiles[i][j], j, i);
                }
            }
        }

    }
}
