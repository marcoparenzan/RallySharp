using RallySharp.Engine;
using RallySharp.Levels;
using RallySharp.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RallySharp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var gameState = new GameState();
            var level = new Level0
            {
                GameState = gameState
            };

            var form =
                new DoubleBufferForm()
            ;
            form.Initialize();
            //form.FormBorderStyle = FormBorderStyle.None;
            //form.WindowState = FormWindowState.Maximized;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ClientSize = new Size(960, 640);
            form.KeyDown += (s, e) => {

                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        form.Close();
                        break;
                    case Keys.Space:
                        level.Fire.Set();
                        break;
                    case Keys.Left:
                        level.MoveLeft.Set();
                        break;
                    case Keys.Right:
                        level.MoveRight.Set();
                        break;
                    case Keys.Up:
                        level.MoveUp.Set();
                        break;
                    case Keys.Down:
                        level.MoveDown.Set();
                        break;
                    default:
                        break;
                }
            
            };
            form.KeyUp += (s, e) => {

                switch (e.KeyCode)
                {
                    case Keys.Space:
                        level.Fire.Reset();
                        break;
                    case Keys.Left:
                        level.MoveLeft.Reset();
                        break;
                    case Keys.Right:
                        level.MoveRight.Reset();
                        break;
                    case Keys.Up:
                        level.MoveUp.Reset();
                        break;
                    case Keys.Down:
                        level.MoveDown.Reset();
                        break;
                    default:
                        break;
                }

            };
            form.Show();

            var refrate = (int) Math.Round(1000.0 / 30, 0);

            Timer timer = new();
            timer.Interval = refrate;
            timer.Tick += (s, e) =>
            {
                var start = DateTime.Now;

                form.Render(level, level.MainSprite);
                level.Update();

                var stop = DateTime.Now;
                form.FrameRate = (int)Math.Round(1000.0 / (stop - start).TotalMilliseconds, 0);

            };
            timer.Start();

            Application.Run(form);
        }
    }
}
