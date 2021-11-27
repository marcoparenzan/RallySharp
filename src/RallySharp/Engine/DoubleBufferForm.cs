using RallySharp.Assets;
using RallySharp.Levels;
using RallySharp.Models;
using System;
using System.Collections.Generic;
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

        public DoubleBufferForm Initialize(Level1 stage, Level1Resources resources)
        {
            this.stage = stage;
            this.resources = resources;
            this.ClientSize = new Size((int) ((resources.Size.x + 1) * resources.TileSize.x), (int) ((resources.Size.y + 1) * resources.TileSize.y));
            this.Text = resources.Title;
            return this;
        }

        private Level1 stage;
        private Level1Resources resources;

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
                    //new Rectangle(resources.TileSize.x, resources.TileSize.y, this.ClientRectangle.Width - resources.TileSize.x * 2, this.ClientRectangle.Height - resources.TileSize.y * 2)
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

        void Render(Level1 stage, Graphics g)
        {
            var xx = stage.MainCar.Pos.x - (int)(this.ClientRectangle.Width / 2); if (xx < 0) xx = 0;
            var yy = stage.MainCar.Pos.y - (int)(this.ClientRectangle.Height / 2); if (yy < 0) yy = 0;
            var pos = (x: xx, y: yy);

            //
            // render world
            //

            // how many rects in the viewport
            var ys = (int)(this.ClientRectangle.Height / resources.TileSize.y);
            var xs = (int)(this.ClientRectangle.Width / resources.TileSize.x);

            // the x,y converted to offset in the map
            var yt = (int)(Math.Max(pos.y, 0) / resources.TileSize.y); var ym = pos.y % resources.TileSize.y;
            if (ym > 0) { yt++; ym = resources.TileSize.y - ym; }
            var xt = (int)(Math.Max(pos.x, 0) / resources.TileSize.x); var xm = pos.x % resources.TileSize.x;
            if (xm > 0) { xt++; xm = resources.TileSize.x - xm; }

            // render
            var offset = (int)(yt * resources.Size.x + xt);
            var yp = ym;
            for (var y = yt; y < Math.Min(yt + ys, resources.Size.y); y++)
            {
                var offset_row = offset;
                var xp = xm;
                for (var x = xt; x < Math.Min(xt + xs, resources.Size.x); x++)
                {
                    var tileId = resources.DefaultLayer[offset_row++] - 1;
                    var rect = new RectangleF(xp, yp, resources.TileSize.x, resources.TileSize.y);
                    g.DrawImage(resources.Set, rect, resources.TileRectCache[tileId], GraphicsUnit.Pixel);
                    xp += resources.TileSize.x;
                }
                offset += (int)resources.Size.x;
                yp += resources.TileSize.y;
            }

            //
            // render sprites
            //

            var f = resources.Frames[stage.MainCar.Type];
            var mainProj = new RectangleF(stage.MainCar.Pos.x - pos.x - (f.Width / 2), stage.MainCar.Pos.y - pos.y - (f.Height / 2), f.Width, f.Height);
            g.DrawImage(resources.SpriteSheet, mainProj, resources.Frames[stage.MainCar.Type], GraphicsUnit.Pixel);

            foreach (var enemySprite in stage.Enemies)
            {
                f = resources.Frames[enemySprite.Type];
                var enemyProj = new RectangleF(enemySprite.Pos.x - pos.x - (f.Width / 2), enemySprite.Pos.y - pos.y - (f.Height / 2), f.Width, f.Height);

                // check if enemy is visible
                if (enemyProj.X < 0) continue;
                if (enemyProj.Y < 0) continue;
                if (enemyProj.X > this.ClientRectangle.Width) continue;
                if (enemyProj.Y > this.ClientRectangle.Height) continue;

                // yes it is
                g.DrawImage(resources.SpriteSheet, enemyProj, resources.Frames[enemySprite.Type], GraphicsUnit.Pixel);
            }
        }
    }
}
