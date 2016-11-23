using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeper
{
    class Tile : Button
    {
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
        //PictureBox picture;

        public Tile()
        {
            Status = TileStatus.Unpressed;
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
    }
}
