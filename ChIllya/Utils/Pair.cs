namespace ChIllya.Utils
{
    public class Pair<TFirst, TLast>
    {
        public TFirst? First { get; set; }
        public TLast? Last { get; set; }

        public Pair()
        {
            
        }

        public Pair(TFirst first, TLast last)
        {
            First = first;
            Last = last;
        }
    }
}