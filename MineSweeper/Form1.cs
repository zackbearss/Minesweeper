using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class Form1 : Form
    {
        Game game;
        public Form1()
        {
            InitializeComponent();

            //Type tp = minesLabel.GetType().BaseType;
            //System.Reflection.PropertyInfo pi = tp.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            //pi.SetValue(minesLabel, true, null);

            game = new Game(this, timerLabel, minesLabel, tableLayoutPanelMineField);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.NewGame();
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.SetDifficutly(Game.Difficulty.Easy);
            game.NewGame();
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.SetDifficutly(Game.Difficulty.Medium);
            game.NewGame();
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.SetDifficutly(Game.Difficulty.Hard);
            game.NewGame();
        }
    }
}
