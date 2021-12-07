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

            var focus_x = focus.Pos.X - (int)(this.ClientRectangle.Width / 2); if (focus_x < 0) focus_x = 0;
            var focus_y = focus.Pos.Y - (int)(this.ClientRectangle.Height / 2); if (focus_y < 0) focus_y = 0;

            //
            // render world
            //

            // how many rects in the viewport
            var viewport_height = (int)(this.ClientRectangle.Height / Resources.TileHeight);
            var viewport_width = (int)(this.ClientRectangle.Width / Resources.TileWidth);

            // the x,y converted to offset in the map
            var offset_y = (int)(Math.Max(focus_y, 0) / Resources.TileHeight); var ym = focus_y % Resources.TileHeight;
            if (ym > 0) { offset_y++; ym = Resources.TileHeight - ym; }
            var offset_x = (int)(Math.Max(focus_x, 0) / Resources.TileWidth); var xm = focus_x % Resources.TileWidth;
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
                try
                {
                    var frame = selected_spriterects[sprite.Animation.CurrentFrame];
                    var projection = new RectangleF(sprite.Pos.X - focus_x, sprite.Pos.Y - focus_y, frame.Width - 1, frame.Height - 1);

                    // check if enemy is visible
                    if (projection.X < 0) return;
                    if (projection.Y < 0) return;
                    if (projection.X + 24 > this.ClientRectangle.Width) return;
                    if (projection.Y + 24 > this.ClientRectangle.Height) return;

                    // yes it is
                    //bufferedGraphics.Graphics.DrawImage(selected_spritesheet, projection, frame, GraphicsUnit.Pixel);
                    //bufferedGraphics.Graphics.DrawRectangle(Pens.White, projection.X, projection.Y, projection.Width - 1, projection.Height - 1);
                    Console.WriteLine(projection);
                }
                catch (Exception ex)
                { 
                }
            }

            ///
            ///
            ///
            bufferedGraphics.Graphics.DrawString($"Current={stage.MainSprite.Pos} Direction={stage.MainSprite.Direction} Animation={stage.MainSprite.Animation}", this.Font, Brushes.White, 32, 32);

            this.Invalidate();
        }
    }
}
