using RallySharp.Levels;
using RallySharp.Models;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace RallySharp.Engine
{
    public partial class DoubleBufferForm : Form
    {
        public DoubleBufferForm()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        BufferedGraphics bufferedGraphics;

        public DoubleBufferForm Initialize()
        {
            this.ClientSize = new Size((int)((Tilemap.Width) * Tilesheet.Width), (int)((Tilemap.Height) * Tilesheet.Height));
            return this;
        }

        public bool Suspended { get; set; } = true;
        public int FrameRate { get; set; }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);

            if (this.bufferedGraphics != null)
            {
                this.Suspended = true;
                this.bufferedGraphics.Dispose();
                this.bufferedGraphics = null;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.bufferedGraphics == null)
            {
                this.bufferedGraphics = BufferedGraphicsManager.Current.Allocate(e.Graphics,
                    new Rectangle(Tilesheet.Width, Tilesheet.Height, this.ClientRectangle.Width - Tilesheet.Width * 2, this.ClientRectangle.Height - Tilesheet.Height * 2)
                );
                this.Suspended = false;
            }
            else
            {
                this.bufferedGraphics.Render(e.Graphics);
            }
        }

        public void Render(Level0 level, Sprite focus)
        {
            bufferedGraphics.Graphics.Clear(Color.Black);

            //
            //
            //

            var focus_x = focus.Pos.X - (int)(this.ClientRectangle.Width / 2); if (focus_x < Tilesheet.Width) focus_x = Tilesheet.Width;
            var focus_y = focus.Pos.Y - (int)(this.ClientRectangle.Height / 2); if (focus_y < Tilesheet.Height) focus_y = Tilesheet.Height;

            //
            // render world
            //

            // how many rects in the viewport
            var viewport_height = (int)(this.ClientRectangle.Height / Tilesheet.Height);
            var viewport_width = (int)(this.ClientRectangle.Width / Tilesheet.Width);

            // the x,y converted to offset in the map
            var offset_y = (int)(Math.Max(focus_y, 0) / Tilesheet.Height); 
            var ym = focus_y % Tilesheet.Height;
            if (ym > 0) { offset_y++; ym = Tilesheet.Height - ym; }

            var offset_x = (int)(Math.Max(focus_x, 0) / Tilesheet.Width); 
            var xm = focus_x % Tilesheet.Width;
            if (xm > 0) { offset_x++; xm = Tilesheet.Width - xm; }

            // render

            var selected_tilesheet = Tilemap.Data[level.Index];
            var selected_tileset = Tilesheet.Bitmaps[level.Index];
            var selected_tilerects = Tilesheet.Rects[level.Index];

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
                    bufferedGraphics.Graphics.DrawImage(selected_tileset, rect, selected_tilerects[tileId], GraphicsUnit.Pixel);
                    // bufferedGraphics.Graphics.FillRectangle(tileId == 1 ? Brushes.Blue : Brushes.Black, rect);
                    xp += Tilesheet.Width;
                }
                offset_i += (int)Tilemap.Width;
                yp += Tilesheet.Height;
            }

            //
            // render sprites
            //

            var bitmap = Spritesheet.Bitmaps[level.Index];
            var rects = Spritesheet.Rects[level.Index];

            foreach (var sprite in level.Sprites)
            {
                var projection = new RectangleF(sprite.Pos.X - focus_x, sprite.Pos.Y - focus_y, Tilesheet.Width, Tilesheet.Height);

                // check if enemy is visible
                if (projection.X < 0) continue;
                if (projection.Y < 0) continue;
                if (projection.X > this.ClientRectangle.Width - Tilesheet.Width) continue;
                if (projection.Y > this.ClientRectangle.Height - Tilesheet.Height) continue;

                bufferedGraphics.Graphics.DrawImage(bitmap, projection, rects[sprite.CurrentFrame], GraphicsUnit.Pixel);
                // bufferedGraphics.Graphics.FillRectangle(Brushes.Red, projection);
                //bufferedGraphics.Graphics.DrawRectangle(Pens.White, projection.X, projection.Y, projection.Width - 1, projection.Height - 1);

                //Debug.WriteLine($"{sprite.Id}//{projection}//{sprite.Direction}//({focus_x},{focus_y})");
            }

            ///
            ///
            ///
            bufferedGraphics.Graphics.DrawString($"Lives={level.GameState.Lives} Score={level.GameState.Score} FlagScore={level.GameState.FlagScore} Fuel={level.GameState.Fuel} Current={level.MainSprite.Pos} Animation={level.MainSprite.CurrentFrame}", this.Font, Brushes.White, 32, 32);

            this.Invalidate();
        }
    }
}
