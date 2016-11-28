using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeper
{
    class Tile
    {
		internal Action<Tile, MouseButtons> ClickEvent;

		public enum TileType
        {
            Empty,
            Mine,
        };
        
        public enum TileStatus
        {
            Unpressed,
            Pressed,
            Flagged
        };

        public TileType Type { get; set; }
        public TileStatus Status { get; private set; }
		public Button TileButton { get; private set; }
		public Label TileLabel { get; private set; }
        //PictureBox picture;

        public Tile()
        {
            Status = TileStatus.Unpressed;
			TileButton = new Button();
			TileLabel = new Label();

			TileLabel.Visible = false;
			TileLabel.Width = 20;
			TileLabel.Height = 20;
			TileLabel.Dock = DockStyle.Fill;

			TileButton.Width = 20;
			TileButton.Height = 20;
			TileButton.Margin = new Padding(-10, -10, -10, -10);
			TileButton.Dock = DockStyle.Fill;
			TileButton.MouseClick += TileButton_MouseClick;
		}

		private void TileButton_MouseClick(object sender, MouseEventArgs e)
		{
			ClickEvent(this, e.Button);
		}

		public int Update(TileStatus newStatus, int surrondingMinesCount)
        {
            Status = newStatus;
            
            switch(Status)
            {
                case TileStatus.Unpressed:
                    //display regular look
                    return 0;
                case TileStatus.Pressed:
                    if(Type == TileType.Empty)
                    {
                        //update number on tile with surrongMinesCount
                        return 0;
                    }
                    else
                    {
                        //update mine picture
                        return 1;
                    }
                case TileStatus.Flagged:
                    //display flag on picturebox
                    return 0;
                default:
                    return 0;
            }
        }

		public void DisplayNumber(int number)
		{
			switch (number)
			{
				case 0:
					break;
				case 1:
					TileLabel.ForeColor = System.Drawing.Color.Blue;
					break;
				case 2:
					TileLabel.ForeColor = System.Drawing.Color.Green;
					break;
				case 3:
					TileLabel.ForeColor = System.Drawing.Color.Red;
					break;
				case 4:
					TileLabel.ForeColor = System.Drawing.Color.Navy;
					break;
				case 5:
					TileLabel.ForeColor = System.Drawing.Color.Maroon;
					break;
				case 6:
					TileLabel.ForeColor = System.Drawing.Color.Aqua;
					break;
				case 7:
					TileLabel.ForeColor = System.Drawing.Color.Purple;
					break;
				case 8:
					TileLabel.ForeColor = System.Drawing.Color.Gray;
					break;
			}
			TileButton.Visible = false;
			TileLabel.Visible = true;
			TileLabel.Text = number.ToString();
		}

		public void DisplayFlag()
		{

		}
	}
}
