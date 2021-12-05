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

            var stage = new Level0();

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
                        stage.Fire.Set();
                        break;
                    case Keys.Left:
                        stage.MoveLeft.Set();
                        break;
                    case Keys.Right:
                        stage.MoveRight.Set();
                        break;
                    case Keys.Up:
                        stage.MoveUp.Set();
                        break;
                    case Keys.Down:
                        stage.MoveDown.Set();
                        break;
                    default:
                        break;
                }
            
            };
            form.KeyUp += (s, e) => {

                switch (e.KeyCode)
                {
                    case Keys.Space:
                        stage.Fire.Reset();
                        break;
                    case Keys.Left:
                        stage.MoveLeft.Reset();
                        break;
                    case Keys.Right:
                        stage.MoveRight.Reset();
                        break;
                    case Keys.Up:
                        stage.MoveUp.Reset();
                        break;
                    case Keys.Down:
                        stage.MoveDown.Reset();
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

                form.Render(stage);
                stage.Update();

                var stop = DateTime.Now;
                form.FrameRate = (int)Math.Round(1000.0 / (stop - start).TotalMilliseconds, 0);

            };
            timer.Start();

            Application.Run(form);
        }
    }
}
