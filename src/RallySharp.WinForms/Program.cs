using RallySharp.Levels;
using RallySharp.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RallySharp.WinForms
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

            var selected_tilesheet = Tilemap.Data[level.Index];
            var selected_tileset = Tilesheet.Bitmaps[level.Index];
            var selected_tilerects = Tilesheet.Rects[level.Index];

            var bitmap = Spritesheet.Bitmaps[level.Index];
            var rects = Spritesheet.Rects[level.Index];

            var form = new DoubleBufferForm((Tilesheet.Width, Tilesheet.Height));
            //form.FormBorderStyle = FormBorderStyle.None;
            //form.WindowState = FormWindowState.Maximized;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ClientSize = new Size(960, 640);
            //form.ClientSize = new Size((int)((Tilemap.Width) * Tilesheet.Width), (int)((Tilemap.Height) * Tilesheet.Height));

            void Render()
            {
                form.CurrentGraphics.Clear(Color.Black);

                //
                //
                //

                var focus_x = level.MainSprite.Pos.X - (int)(form.ClientRectangle.Width / 2); if (focus_x < Tilesheet.Width) focus_x = Tilesheet.Width;
                var focus_y = level.MainSprite.Pos.Y - (int)(form.ClientRectangle.Height / 2); if (focus_y < Tilesheet.Height) focus_y = Tilesheet.Height;

                //
                // render world
                //

                // how many rects in the viewport
                var viewport_height = (int)(form.ClientRectangle.Height / Tilesheet.Height);
                var viewport_width = (int)(form.ClientRectangle.Width / Tilesheet.Width);

                // the x,y converted to offset in the map
                var offset_y = (int)(Math.Max(focus_y, 0) / Tilesheet.Height);
                var ym = focus_y % Tilesheet.Height;
                if (ym > 0) { offset_y++; ym = Tilesheet.Height - ym; }

                var offset_x = (int)(Math.Max(focus_x, 0) / Tilesheet.Width);
                var xm = focus_x % Tilesheet.Width;
                if (xm > 0) { offset_x++; xm = Tilesheet.Width - xm; }

                // render

                var offset_i = (int)(offset_y * Tilemap.Width + offset_x);
                var yp = ym;
                for (var y = offset_y; y < Math.Min(offset_y + viewport_height, Tilemap.Height); y++)
                {
                    var offset_row = offset_i;
                    var xp = xm;
                    for (var x = offset_x; x < Math.Min(offset_x + viewport_width, Tilemap.Width); x++)
                    {
                        var tileId = selected_tilesheet[offset_row++] - 1;
                        var rect = new RectangleF(xp, yp, Tilesheet.Width, Tilesheet.Height);
                        form.CurrentGraphics.DrawImage(selected_tileset, rect, selected_tilerects[tileId], GraphicsUnit.Pixel);
                        // bufferedGraphics.Graphics.FillRectangle(tileId == 1 ? Brushes.Blue : Brushes.Black, rect);
                        xp += Tilesheet.Width;
                    }
                    offset_i += (int)Tilemap.Width;
                    yp += Tilesheet.Height;
                }

                //
                // render sprites
                //

                foreach (var sprite in level.Sprites)
                {
                    var projection = new RectangleF(sprite.Pos.X - focus_x, sprite.Pos.Y - focus_y, Tilesheet.Width, Tilesheet.Height);

                    // check if enemy is visible
                    if (projection.X < 0) continue;
                    if (projection.Y < 0) continue;
                    if (projection.X > form.ClientRectangle.Width - Tilesheet.Width) continue;
                    if (projection.Y > form.ClientRectangle.Height - Tilesheet.Height) continue;

                    form.CurrentGraphics.DrawImage(bitmap, projection, rects[sprite.CurrentFrame], GraphicsUnit.Pixel);
                    // bufferedGraphics.Graphics.FillRectangle(Brushes.Red, projection);
                    //bufferedGraphics.Graphics.DrawRectangle(Pens.White, projection.X, projection.Y, projection.Width - 1, projection.Height - 1);
                }

                ///
                ///
                ///
                form.CurrentGraphics.DrawString($"Lives={level.GameState.Lives} Score={level.GameState.Score} FlagScore={level.GameState.FlagScore} Fuel={level.GameState.Fuel} Current={level.MainSprite.Pos} Animation={level.MainSprite.CurrentFrame}", form.Font, Brushes.White, 32, 32);
            }

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

                Render();
                form.Invalidate();
                level.Update();

                var stop = DateTime.Now;
                form.FrameRate = (int)Math.Round(1000.0 / (stop - start).TotalMilliseconds, 0);

            };
            timer.Start();

            Application.Run(form);
        }

    }
}
