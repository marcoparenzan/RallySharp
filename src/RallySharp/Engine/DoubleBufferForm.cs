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
            this.ClientSize = new Size((int)((Resources.Width) * Resources.TileWidth), (int)((Resources.Height) * Resources.TileHeight));
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
                    new Rectangle(Resources.TileWidth, Resources.TileHeight, this.ClientRectangle.Width - Resources.TileWidth * 2, this.ClientRectangle.Height - Resources.TileHeight * 2)
                );
                this.Suspended = false;
            }
            else
            {
                this.bufferedGraphics.Render(e.Graphics);
            }
        }

        public void Render(Level0 stage, Sprite focus)
        {
            bufferedGraphics.Graphics.Clear(Color.Black);

            //
            //
            //

            var focus_x = focus.Pos.X - (int)(this.ClientRectangle.Width / 2); if (focus_x < Resources.TileWidth) focus_x = Resources.TileWidth;
            var focus_y = focus.Pos.Y - (int)(this.ClientRectangle.Height / 2); if (focus_y < Resources.TileHeight) focus_y = Resources.TileHeight;

            //
            // render world
            //

            // how many rects in the viewport
            var viewport_height = (int)(this.ClientRectangle.Height / Resources.TileHeight);
            var viewport_width = (int)(this.ClientRectangle.Width / Resources.TileWidth);

            // the x,y converted to offset in the map
            var offset_y = (int)(Math.Max(focus_y, 0) / Resources.TileHeight); 
            var ym = focus_y % Resources.TileHeight;
            if (ym > 0) { offset_y++; ym = Resources.TileHeight - ym; }

            var offset_x = (int)(Math.Max(focus_x, 0) / Resources.TileWidth); 
            var xm = focus_x % Resources.TileWidth;
            if (xm > 0) { offset_x++; xm = Resources.TileWidth - xm; }

            // render

            var selected_tilesheet = Resources.Tiles[stage.Index];
            var selected_tileset = Resources.TileSet[stage.Index];
            var selected_tilerects = Resources.TileSetRectCache[stage.Index];

            var offset_i = (int)(offset_y * Resources.Width + offset_x);
            var yp = ym;
            for (var y = offset_y; y < Math.Min(offset_y + viewport_height, Resources.Height); y++)
            {
                var offset_row = offset_i;
                var xp = xm;
                for (var x = offset_x; x < Math.Min(offset_x + viewport_width, Resources.Width); x++)
                {
                    var tileId = selected_tilesheet[offset_row++] - 1;
                    var rect = new RectangleF(xp, yp, Resources.TileWidth, Resources.TileHeight);
                    bufferedGraphics.Graphics.DrawImage(selected_tileset, rect, selected_tilerects[tileId], GraphicsUnit.Pixel);
                    // bufferedGraphics.Graphics.FillRectangle(tileId == 1 ? Brushes.Blue : Brushes.Black, rect);
                    xp += Resources.TileWidth;
                }
                offset_i += (int)Resources.Width;
                yp += Resources.TileHeight;
            }

            //
            // render sprites
            //

            var selected_spritesheet = Resources.SpriteSheet[stage.Index];
            var selected_spriterects = Resources.SpriteSheetRectCache[stage.Index];

            foreach (var sprite in stage.Sprites)
            {
                var frame = selected_spriterects[sprite.Animation.CurrentFrame];
                var projection = new RectangleF(sprite.Pos.X - focus_x, sprite.Pos.Y - focus_y, frame.Width - 1, frame.Height - 1);

                // check if enemy is visible
                if (projection.X < 0) continue;
                if (projection.Y < 0) continue;
                if (projection.X > this.ClientRectangle.Width - Resources.TileWidth) continue;
                if (projection.Y > this.ClientRectangle.Height - Resources.TileHeight) continue;

                bufferedGraphics.Graphics.DrawImage(selected_spritesheet, projection, frame, GraphicsUnit.Pixel);
                // bufferedGraphics.Graphics.FillRectangle(Brushes.Red, projection);
                bufferedGraphics.Graphics.DrawRectangle(Pens.White, projection.X, projection.Y, projection.Width - 1, projection.Height - 1);

                //Debug.WriteLine($"{sprite.Id}//{projection}//{sprite.Direction}//({focus_x},{focus_y})");
            }

            ///
            ///
            ///
            bufferedGraphics.Graphics.DrawString($"Current={stage.MainSprite.Pos} Direction={stage.MainSprite.Direction} Animation={stage.MainSprite.Animation}", this.Font, Brushes.White, 32, 32);

            this.Invalidate();
        }
    }
}
