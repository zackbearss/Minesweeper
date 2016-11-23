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
        Timer timer;
        Random random;
        int width;
        int height;
        int mineCount;
        int timeElapsed;

        public Game()
        {
            SetDifficutly(Difficulty.Easy);

            timeDisplay.Text = "0";
            mineDisplay.Text = "10";
            random = new Random();
            timer.Tick += Timer_Tick;
            board = new Board(width, height, mineCount, random);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
                    break;
                case Difficulty.Medium:
                    mineCount = 40;
                    width = height = 16;
                    break;
                case Difficulty.Hard:
                    mineCount = 99;
                    width = height = 24;
                    break;
            }
        }

    }
}
