using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsPlayfield
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var stage = new Stage();

            var form = new DoubleBufferForm((16,16));
            //form.FormBorderStyle = FormBorderStyle.None;
            //form.WindowState = FormWindowState.Maximized;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ClientSize = new Size(640, 960);

            form.KeyDown += (s, e) => {

                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        form.Close();
                        break;
                    case Keys.Space:
                        break;
                    case Keys.Left:
                        break;
                    case Keys.Right:
                        break;
                    case Keys.Up:
                        break;
                    case Keys.Down:
                        break;
                    default:
                        break;
                }
            
            };
            form.KeyUp += (s, e) => {

                switch (e.KeyCode)
                {
                    case Keys.Space:
                        break;
                    case Keys.Left:
                        break;
                    case Keys.Right:
                        break;
                    case Keys.Up:
                        break;
                    case Keys.Down:
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

                RenderFrame();
                form.Invalidate();
                stage.Update();

                var stop = DateTime.Now;
                form.FrameRate = (int)Math.Round(1000.0 / (stop - start).TotalMilliseconds, 0);

            };
            timer.Start();

            Application.Run(form);


            void RenderFrame()
            {
                form.CurrentGraphics.Clear(Color.Black);

                //
                //
                //
                form.CurrentGraphics.FillEllipse(Brushes.Red, 320 + stage.X - 10, 320 - 10, 20, 20);

                //
                //
                //

                form.CurrentGraphics.DrawString($"Frames={form.FrameRate}", form.Font, Brushes.White, 32, 32);
            }
        }
    }
}
