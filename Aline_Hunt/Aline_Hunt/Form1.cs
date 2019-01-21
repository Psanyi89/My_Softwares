//#define My_Debug

using Aline_Hunt.Properties;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Aline_Hunt
{
    public partial class Alienhunt : Form
    {
        private int FrameNum = 8;
        private const int ExpNum = 3;
        private bool hit = false;
        private string _skill = "Novice";
        private int GameFrame = 0;
        private int exptime = 0;
        private int _hits = 0;
        private int _misses = 0;
        private int _totalShots = 0;
        private double _averageHits = 0;
#if My_Debug
        int _cursX = 0;
        int _cursY = 0;
#endif
        private CUFO _ufo;
        private Explosion _exp;
        private Menu _menu;
        private ScoreBoard _scoreboard;
        private Random rnd = new Random();
        public Alienhunt()
        {
            InitializeComponent();
            #region Create Custom Cursor
            Bitmap bitmap = new Bitmap(Resources.target_sight);
            Cursor = CustomCursor.CreateCursor(bitmap, bitmap.Height / 2, bitmap.Width / 2);
            #endregion
            #region Create UFO
            _ufo = new CUFO() { Left = 10, Top = 500 };
            #endregion
            #region Create Menu
            _menu = new Menu() { Left = 550, Top = 10 };

            #endregion
            #region Create ScoreBoard
            _scoreboard = new ScoreBoard() { Left = 10, Top = 10 };

            #endregion
            #region Create Explosion
            _exp = new Explosion();
            #endregion

        }

        private void timerGameLoop_Tick(object sender, EventArgs e)
        {

            if (GameFrame >= FrameNum)
            {
                UpdateUFO();
                GameFrame = 0;

            }
            if (hit)
            {
                if (exptime >= ExpNum)
                {
                    hit = false;
                    exptime = 0;
                    UpdateUFO();
                }
                exptime++;
            }
            GameFrame++;
            Refresh();
        }

        private void UpdateUFO()
        {
            _ufo.Update(
                rnd.Next(Resources.UFO.Width, Width - Resources.UFO.Width),
                rnd.Next(Height / 2, Height - Resources.UFO.Height * 2)
                );
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            _menu.DrawImage(dc);
            _scoreboard.DrawImage(dc);

            if (hit == true)
            {
                _exp.DrawImage(dc);
            }
            else
            {

                _ufo.DrawImage(dc);
            }

#if My_Debug
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis;
            Font _font = new Font("Stencil", 12, FontStyle.Regular);
            TextRenderer.DrawText(dc, $"X={_cursX.ToString()}:Y={_cursY.ToString()}",
                _font,new Rectangle(30,28,120,20),SystemColors.ControlLightLight,flags);
#endif

            #region Scores displayed
            TextFormatFlags flag = TextFormatFlags.Left;
            Font font = new Font("Stencil", 28, FontStyle.Regular);
            TextRenderer.DrawText(e.Graphics, $"Shots: {_totalShots}", font,
                new Rectangle(10, 30, 250, 40), SystemColors.ControlText, flag);
            TextRenderer.DrawText(e.Graphics, $"Hits: {_hits}", font,
                new Rectangle(10, 96, 250, 40), SystemColors.ControlText, flag);
            TextRenderer.DrawText(e.Graphics, $"Misses: {_misses}", font,
                new Rectangle(10, 138, 250, 40), SystemColors.ControlText, flag);
            TextRenderer.DrawText(e.Graphics, $"Avg: {_averageHits.ToString("F0")}%", font,
                new Rectangle(10, 180, 300, 50), SystemColors.ControlText, flag);
            TextRenderer.DrawText(e.Graphics, $"Skill: {_skill}", font,
               new Rectangle(5, 250, 325, 40), SystemColors.ControlText, flag);
            #endregion
            base.OnPaint(e);
        }

        private void Alienhunt_MouseMove(object sender, MouseEventArgs e)
        {
#if My_Debug
            _cursX = e.X;
            _cursY = e.Y;
#endif
            Refresh();
        }

        private void Alienhunt_MouseClick(object sender, MouseEventArgs e)
        {
            #region Start coordinates
            if (e.X > 580 && e.X < 821 && e.Y > 49 && e.Y < 111)
            {
                timerGameLoop.Start();

            }
            #endregion
            #region Stop coordinates
            else if (e.X > 599 && e.X < 801 && e.Y > 114 && e.Y < 161)
            {
                timerGameLoop.Stop();
            }
            #endregion
            #region Reset coordinates
            else if (e.X > 624 && e.X < 771 && e.Y > 169 && e.Y < 211)
            {
                timerGameLoop.Stop();
                _hits = 0;
                _misses = 0;
                _totalShots = 0;
                _averageHits = 0;
                _skill = "Novice";
            }
            #endregion
            #region Exit coordinates
            else if (e.X > 641 && e.X < 756 && e.Y > 220 && e.Y < 261)
            {
                timerGameLoop.Stop();
                Application.Exit();
            }
            #endregion
            #region Hit

            else
            {
                if (timerGameLoop.Enabled)
                {
                    _totalShots++;
                    if (_ufo.Hit(e.X, e.Y))
                    {
                        hit = true;
                        _exp.Left = _ufo.Left - Resources.explosion.Width / 3;
                        _exp.Top = _ufo.Top - Resources.explosion.Height / 3;
                        _hits++;
                        Boom();
                    }
                    _misses = _totalShots - _hits;
                    _averageHits = (double)_hits / _totalShots * 100.0;
                    if (_averageHits >= 99 && _totalShots > 50)
                    {
                        _skill = "PRO";
                        FrameNum = 5;
                    }
                    else if (_averageHits >= 75 && _averageHits < 100 && _totalShots > 50)
                    {
                        _skill = "Hunter";
                        FrameNum = 6;
                    }
                    else if (_averageHits < 75 && _averageHits >= 50 && _totalShots > 50)
                    {
                        _skill = "Warrior";
                        FrameNum = 7;
                    }
                    else if (_averageHits < 50 && _averageHits >= 25 && _totalShots > 50)
                    {
                        _skill = "Novice";
                        FrameNum = 8;
                    }
                    else if (_averageHits < 25 && _totalShots > 50)
                    {
                        _skill = "Noob";
                        FrameNum = 10;
                    }
                }
            }
            #endregion
            OpenFire();
        }
        private void OpenFire()
        {
            #region Laser sound
            SoundPlayer soundPlayer = new SoundPlayer(Resources.laser);
            soundPlayer.Play();
            #endregion

        }
        private void Boom()
        {
            #region Explosion sound
            SoundPlayer soundPlayer = new SoundPlayer(Resources.boom);
            soundPlayer.Play();
            #endregion

        }
    }
}
