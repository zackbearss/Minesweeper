﻿using System;
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

            game = new Game(this, timerLabel, minesLabel, tableLayoutPanelMineField);
        }

    }
}
