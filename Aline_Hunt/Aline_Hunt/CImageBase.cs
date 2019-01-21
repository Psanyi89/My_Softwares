using System;
using System.Drawing;

namespace Aline_Hunt
{
    internal class CImageBase : IDisposable
    {
         bool diposed = false;
         Bitmap _bitmap;
        private int X;
        private int Y;
        public int Left { get { return X; } set { X = value; } }
        public int Top { get { return Y; } set { Y = value; } }
    
        public CImageBase(Bitmap _resource)
        {
            _bitmap = new Bitmap(_resource);
        }
        public void DrawImage(Graphics gfx)
        {
            gfx.DrawImage(_bitmap, X, Y);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (diposed)
            {
                return;
            }
            if (disposing)
            {
                _bitmap.Dispose();
            }
            diposed = true;
        }
    }
}
