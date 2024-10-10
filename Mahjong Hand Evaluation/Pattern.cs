namespace Mahjong_Hand_Evaluation
{
    internal class Pattern
    {
        /// <summary>
        /// Hand closedness requirement. BONUS means open hand allowed, but if closed, value +1
        /// </summary>
        public enum CLOSE
        {
            YES,
            NO,
            BONUS
        }
        public string Name { get; protected set; }
        public int Value { get; protected set; }
        public CLOSE Close { get; protected set; }
        public string Description { get; protected set; }
        public Pattern(string Name, int Value, CLOSE Close, string Description)
        {
            this.Name = Name;
            this.Value = Value;
            this.Close = Close;
            this.Description = Description;
        }

        internal int OutputValue(bool Close)
        {
            if (this.Close == CLOSE.BONUS && Close) return Value + 1;
            else return Value;
        }

        internal string OutputString(bool Close)
        {
            if (this.Close == CLOSE.BONUS && Close) return "Closed " + Name + " - " + (Value + 1) + "\n";
            else return Name + " - " + Value + "\n";
        }

        internal static Pattern HONOR_BONUS = new("Honor Bonus", 1, CLOSE.NO, 
            "A Set of Dragon or Seat Wnd or Round Wind. Each of such Set in a hand is counted as 1. If a Set is both Seat and Round Wind, it is counted as 2.");
        internal static Pattern NO_POINT = new("No Point", 1, CLOSE.YES,
            "Standard hand with no additional Base point from hand content, i.e., no Triplet/Quad; the winning tile must be to complete a 2-wait Sequence; the pair must not be Dragon or seat/match Wind");
        internal static Pattern DOUBLE_SEQUENCE = new("Double Sequence", 1, CLOSE.YES,
            "2 identical Sequences");
        internal static Pattern TWICE_DOUBLE_SEQUENCE = new("2 Double Sequence", 3, CLOSE.YES,
            "2 instances of Double Sequence");
        internal static Pattern THREE_COLOR_SEQUENCE = new("3 Color Sequence", 1, CLOSE.BONUS,
            "3 Sequences of same numbers on 3 suits");
        internal static Pattern STRAIGHT = new("Straight", 1, CLOSE.BONUS,
            "3 Sequences of 1-2-3, 4-5-6, 7-8-9 on same suit");
        internal static Pattern ALL_TRIPLETS = new("All Triplets", 2, CLOSE.NO,
            "All Sets are Triplets/Quads");
        internal static Pattern THREE_COLOR_TRIPLETS = new("3 Color Triplets", 2, CLOSE.NO,
            "3 Triplets/Quads of same number on 3 suits");
        internal static Pattern THREE_CLOSED_TRIPLETS = new("3 Closed Triplets", 2, CLOSE.NO,
            "3 concealed Triplets/Quads");
        internal static Pattern THREE_CHAINED_TRIPLETS = new("3 Chained Triplets", 2, CLOSE.NO,
            "3 Triplets/Quads of consecutive numbers of same suit");
        internal static Pattern THREE_QUADS = new("3 Quads", 4, CLOSE.NO,
            "3 Quads");
        internal static Pattern LITTLE_THREE_DRAGON = new("Little 3 Dragon", 2, CLOSE.NO,
            "2 Sets and 1 pair of Dragon");
        internal static Pattern THREE_WIND_TRIPLETS = new("3 Wind Triplets", 2, CLOSE.NO,
            "3 Sets of Wind");
        internal static Pattern ALL_SIMPLE = new("All Simple", 1, CLOSE.NO,
            "Suited tiles of number from 2 to 8, i.e., no Terminal and Honor tiles");
        internal static Pattern PURE_MIDDLE = new("Pure Middle", 2, CLOSE.BONUS,
            "Suited tiles of number from 3 to 7");
        internal static Pattern MIXED_OUTSIDE = new("Mixed Outside", 1, CLOSE.BONUS,
            "All your Sets and pair must have at least 1 Terminal or Honor tile");
        internal static Pattern PURE_OUTSIDE = new("Pure Outside", 2, CLOSE.BONUS,
            "All your Sets and pair must have at least 1 Terminal tile");
        internal static Pattern MIXED_TERMINAL = new("Mixed Terminal", 2, CLOSE.NO, 
            "All tiles are Terminals or Honors");
        internal static Pattern FIVE_SUIT_COLLECTED = new("5 Suits Collected", 1, CLOSE.BONUS,
            "Standard hand that consists of Circle, Bamboo, Character, Wind, Dragon and must NOT contain more than 1 open Sequence");
        internal static Pattern MIXED_FLUSH = new("Mixed Flush", 2, CLOSE.BONUS,
            "All tiles from 1 suit or Honors");
        internal static Pattern FLUSH = new("Flush", 5, CLOSE.BONUS,
            "All tiles from 1 suit");
        internal static Pattern NINE_GATE = new("9 Gates", 10, CLOSE.YES,
            "1-1-1-2-3-4-5-6-7-8-9-9-9 of 1 suit and 1 same tile");
        internal static Pattern FOUR_CLOSED_TRIPLETS = new("4 Closed Triplets", 8, CLOSE.NO,
            "4 concealed Triples/Quads"); //4 closed sets. Functionally yes
        internal static Pattern FOUR_CHAINED_TRIPLETS = new("4 Chained Triplets", 8, CLOSE.NO,
            "4 Triplets/Quads of consecutive number of same suit");
        internal static Pattern FOUR_QUADS = new("4 Quads", 16, CLOSE.NO,
            "4 Quads");
        internal static Pattern ALL_TERMINAL = new("All Terminals", 10, CLOSE.NO, 
            "All titles are Terminals");
        internal static Pattern ALL_HONOR = new("All Honors", 8, CLOSE.NO,
            "All tiles are Honors");
        internal static Pattern ALL_HONOR_BIG_SEVEN_STAR = new("All Honors: Big 7 Star", 16, CLOSE.NO, 
            "All Honors, 7 pairs variant"); //Only for 7 Pairs. Functionally yes
        internal static Pattern BIG_THREE_DRAGON = new("Big 3 Dragon", 8, CLOSE.NO,
            "3 Sets of Dragon");
        internal static Pattern BIG_FOUR_WIND = new("Big 4 Wind", 12, CLOSE.NO, 
            "4 Sets of Wind");
        internal static Pattern LITTLE_FOUR_WIND = new("Little 4 Wind", 9, CLOSE.NO,
            "3 Sets and 1 pair of Wind");

        internal static Pattern SEVEN_PAIRS = new("7 Pairs", 1, CLOSE.NO, 
            "7 different pairs"); //No open sets can be made. Functionally yes
        internal static Pattern THIRTEEN_ORPHANS = new("13 Orphans", 13, CLOSE.NO,
            "12 different Terminal/Honor tiles and a pair of the 13th tile"); //No open sets can be made. Functionally yes

        ///Optional yaku

        internal static Pattern SIX_CONNECTED = new("6-connected", 4, CLOSE.YES,
            "2 identical series of 6 consecutive tiles of 1 suit (This pattern can be applied to both standard hand and 7 pairs hand)");
        internal static Pattern SEVEN_CONNECTED = new("7-connected", 7, CLOSE.YES,
            "2 identical series of 7 consecutive tiles of 1 suit (This pattern can be applied to both standard hand and 7 pairs hand)");

        public static List<Pattern> ListOfPattern { get; protected set; } =
        [
            HONOR_BONUS,
            NO_POINT,
            DOUBLE_SEQUENCE,
            TWICE_DOUBLE_SEQUENCE,
            THREE_COLOR_SEQUENCE,
            STRAIGHT,
            ALL_TRIPLETS,
            THREE_COLOR_TRIPLETS,
            THREE_CLOSED_TRIPLETS,
            THREE_CHAINED_TRIPLETS,
            THREE_QUADS,
            LITTLE_THREE_DRAGON,
            THREE_WIND_TRIPLETS,
            ALL_SIMPLE,
            PURE_MIDDLE,
            MIXED_OUTSIDE,
            PURE_OUTSIDE,
            MIXED_TERMINAL,
            FIVE_SUIT_COLLECTED,
            MIXED_FLUSH,
            FLUSH,
            NINE_GATE,
            FOUR_CLOSED_TRIPLETS,
            FOUR_CHAINED_TRIPLETS,
            FOUR_QUADS,
            ALL_TERMINAL,
            ALL_HONOR,
            ALL_HONOR_BIG_SEVEN_STAR,
            BIG_THREE_DRAGON,
            BIG_FOUR_WIND,
            LITTLE_FOUR_WIND,
            SEVEN_PAIRS,
            THIRTEEN_ORPHANS
        ];

        public static List<Pattern> ListOfPatternOptional { get; protected set; } =
        [
            SIX_CONNECTED,
            SEVEN_CONNECTED
        ];
    }
    
}
