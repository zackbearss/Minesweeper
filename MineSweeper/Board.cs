using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeper
{
    class Board
    {
        internal Action<bool> GameOver;
        internal Action GameStarted;

        List<List<Tile>> tiles;
        int mineCount;
        int tileWidth;
        int tileHeight;
        int tilesPressed;   //counting tiles pressed
        public int ModifiedMineCount { get; set; }  //counting flags 
        bool HasGameStarted;

        Random random;

        public Board(int width, int height, int mineCount, Random random)
        {
            HasGameStarted = false;
            tileWidth = width;
            tileHeight = height;
            this.mineCount = mineCount;
            this.random = random;
            ModifiedMineCount = mineCount;
            tilesPressed = 0;

			CreateTiles();
			AddMines();
        }

		private void CreateTiles()
		{
			tiles = new List<List<Tile>>();
			for (int i = 0; i < tileHeight; i++)
			{
				List<Tile> tilesWidth = new List<Tile>();
				for (int j = 0; j < tileWidth; j++)
				{
					Tile tile = new Tile();
					tile.ClickEvent += ClickHandler;
					tilesWidth.Add(tile);
				}
				tiles.Add(tilesWidth);
			}
		}

		private void AddMines()
		{
			List<Mine> tempMineList = DetermineMinesLocation();
			foreach (Mine point in tempMineList)
			{
				tiles[point.Y][point.X].Type = Tile.TileType.Mine;
			}
		}

		private void ClickHandler(Tile tile, MouseButtons button)
		{
			Tile.TileStatus status = new Tile.TileStatus();
            if(!HasGameStarted)
            {
                HasGameStarted = true;
                GameStarted();
            }
			//determine the new status of the tile
			switch (button)
			{
				case MouseButtons.Left:
					if (tile.Status != Tile.TileStatus.Unpressed)
						return;
					status = Tile.TileStatus.Pressed;
                    tilesPressed++;
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
			//TODO: change name of function
			if (surrondingMineCount == 0)
				OpenSurrondingTiles(tile);
            if ((tileWidth * tileHeight) - mineCount == tilesPressed)
                GameOver(true);
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

		void OpenSurrondingTiles(Tile tile)
		{
			int XLocation = 0;
			int YLocation = 0;

			for (int i = 0; i < tileHeight; i++)
			{
				if (tiles[i].Contains(tile))
				{
					YLocation = i;
					XLocation = tiles[i].IndexOf(tile);
				}
			}

			//check 8 surronding mines 
			if (XLocation - 1 >= 0)
			{
				if (YLocation - 1 >= 0)
				{
					tiles[YLocation - 1][XLocation - 1].ClickEvent(tiles[YLocation - 1][XLocation - 1], MouseButtons.Left);
				}
				if (YLocation + 1 < tileHeight)
				{
					tiles[YLocation + 1][XLocation - 1].ClickEvent(tiles[YLocation + 1][XLocation - 1], MouseButtons.Left);
				}
				tiles[YLocation][XLocation - 1].ClickEvent(tiles[YLocation][XLocation - 1], MouseButtons.Left);
			}
			if (XLocation + 1 < tileWidth)
			{
				if (YLocation - 1 >= 0)
				{
					tiles[YLocation - 1][XLocation + 1].ClickEvent(tiles[YLocation - 1][XLocation + 1], MouseButtons.Left);
				}
				if (YLocation + 1 < tileHeight)
				{
					tiles[YLocation + 1][XLocation + 1].ClickEvent(tiles[YLocation + 1][XLocation + 1], MouseButtons.Left);
				}
				tiles[YLocation][XLocation + 1].ClickEvent(tiles[YLocation][XLocation + 1], MouseButtons.Left);
			}

			if (YLocation - 1 >= 0)
			{
				tiles[YLocation - 1][XLocation].ClickEvent(tiles[YLocation - 1][XLocation], MouseButtons.Left);
			}
			if (YLocation + 1 < tileHeight)
			{
				tiles[YLocation + 1][XLocation].ClickEvent(tiles[YLocation + 1][XLocation], MouseButtons.Left);
			}
		}

        public void DrawBoard(TableLayoutPanel mineField)
        {
            //add buttons
            for (int i = 0; i < tileHeight; i++)
            {
                for (int j = 0; j < tileWidth; j++)
                {
                    mineField.Controls.Add(tiles[i][j].TileButton, j, i);
					mineField.Controls.Add(tiles[i][j].TileLabel, j, i);
				}
            }
        }

    }
}
