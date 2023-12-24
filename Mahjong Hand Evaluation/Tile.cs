namespace Mahjong_Hand_Evaluation
{
    internal class Tile : IEqualityComparer<Tile>
    {
        public enum TYPE { CIRCLE = 0, BAMBOO = 1, CHARACTER = 2, WIND = 3, DRAGON = 4}
        public TYPE Type { get; private set; }
        public int TypeIndex { get; private set; }
        public Tile(TYPE Type = TYPE.CIRCLE, int TypeIndex = 0)
        {
            this.Type = Type;
            this.TypeIndex = TypeIndex;
        }
        public Tile(int Index)
        {
            int t, ti;
            GetTileDetail(Index, out t, out ti);
            Type = (TYPE)t;
            TypeIndex = ti;
        }
        public Tile(Tile C) : this(C.Type, C.TypeIndex) { }

        public static void GetTileDetail(int Index, out int Type, out int TypeIndex)
        {
            if (Index >= 0 && Index < 27)
            {
                Type = Index / 9;
                TypeIndex = Index % 9;
            }
            else if (Index >= 27 && Index < 31)
            {
                Type = 3;
                TypeIndex = Index - 27;
            }
            else if (Index >= 31 && Index < 34)
            {
                Type = 4;
                TypeIndex = Index - 31;
            }
            else throw new Exception("Unexpected Type Index");
        }
        virtual public Tile Clone() { return new Tile(this); }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;  
            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            int res = TypeIndex;
            if (Type == TYPE.BAMBOO) res += 9;
            if (Type == TYPE.CHARACTER) res += 18;
            if (Type == TYPE.WIND) res += 27;
            if (Type == TYPE.DRAGON) res += 31;
            return res;
        }

        bool IEqualityComparer<Tile>.Equals(Tile? x, Tile? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return x.Equals(y);
        }

        int IEqualityComparer<Tile>.GetHashCode(Tile obj)
        {
            return obj.GetHashCode();
        }
    }
}
