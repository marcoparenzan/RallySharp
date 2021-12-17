using RallySharp.Stages;

namespace RallySharp.Models
{
    public class MainSprite: Sprite
    {
        public CarAnimation Animation { get; set; }

        public override int CurrentFrame => Animation.CurrentFrame;

        byte direction;

        byte? newDirection;

        public void NewDirection(byte? value = null) => newDirection = value;

        protected override void UpdateRunning()
        {
            if (newDirection.HasValue && newDirection.Value != direction)
            {
                if (Pos.X % 24 != 0 || Pos.Y % 24 != 0)
                {
                    newDirection = default;
                }
            }

            newDirection ??= direction;
            byte rotation = 0;


            while (true)
            {
                var xt = 0;
                var yt = 0;

                var nextPos = Pos + Vec.Speed[newDirection.Value];
                if (newDirection == 0)
                {
                    yt = (int)((nextPos.Y) / Tilesheet.Height);
                    xt = (int)((nextPos.X) / Tilesheet.Width);
                }
                else if (newDirection == 1)
                {
                    yt = (int)((nextPos.Y) / Tilesheet.Height);
                    xt = (int)((nextPos.X + Tilesheet.Width - 1) / Tilesheet.Width);
                }
                else if (newDirection == 2)
                {
                    yt = (int)((nextPos.Y + Tilesheet.Height - 1) / Tilesheet.Height);
                    xt = (int)((nextPos.X) / Tilesheet.Width);
                }
                else if (newDirection == 3)
                {
                    yt = (int)((nextPos.Y) / Tilesheet.Height);
                    xt = (int)((nextPos.X) / Tilesheet.Width);
                }

                var offset = (int)(yt * Tilemap.Width + xt);
                var tileId = Tilemap.Data[offset];

                if (tileId != 2)
                {
                    newDirection = (byte) ((newDirection + (++rotation)) % 4); // increased rotation to make 90/180/270
                }
                else
                {
                    if (newDirection == direction)
                    {
                        Animation.Update();
                    }
                    else
                    {
                        Animation.NewDirection(newDirection.Value, (rotation == 2) ? -1 : 1);
                    }
                    Pos = nextPos;
                    direction = newDirection.Value;
                    newDirection = null; // reset so does not loop
                    break;
                }
            }
        }

        protected override void UpdateCrashed()
        {
            if (Animation.CurrentFrame != 48) Animation.Crashed();
        }

        protected override void UpdateEnded()
        {
            if (Animation.CurrentFrame != 48) Animation.Crashed();
        }

        public bool CollidesWith(Sprite that)
        {
            var dx = Math.Abs(that.Pos.X - this.Pos.X);
            var dy = Math.Abs(that.Pos.Y - this.Pos.Y);
            return (dx < Tilesheet.Width) && (dy < Tilesheet.Height);
        }
    }
}
