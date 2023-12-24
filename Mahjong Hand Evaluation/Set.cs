namespace Mahjong_Hand_Evaluation
{
    internal class SetOfTile : IEqualityComparer<SetOfTile>
    {
        public enum SETTYPE
        {
            SEQUENCE = 0,
            TRIPLET = 1,
            QUAD = 2
        }
        public Tile StartTile { get; private set; }
        public bool Close { get; set; }
        public SETTYPE Type { get; private set; }
        public SetOfTile(Tile StartTile, bool Close = true, SETTYPE Type = SETTYPE.SEQUENCE)
        {
            this.StartTile = StartTile;
            this.Close = Close;
            this.Type = Type;
        }
        public SetOfTile() : this(new Tile()) { }
        public SetOfTile(SetOfTile C): this(C.StartTile.Clone(), C.Close, C.Type) { }
        public virtual SetOfTile Clone() { return new SetOfTile(this); }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode() 
        { 
            return 68 * (int)Type + 34 * (Close? 1 : 0) + StartTile.GetHashCode();
        }

        bool IEqualityComparer<SetOfTile>.Equals(SetOfTile? x, SetOfTile? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return x.Equals(y);
        }

        int IEqualityComparer<SetOfTile>.GetHashCode(SetOfTile obj)
        {
            return obj.GetHashCode();
        }
    }
}
