using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Models
{
    public struct Vec
    {
        public float x { get; set; }
        public float y { get; set; }

        public static implicit operator Vec((float x, float y) v) => new Vec { x = v.x, y = v.y };

        public void Deconstruct(out float x, out float y)
        {
            x = this.x;
            y = this.y;
        }

        public static Vec operator +(Vec a, Vec b) => new Vec { x = a.x + b.x, y = a.y + b.y };
        public static Vec operator +(Vec a, (float x, float y) b) => new Vec { x = a.x + b.x, y = a.y + b.y };
        public static Vec operator -(Vec a, Vec b) => new Vec { x = a.x - b.x, y = a.y - b.y };
        public static Vec operator -(Vec a, (float x, float y) b) => new Vec { x = a.x - b.x, y = a.y - b.y };
        public static Vec operator *(Vec a, float b) => new Vec { x = a.x * b, y = a.y * b };
        public static Vec operator /(Vec a, float b) => new Vec { x = a.x / b, y = a.y / b };

        public void Add(Vec v)
        {
            x += v.x;
            y += v.y;
        }

        public void Add((float x, float y) v)
        {
            x += v.x;
            y += v.y;
        }

        public void Sub(Vec v)
        {
            x -= v.x;
            y -= v.y;
        }

        public void Sub((float x, float y) v)
        {
            x -= v.x;
            y -= v.y;
        }

        public static bool operator ==(Vec a, (float x, float y) b)
        {
            if (a.x != b.x) return false;
            if (a.y != b.y) return false;
            return true;
        }

        public static bool operator !=(Vec a, (float x, float y) b)
        {
            if (a.x != b.x) return true;
            if (a.y != b.y) return true;
            return false;
        }


        public static bool operator ==(Vec a, Vec b)
        {
            if (a.x != b.x) return false;
            if (a.y != b.y) return false;
            return true;
        }

        public static bool operator !=(Vec a, Vec b)
        {
            if (a.x != b.x) return true;
            if (a.y != b.y) return true;
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

        public override string ToString() => $"({x},{y})";
    }
}
