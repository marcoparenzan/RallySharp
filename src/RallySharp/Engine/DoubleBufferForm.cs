using RallySharp.Levels;
using RallySharp.Models;
using System;
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
            this.Font = new Font("C64 Pro Mono Normale", 12);
        }

        BufferedGraphics bufferedGraphics;

        public DoubleBufferForm Initialize()
        {
            this.ClientSize = new Size((int)((Resources.Width + 1) * Resources.TileWidth), (int)((Resources.Height + 1) * Resources.TileHeight));
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
                    //new Rectangle(Resources.TileWidth, Resources.TileHeight, this.ClientRectangle.Width - Resources.TileWidth * 2, this.ClientRectangle.Height - Resources.TileHeight * 2)
                    this.ClientRectangle
                );

                this.Suspended = false;
            }
            else
            {
                this.bufferedGraphics.Render(e.Graphics);
            }
        }

        public void Render(Level0 stage)
        {
            bufferedGraphics.Graphics.Clear(Color.Black);

            //
            //
            //

            var xx = stage.MainSprite.Pos.x - (int)(this.ClientRectangle.Width / 2); if (xx < 0) xx = 0;
            var yy = stage.MainSprite.Pos.y - (int)(this.ClientRectangle.Height / 2); if (yy < 0) yy = 0;
            var pos = (x: xx, y: yy);

            //
            // render world
            //

            // how many rects in the viewport
            var ys = (int)(this.ClientRectangle.Height / Resources.TileHeight);
            var xs = (int)(this.ClientRectangle.Width / Resources.TileWidth);

            // the x,y converted to offset in the map
            var yt = (int)(Math.Max(pos.y, 0) / Resources.TileHeight); var ym = pos.y % Resources.TileHeight;
            if (ym > 0) { yt++; ym = Resources.TileHeight - ym; }
            var xt = (int)(Math.Max(pos.x, 0) / Resources.TileWidth); var xm = pos.x % Resources.TileWidth;
            if (xm > 0) { xt++; xm = Resources.TileWidth - xm; }

            // render
            var offset = (int)(yt * Resources.Width + xt);
            var yp = ym;
            for (var y = yt; y < Math.Min(yt + ys, Resources.Height); y++)
            {
                var offset_row = offset;
                var xp = xm;
                for (var x = xt; x < Math.Min(xt + xs, Resources.Width); x++)
                {
                    var tileId = Resources.Tiles[0][offset_row++] - 1;
                    var rect = new RectangleF(xp, yp, Resources.TileWidth, Resources.TileHeight);
                    bufferedGraphics.Graphics.DrawImage(Resources.TileSet[0], rect, Resources.TileSetRectCache[0][tileId], GraphicsUnit.Pixel);
                    xp += Resources.TileWidth;
                }
                offset += (int)Resources.Width;
                yp += Resources.TileHeight;
            }

            //
            // render sprites
            //

            foreach (var sprite in stage.Sprites)
            {
                var f = Resources.SpriteSheetRectCache[0][sprite.CurrentAnimationFrame];
                var projection = new RectangleF(sprite.Pos.x - pos.x, sprite.Pos.y - pos.y, f.Width - 1, f.Height - 1);

                // check if enemy is visible
                if (projection.X < 0) return;
                if (projection.Y < 0) return;
                if (projection.X > this.ClientRectangle.Width) return;
                if (projection.Y > this.ClientRectangle.Height) return;

                // yes it is
                bufferedGraphics.Graphics.DrawImage(Resources.SpriteSheet[0], projection, Resources.SpriteSheetRectCache[0][sprite.CurrentAnimationFrame], GraphicsUnit.Pixel);
                bufferedGraphics.Graphics.DrawRectangle(sprite.Collided ? Pens.Yellow : Pens.White, projection.X, projection.Y, projection.Width - 1, projection.Height - 1);
            }

            ///
            ///
            ///
            bufferedGraphics.Graphics.DrawString($"Current={stage.MainSprite.Pos}", this.Font, Brushes.White, 32, 32);

            this.Invalidate();
        }
    }
}
