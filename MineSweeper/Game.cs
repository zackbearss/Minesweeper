using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeper
{
    class Game
    {
        public enum Difficulty
        {
            Easy,   //8x8, 10 mines
            Medium, //16x16, 40 mines
            Hard    //24x24, 99 mines
        }

        Board board;
        Difficulty difficulty;
        Label timeDisplay;
        Label mineDisplay;
        TableLayoutPanel mineField;
        Form window;
        Timer timer;
        Random random;
        int width;
        int height;
        int mineCount;
        int timeElapsed;

        public Game(Form Window, Label TimeDisplay, Label MineDisplay, TableLayoutPanel MineField)
        {
            window = Window;
            timeDisplay = TimeDisplay;
            mineDisplay = MineDisplay;
            mineField = MineField;
            timer = new Timer();

            timeDisplay.Text = "0";
            mineDisplay.Text = "10";

            SetDifficutly(Difficulty.Hard);

            timeElapsed = 0;

            random = new Random();
            timer.Tick += Timer_Tick;
            board = new Board(width, height, mineCount, random);

            CreateMineField();

            timer.Interval = 1000;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            timeDisplay.Text = timeElapsed.ToString();
        }

        public void NewGame()
        {

        }

        public void GameOver()
        {

        }

        public void SetDifficutly(Difficulty diff)
        {
            difficulty = diff;
            switch(diff)
            {
                case Difficulty.Easy:
                    mineCount = 10;
                    width = height = 8;
                    window.Height = 300;
                    window.Width = 150;
                    break;
                case Difficulty.Medium:
                    mineCount = 40;
                    width = height = 16;
                    window.Height = 400;
                    window.Width = 300;
                    break;
                case Difficulty.Hard:
                    mineCount = 99;
                    width = height = 24;
                    window.Height = 400;
                    window.Width = 600;
                    break;
            }
        }

        public void CreateMineField()
        {
            //create width
            mineField.ColumnCount = width;
            for(int i = 0; i < width; i++)
            {
                mineField.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            }
            //create height
            mineField.RowCount = height;
            for (int i = 0; i < height; i++)
            {
                mineField.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            board.DrawBoard(mineField);
        }

    }
}
