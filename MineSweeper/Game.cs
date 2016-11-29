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

            SetDifficutly(Difficulty.Easy);   

            random = new Random();
            NewGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            timeDisplay.Text = timeElapsed.ToString();
        }

        public void NewGame()
        {
            timer = new Timer();
            timeElapsed = 0;
            timeDisplay.Text = timeElapsed.ToString();

            board = new Board(width, height, mineCount, random);
            board.GameOver += GameOver;
            board.GameStarted += GameStarted;

            CreateMineField();

            timer.Tick += Timer_Tick;
            timer.Interval = 1000;
        }

        private void GameStarted()
        {
            timer.Start();
        }

        public void GameOver(bool DidUserWin)
        {
            timer.Stop();
            if (DidUserWin)
                MessageBox.Show("You won!", "Game Over");
            else
                MessageBox.Show("You lose!", "Game Over");
            NewGame();
        }

        public void SetDifficutly(Difficulty diff)
        {
            difficulty = diff;
            switch(diff)
            {
                case Difficulty.Easy:
                    mineCount = 10;
                    width = height = 9;
                    window.Height = 293;
                    window.Width = 203;
                    break;
                case Difficulty.Medium:
                    mineCount = 40;
                    width = height = 16;
                    window.Height = 465;
                    window.Width = 341;
                    break;
                case Difficulty.Hard:
                    mineCount = 99;
					width = 30;
					height = 16;
                    window.Height = 465;
                    window.Width = 621;
                    break;
            }
        }

        public void CreateMineField()
        {
            //clear everything that was there
            mineField.Controls.Clear();
            mineField.RowStyles.Clear();
            mineField.ColumnStyles.Clear();

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
