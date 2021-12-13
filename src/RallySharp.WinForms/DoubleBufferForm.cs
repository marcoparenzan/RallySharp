using System;
using System.Drawing;
using System.Windows.Forms;

namespace RallySharp.WinForms
{
    public partial class DoubleBufferForm : Form
    {
        public DoubleBufferForm((int x, int y) padding)
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.padding = padding;
        }

        BufferedGraphics bufferedGraphics;
        private (int x, int y) padding;

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
                    new Rectangle(padding.x, padding.y, this.ClientRectangle.Width - padding.x * 2, this.ClientRectangle.Height - padding.y * 2)
                );
                this.Suspended = false;
            }
            else
            {
                this.bufferedGraphics.Render(e.Graphics);
            }
        }

        public Graphics CurrentGraphics => this.bufferedGraphics.Graphics;
    }
}
