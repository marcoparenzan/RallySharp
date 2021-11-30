using RallySharp.Levels;
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

        public DoubleBufferForm Initialize(Level0 stage)
        {
            this.stage = stage;
            this.ClientSize = new Size((int) ((Resources.Width + 1) * Resources.TileWidth), (int) ((Resources.Height + 1) * Resources.TileHeight));
            return this;
        }

        private Level0 stage;

        public bool Suspended { get;  set; } = true;
        public int FrameRate { get; set; }

        public ButtonTrigger Fire { get; } = new ButtonTrigger();
        public ButtonTrigger MoveLeft { get; } = new ButtonTrigger();
        public ButtonTrigger MoveRight { get; } = new ButtonTrigger();
        public ButtonTrigger MoveUp { get; } = new ButtonTrigger();
        public ButtonTrigger MoveDown { get; } = new ButtonTrigger();

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

        public void FrameUpdate()
        {
            if (MoveLeft.Triggered())
            {
                stage.MoveLeft();
            }
            if (MoveRight.Triggered())
            {
                stage.MoveRight();
            }
            if (MoveUp.Triggered())
            {
                stage.MoveUp();
            }
            if (MoveDown.Triggered())
            {
                stage.MoveDown();
            }
            if (Fire.Triggered())
            {
                stage.Fire();
            }

            stage.Update();
        }

        public void FrameRender()
        {
            bufferedGraphics.Graphics.Clear(Color.Black);
            Render(stage, bufferedGraphics.Graphics);
            //bufferedGraphics.Graphics.DrawString($"FrameRate={FrameRate} View=({view.Pos.x},{view.Pos.y}) Car=({stage.Current.x},{stage.Current.y})", this.Font, Brushes.White, 32, 32);
            bufferedGraphics.Graphics.DrawString($"Current={stage.MainCar.Pos}", this.Font, Brushes.White, 32, 32);
            this.Invalidate();
        }

        void Render(Level0 stage, Graphics g)
        {
            var xx = stage.MainCar.Pos.x - (int)(this.ClientRectangle.Width / 2); if (xx < 0) xx = 0;
            var yy = stage.MainCar.Pos.y - (int)(this.ClientRectangle.Height / 2); if (yy < 0) yy = 0;
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
                    g.DrawImage(Resources.TileSet[0], rect, Resources.TileSetRectCache[0][tileId], GraphicsUnit.Pixel);
                    xp += Resources.TileWidth;
                }
                offset += (int)Resources.Width;
                yp += Resources.TileHeight;
            }

            //
            // render sprites
            //

            var f = Resources.SpriteSheetRectCache[0][stage.MainCar.Type];
            var mainProj = new RectangleF(stage.MainCar.Pos.x - pos.x - (f.Width / 2), stage.MainCar.Pos.y - pos.y - (f.Height / 2), f.Width, f.Height);
            g.DrawImage(Resources.SpriteSheet[0], mainProj, Resources.SpriteSheetRectCache[0][stage.MainCar.Type], GraphicsUnit.Pixel);

            foreach (var enemySprite in stage.Enemies)
            {
                f = Resources.SpriteSheetRectCache[0][enemySprite.Type];
                var enemyProj = new RectangleF(enemySprite.Pos.x - pos.x - (f.Width / 2), enemySprite.Pos.y - pos.y - (f.Height / 2), f.Width, f.Height);

                // check if enemy is visible
                if (enemyProj.X < 0) continue;
                if (enemyProj.Y < 0) continue;
                if (enemyProj.X > this.ClientRectangle.Width) continue;
                if (enemyProj.Y > this.ClientRectangle.Height) continue;

                // yes it is
                g.DrawImage(Resources.SpriteSheet[0], enemyProj, Resources.SpriteSheetRectCache[0][enemySprite.Type], GraphicsUnit.Pixel);
            }
        }
    }
}
