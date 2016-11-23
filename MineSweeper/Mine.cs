using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeper
{
    public sealed class Mine
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(obj, this))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            Mine mine = obj as Mine;
            return this.X == mine.X && this.Y == mine.Y;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode();
        }

        public static bool operator ==(Mine x, Mine y)
        {
            return object.Equals(x, y);
        }

        public static bool operator !=(Mine x, Mine y)
        {
            return !object.Equals(x, y);
        }

        public override string ToString()
        {
            return "X = " + X + " Y = " + Y;
        }
    }
}
