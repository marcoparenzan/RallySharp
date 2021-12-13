using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Models
{
    public struct Vec
    {
        public static Vec[] Speed { get; } = { (0, -6), (6, 0), (0, 6), (-6, 0) };

        public float X { get; set; }
        public float Y { get; set; }

        public static implicit operator Vec((float x, float y) v) => new() { X = v.x, Y = v.y };

        public void Deconstruct(out float x, out float y)
        {
            x = this.X;
            y = this.Y;
        }

        public static Vec operator +(Vec a, Vec b) => new() { X = a.X + b.X, Y = a.Y + b.Y };
        public static Vec operator +(Vec a, (float x, float y) b) => new() { X = a.X + b.x, Y = a.Y + b.y };
        public static Vec operator -(Vec a, Vec b) => new() { X = a.X - b.X, Y = a.Y - b.Y };
        public static Vec operator -(Vec a, (float x, float y) b) => new() { X = a.X - b.x, Y = a.Y - b.y };
        public static Vec operator *(Vec a, float b) => new() { X = a.X * b, Y = a.Y * b };
        public static Vec operator /(Vec a, float b) => new() { X = a.X / b, Y = a.Y / b };

        public void Add(Vec v)
        {
            X += v.X;
            Y += v.Y;
        }

        public void Add((float x, float y) v)
        {
            X += v.x;
            Y += v.y;
        }

        public void Sub(Vec v)
        {
            X -= v.X;
            Y -= v.Y;
        }

        public void Sub((float x, float y) v)
        {
            X -= v.x;
            Y -= v.y;
        }

        public static bool operator ==(Vec a, (float x, float y) b)
        {
            if (a.X != b.x) return false;
            if (a.Y != b.y) return false;
            return true;
        }

        public static bool operator !=(Vec a, (float x, float y) b)
        {
            if (a.X != b.x) return true;
            if (a.Y != b.y) return true;
            return false;
        }

        public static bool operator ==(Vec a, Vec b)
        {
            if (a.X != b.X) return false;
            if (a.Y != b.Y) return false;
            return true;
        }

        public static bool operator !=(Vec a, Vec b)
        {
            if (a.X != b.X) return true;
            if (a.Y != b.Y) return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Vec) return false;
            return this == (Vec)obj;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString() => $"({X},{Y})";
    }
}
