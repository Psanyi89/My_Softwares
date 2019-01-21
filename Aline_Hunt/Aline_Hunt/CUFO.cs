using Aline_Hunt.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aline_Hunt
{
    class CUFO :CImageBase
    {
        private Rectangle _ufoHotSpot = new Rectangle();
        public CUFO()
            :base(Resources.UFO)
        {
            _ufoHotSpot.X = Left + 20;
            _ufoHotSpot.Y = Top - 1;
            _ufoHotSpot.Width = 75;
            _ufoHotSpot.Height = 56;
        }
        public void Update(int X,int Y)
        {
            Left = X;
            Top = Y;
            _ufoHotSpot.X = Left + 20;
            _ufoHotSpot.Y = Top - 1;
        }
        public bool Hit(int X,int Y)
        {
            Rectangle c = new Rectangle(X, Y, 1, 1); // create a cursor rect quick way to check for hits.
            if(_ufoHotSpot.Contains(c))
            {
                return true;
            }
            return false;
        }
    }
}
