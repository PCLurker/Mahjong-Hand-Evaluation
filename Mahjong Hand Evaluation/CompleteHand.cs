namespace Mahjong_Hand_Evaluation
{
    internal abstract class CompleteHand
    {
        public Tile Pair { get; set; }
        public bool Draw { get; set; }
        public bool Closed { get; set; }
        /// <summary>
        /// Include Reach/Double Reach, One-shot, Self-pick, Rob a Quad, Dead wall draw, Last turn, Blessing of Heaven/Earth/Man
        /// </summary>
        public int NonContentPattern { get; set; }
        public int Dora { get; set; }
        public int Base { protected set; get; }
        public int PatternCount { protected set; get; }
        public CompleteHand(Tile P, bool Draw = true, bool Closed = true, int NonContentPattern = 0, int Dora = 0, int Base = 0, int Pattern = 0)
        {
            this.Pair = P;
            this.Draw = Draw;
            this.Closed = Closed;
            this.NonContentPattern = NonContentPattern;
            this.Dora = Dora;
            this.Base = Base;
            this.PatternCount = Pattern;
        }
        public CompleteHand() : this(new Tile()) { }
        public CompleteHand(CompleteHand C) : this(C.Pair.Clone(), C.Draw, C.Closed, C.NonContentPattern, C.Dora, C.Base, C.PatternCount) { }
        public abstract CompleteHand Clone();
        //public virtual WinningHand Clone() { return new WinningHand(this); }
        public abstract void CalculateBase();
        public abstract void CalculatePattern(out string PatternList);
        public int BaseValue(int Honba) { return (5 + Honba + (Draw ? 1 : 0) + (Closed ? 1 : 0) + Base) * (PatternCount + NonContentPattern + Dora); }
    }
    /// <summary>
    /// Hand containing 1 pair and 4 standard sets (Standard hand)
    /// </summary>
    internal class Set4Hand : CompleteHand
    {
        public List<SetOfTile> ListOfSet { get; set; }
        public int Complete { get; set; }
        public int SeatWind { get; set; }
        public int RoundWind { get; set; }
        protected int HonorCount { get; set; } = 0;
        public Set4Hand(List<SetOfTile> ListOfSet, Tile Pair, bool Draw = true, bool Closed = true, int NonContentPattern = 0, int Dora = 0, int Base = 0, int Pattern = 0,
            int Complete = 0, int SeatWind = 0, int RoundWind = 0) : base(Pair, Draw, Closed, NonContentPattern, Dora, Base, Pattern)
        {
            this.ListOfSet = ListOfSet;
            this.Complete = Complete;
            this.SeatWind = SeatWind;
            this.RoundWind = RoundWind;
        }
        public Set4Hand(): this(DefaultSet4HandList(), new Tile()) { }
        public Set4Hand(Set4Hand C) : this(CloneList(C.ListOfSet), C.Pair.Clone(), C.Draw, C.Closed, C.NonContentPattern, C.Dora, C.Base, C.PatternCount, C.Complete, C.SeatWind, C.RoundWind) { }
        public static List<SetOfTile> DefaultSet4HandList()
        {
            List<SetOfTile> setOfTiles = new()
            {
                new SetOfTile(new Tile(Tile.TYPE.CIRCLE, 1), true, SetOfTile.SETTYPE.TRIPLET),
                new SetOfTile(new Tile(Tile.TYPE.CIRCLE, 2), true, SetOfTile.SETTYPE.TRIPLET),
                new SetOfTile(new Tile(Tile.TYPE.CIRCLE, 3), true, SetOfTile.SETTYPE.TRIPLET),
                new SetOfTile(new Tile(Tile.TYPE.CIRCLE, 4), true, SetOfTile.SETTYPE.TRIPLET)
            };
            return setOfTiles;
        }
        public static List<SetOfTile> CloneList(List<SetOfTile> C)
        {
            List<SetOfTile > list = new();
            foreach (SetOfTile i in C) list.Add(i.Clone());
            return list;
        }
        public override void CalculateBase()
        {
            double res = 0;
            //Honor bonus from pair
            if (Pair.Type == Tile.TYPE.DRAGON) res += 0.5;
                else if (Pair.Type == Tile.TYPE.WIND)
                {
                    if (Pair.TypeIndex == SeatWind) res += 0.5;
                    if (Pair.TypeIndex == RoundWind) res += 0.5;
                }
            //Triplet/Quad
            foreach (SetOfTile i in this.ListOfSet)
            {
                if (i.Type == SetOfTile.SETTYPE.SEQUENCE) continue;
                double t = 0.5;
                //Quad or triplet
                if (i.Type == SetOfTile.SETTYPE.QUAD) t *= 4;
                //Closed set
                if (i.Close) t *= 2;
                //Terminal or Honor
                if (i.StartTile.TypeIndex == 0 || i.StartTile.TypeIndex == 8 || i.StartTile.Type == Tile.TYPE.DRAGON || i.StartTile.Type == Tile.TYPE.WIND) t *= 2;
                res += t;
                //Honor bonus
                if (i.StartTile.Type == Tile.TYPE.DRAGON) res += 0.5;
                else if (i.StartTile.Type == Tile.TYPE.WIND)
                {
                    if (i.StartTile.TypeIndex == SeatWind) res += 0.5;
                    if (i.StartTile.TypeIndex == RoundWind) res += 0.5;
                }
            }
            Base = (int)Math.Ceiling(res + (Complete == 0 ? 0 : 0.5));
        }
        public override void CalculatePattern(out string PatternList)
        {
            PatternList = "";
            int res = 0;
            bool NoPoint = Closed && Base == 0;
            bool DoubleSequence = false;
            bool TwiceDoubleSequence = false;
            bool ThreeColorSequence = false;
            bool Straight = false;
            bool AllTriplets = true;
            bool ThreeColorTriplets = false;
            bool ThreeClosedTriplets = false;
            bool ThreeChainedTriplets = false;
            bool ThreeQuads = false;
            int QuadCount = 0;
            bool LittleThreeDragon = false;
            bool ThreeWindTriplets = false;
            bool AllSimple = true;
            bool PureMiddle = true;
            bool MixedOutside = true;
            bool PureOutside = true;
            bool MixedTerminal = true;
            bool FiveSuitCollected = false;
            bool MixedFlush = false;
            bool Flush = false;
            bool NineGate = false;
            bool FourClosedTriplets = false;
            bool FourChainedTriplets = false;
            bool FourQuads = false;
            bool AllTerminal = true;
            bool AllHonor = false;
            bool BigThreeDragon = false;
            bool BigFourWind = false;
            bool LittleFourWind = false;
            HashSet<Tile.TYPE> nonhonor = new();
            HashSet<Tile.TYPE> honor = new();
            int WindCount = 0;
            int DragonCount = 0;
            int OpenSeqCount = 0;
            int ClosedTripletQuadCount = 0;
            int[] CircleSeq = new int[7];
            int[] BambooSeq = new int[7];
            int[] CharacterSeq = new int[7];
            int[] CircleTriplet = new int[9];
            int[] BambooTriplet = new int[9];
            int[] CharacterTriplet = new int[9];
            //if (!Closed)
            //{
            //    NoPoint = false;
            //    DoubleSequence = false;
            //    TwiceDoubleSequence = false;
            //}
            if (Pair.Type == Tile.TYPE.WIND || Pair.Type == Tile.TYPE.DRAGON)
            {
                honor.Add(Pair.Type);
                AllSimple = false;
                PureMiddle = false;
                AllTerminal = false;
                PureOutside = false;
            }
            else
            {
                nonhonor.Add(Pair.Type);
                if (Pair.TypeIndex == 0 || Pair.TypeIndex == 8) 
                {
                    AllSimple = false;
                    PureMiddle = false;
                }
                else if (Pair.TypeIndex == 1 || Pair.TypeIndex == 7)
                {
                    AllTerminal = false;
                    PureMiddle = false;
                    MixedOutside = false;
                    PureOutside = false;
                    MixedTerminal = false;
                }
                else
                {
                    AllTerminal = false;
                    MixedOutside = false;
                    PureOutside = false;
                    MixedTerminal = false;
                }

            }
            foreach (SetOfTile s in ListOfSet)
            {
                if (s.Type == SetOfTile.SETTYPE.SEQUENCE)
                {
                    AllTriplets = false;
                    MixedTerminal = false;
                    AllTerminal = false;
                    if (!s.Close) OpenSeqCount++;
                }
                else if (s.Type == SetOfTile.SETTYPE.TRIPLET)
                {
                    if (s.Close) ClosedTripletQuadCount++;
                }
                else // QUAD
                {
                    QuadCount++;
                    if (s.Close) ClosedTripletQuadCount++;
                }

                if (s.StartTile.Type == Tile.TYPE.WIND)
                {
                    WindCount++;
                    honor.Add(s.StartTile.Type);
                    if (s.StartTile.TypeIndex == SeatWind) HonorCount++;
                    if (s.StartTile.TypeIndex == RoundWind) HonorCount++;
                    AllSimple = false;
                    PureMiddle = false;
                    AllTerminal = false;
                    PureOutside = false;
                }
                else if (s.StartTile.Type == Tile.TYPE.DRAGON)
                {
                    DragonCount++;
                    honor.Add(s.StartTile.Type);
                    HonorCount++;
                    AllSimple = false;
                    PureMiddle = false;
                    AllTerminal = false;
                    PureOutside = false;
                }
                else //Numbered tiles
                {
                    nonhonor.Add(s.StartTile.Type);
                    if (s.Type == SetOfTile.SETTYPE.SEQUENCE)
                    {
                        if (s.StartTile.Type == Tile.TYPE.CIRCLE)
                        {
                            CircleSeq[s.StartTile.TypeIndex]++;
                        }
                        else if (s.StartTile.Type == Tile.TYPE.BAMBOO)
                        {
                            BambooSeq[s.StartTile.TypeIndex]++;
                        }
                        else
                        {
                            CharacterSeq[s.StartTile.TypeIndex]++;
                        }
                        if (s.StartTile.TypeIndex == 0 || s.StartTile.TypeIndex == 6)
                        {
                            AllSimple = false;
                            PureMiddle = false;
                        }
                        else if (s.StartTile.TypeIndex == 1 || s.StartTile.TypeIndex == 5)
                        {
                            PureMiddle = false;
                            MixedOutside = false;
                            PureOutside = false;
                        }
                        else
                        {
                            MixedOutside = false;
                            PureOutside = false;
                        }
                    }
                    else 
                    {
                        if (s.StartTile.Type == Tile.TYPE.CIRCLE)
                        {
                            CircleTriplet[s.StartTile.TypeIndex]++;
                        }
                        else if (s.StartTile.Type == Tile.TYPE.BAMBOO)
                        {
                            BambooTriplet[s.StartTile.TypeIndex]++;
                        }
                        else
                        {
                            CharacterTriplet[s.StartTile.TypeIndex]++;
                        }
                        if (s.StartTile.TypeIndex == 0 || s.StartTile.TypeIndex == 8)
                        {
                            AllSimple = false;
                            PureMiddle = false;
                        }
                        else if (s.StartTile.TypeIndex == 1 || s.StartTile.TypeIndex == 7)
                        {
                            AllTerminal = false;
                            PureMiddle = false;
                            MixedOutside = false;
                            PureOutside = false;
                            MixedTerminal = false;
                        }
                        else
                        {
                            AllTerminal = false;
                            MixedOutside = false;
                            PureOutside = false;
                            MixedTerminal = false;
                        }
                    }
                }
            }
            // Check for sequence based pattern
            for (int i = 0; i < 7; i++)
            {
                bool Seq3 = true;
                if (CircleSeq[i] == 0)
                {
                    Seq3 = false;
                } 
                else if (Closed)
                {
                    if (CircleSeq[i] == 4) TwiceDoubleSequence = true;
                    else if (CircleSeq[i] > 1)
                    {
                        if (DoubleSequence) TwiceDoubleSequence = true;
                        else DoubleSequence = true;
                    }
                }
                if (BambooSeq[i] == 0)
                {
                    Seq3 = false;
                }
                else if (Closed)
                {
                    if (BambooSeq[i] == 4) TwiceDoubleSequence = true;
                    else if (BambooSeq[i] > 1)
                    {
                        if (DoubleSequence) TwiceDoubleSequence = true;
                        else DoubleSequence = true;
                    }
                }
                if (CharacterSeq[i] == 0)
                {
                    Seq3 = false;
                }
                else if (Closed)
                {
                    if (CharacterSeq[i] == 4) TwiceDoubleSequence = true;
                    else if (CharacterSeq[i] > 1)
                    {
                        if (DoubleSequence) TwiceDoubleSequence = true;
                        else DoubleSequence = true;
                    }
                }
                if (Seq3) ThreeColorSequence = true;
            }
            if ((CircleSeq[0] > 0 && CircleSeq[3] > 0 && CircleSeq[6] > 0)
                || (BambooSeq[0] > 0 && BambooSeq[3] > 0 && BambooSeq[6] > 0)
                || (CharacterSeq[0] > 0 && CharacterSeq[3] > 0 && CharacterSeq[6] > 0)) Straight = true;

            // Check for triplet based pattern
            int MaxChain = 0;
            int ChainCircle = 0;
            int ChainBamboo = 0;
            int ChainCharacter = 0;
            for (int i = 0; i < 9; i++)
            {
                bool Tri3 = true;
                if (CircleTriplet[i] == 0)
                {
                    Tri3 = false;
                    ChainCircle = 0;
                }
                else
                {
                    ChainCircle++;
                    if (ChainCircle > MaxChain) MaxChain = ChainCircle;
                }
                if (BambooTriplet[i] == 0)
                {
                    Tri3 = false;
                    ChainBamboo = 0;
                }
                else
                {
                    ChainBamboo++;
                    if (ChainBamboo > MaxChain) MaxChain = ChainBamboo;
                }
                if (CharacterTriplet[i] == 0)
                {
                    Tri3 = false;
                    ChainCharacter = 0;
                }
                else
                {
                    ChainCharacter++;
                    if (ChainCharacter > MaxChain) MaxChain = ChainCharacter;
                }
                if (Tri3) ThreeColorTriplets = true;
            }
            if (MaxChain == 4) FourChainedTriplets = true;
            else if (MaxChain == 3) ThreeChainedTriplets = true;
            if (ClosedTripletQuadCount == 4) FourClosedTriplets = true;
            else if (ClosedTripletQuadCount == 3) ThreeClosedTriplets = true;
            if (QuadCount == 4) FourQuads = true;
            else if (QuadCount == 3) ThreeQuads = true;

            if (DragonCount == 3) BigThreeDragon = true;
            else if (DragonCount == 2 && Pair.Type == Tile.TYPE.DRAGON) LittleThreeDragon = true;

            if (WindCount == 4) BigFourWind = true;
            else if (WindCount == 3)
                if (Pair.Type == Tile.TYPE.WIND) LittleFourWind = true;
                else ThreeWindTriplets = true;

            if (honor.Count + nonhonor.Count == 5 && OpenSeqCount <= 1) FiveSuitCollected = true;
            if (nonhonor.Count == 0) AllHonor = true;
            if (nonhonor.Count == 1)
            {
                if (honor.Count > 0) MixedFlush = true;
                else
                {
                    Flush = true;
                    //Nine gate
                    if (Closed && QuadCount == 0)
                    {
                        int[] index = new int[9];
                        index[Pair.TypeIndex] += 2;
                        foreach (SetOfTile s in ListOfSet)
                        {
                            if (s.Type == SetOfTile.SETTYPE.SEQUENCE)
                            {
                                index[s.StartTile.TypeIndex] += 1;
                                index[s.StartTile.TypeIndex + 1] += 1;
                                index[s.StartTile.TypeIndex + 2] += 1;
                            }
                            else index[s.StartTile.TypeIndex] += 3;
                        }
                        if (index[0] >= 3 && index[1] >= 1 && index[2] >= 1
                            && index[3] >= 1 && index[4] >= 1 && index[5] >= 1
                            && index[6] >= 1 && index[7] >= 1 && index[8] >= 3) NineGate = true;
                    }
                }
            }

            if (HonorCount > 0) { res += HonorCount; PatternList += "Honor Bonus - " + HonorCount + "\n"; }
            if (NoPoint) { res += Pattern.NO_POINT.OutputValue(Closed); PatternList += Pattern.NO_POINT.OutputString(Closed); }

            if (TwiceDoubleSequence) { res += Pattern.TWICE_DOUBLE_SEQUENCE.OutputValue(Closed); PatternList += Pattern.TWICE_DOUBLE_SEQUENCE.OutputString(Closed); }
            else if (DoubleSequence) { res += Pattern.DOUBLE_SEQUENCE.OutputValue(Closed); PatternList += Pattern.DOUBLE_SEQUENCE.OutputString(Closed); }

            if (ThreeColorSequence) { res += Pattern.THREE_COLOR_SEQUENCE.OutputValue(Closed); PatternList += Pattern.THREE_COLOR_SEQUENCE.OutputString(Closed); }
            if (Straight) { res += Pattern.STRAIGHT.OutputValue(Closed); PatternList += Pattern.STRAIGHT.OutputString(Closed); }
            if (AllTriplets) { res += Pattern.ALL_TRIPLETS.OutputValue(Closed); PatternList += Pattern.ALL_TRIPLETS.OutputString(Closed); }
            if (ThreeColorTriplets) { res += Pattern.THREE_COLOR_TRIPLETS.OutputValue(Closed); PatternList += Pattern.THREE_COLOR_TRIPLETS.OutputString(Closed); }

            if (FourClosedTriplets) { res += Pattern.FOUR_CLOSED_TRIPLETS.OutputValue(Closed); PatternList += Pattern.FOUR_CLOSED_TRIPLETS.OutputString(Closed); }
            else if (ThreeClosedTriplets) { res += Pattern.THREE_CLOSED_TRIPLETS.OutputValue(Closed); PatternList += Pattern.THREE_CLOSED_TRIPLETS.OutputString(Closed); }
            if (FourChainedTriplets) { res += Pattern.FOUR_CHAINED_TRIPLETS.OutputValue(Closed); PatternList += Pattern.FOUR_CHAINED_TRIPLETS.OutputString(Closed); }
            else if (ThreeChainedTriplets) { res += Pattern.THREE_CHAINED_TRIPLETS.OutputValue(Closed); PatternList += Pattern.THREE_CHAINED_TRIPLETS.OutputString(Closed); }
            if (FourQuads) { res += Pattern.FOUR_QUADS.OutputValue(Closed); PatternList += Pattern.FOUR_QUADS.OutputString(Closed); }
            else if (ThreeQuads) { res += Pattern.THREE_QUADS.OutputValue(Closed); PatternList += Pattern.THREE_QUADS.OutputString(Closed); }

            if (BigThreeDragon) { res += Pattern.BIG_THREE_DRAGON.OutputValue(Closed); PatternList += Pattern.BIG_THREE_DRAGON.OutputString(Closed); }
            if (LittleThreeDragon) { res += Pattern.LITTLE_THREE_DRAGON.OutputValue(Closed); PatternList += Pattern.LITTLE_THREE_DRAGON.OutputString(Closed); }

            if (BigFourWind) { res += Pattern.BIG_FOUR_WIND.OutputValue(Closed); PatternList += Pattern.BIG_FOUR_WIND.OutputString(Closed); }
            else if (LittleFourWind) { res += Pattern.LITTLE_FOUR_WIND.OutputValue(Closed); PatternList += Pattern.LITTLE_FOUR_WIND.OutputString(Closed); }
            else if (ThreeWindTriplets) { res += Pattern.THREE_WIND_TRIPLETS.OutputValue(Closed); PatternList += Pattern.THREE_WIND_TRIPLETS.OutputString(Closed); }            

            if (PureMiddle) { res += Pattern.PURE_MIDDLE.OutputValue(Closed); PatternList += Pattern.PURE_MIDDLE.OutputString(Closed); }
            else if (AllSimple) { res += Pattern.ALL_SIMPLE.OutputValue(Closed); PatternList += Pattern.ALL_SIMPLE.OutputString(Closed); }

            if (FiveSuitCollected) { res += Pattern.FIVE_SUIT_COLLECTED.OutputValue(Closed); PatternList += Pattern.FIVE_SUIT_COLLECTED.OutputString(Closed); }

            if (AllTerminal) { res += Pattern.ALL_TERMINAL.OutputValue(Closed); PatternList += Pattern.ALL_TERMINAL.OutputString(Closed); }
            else if (AllHonor) { res += Pattern.ALL_HONOR.OutputValue(Closed); PatternList += Pattern.ALL_HONOR.OutputString(Closed); }
            else
            {
                if (MixedTerminal) { res += Pattern.MIXED_TERMINAL.OutputValue(Closed); PatternList += Pattern.MIXED_TERMINAL.OutputString(Closed); }
                else if (PureOutside) { res += Pattern.PURE_OUTSIDE.OutputValue(Closed); PatternList += Pattern.PURE_OUTSIDE.OutputString(Closed); }
                else if (MixedOutside) { res += Pattern.MIXED_OUTSIDE.OutputValue(Closed); PatternList += Pattern.MIXED_OUTSIDE.OutputString(Closed); }
            }

            if (Flush)
            {
                res += Pattern.FLUSH.OutputValue(Closed);
                PatternList += Pattern.FLUSH.OutputString(Closed);
                if (NineGate) { res += Pattern.NINE_GATE.OutputValue(Closed); PatternList += Pattern.NINE_GATE.OutputString(Closed); }
            }
            else if (MixedFlush) { res += Pattern.MIXED_FLUSH.OutputValue(Closed); PatternList += Pattern.MIXED_FLUSH.OutputString(Closed); }

            PatternCount = res;
        }
        public override CompleteHand Clone() { return new Set4Hand(this); }
    }
    /// <summary>
    /// Hand containing 1 pair and 6 sets of 2 (7 Pairs)
    /// </summary>
    internal class Set6Hand: CompleteHand
    {
        public List<Tile> ListOfPair { get; set; }
        public Set6Hand(List<Tile> ListOfPair, Tile Pair, bool Draw = true, bool Closed = true, int NonContentPattern = 0, int Dora = 0, int Base = 0, int Pattern = 0) : base(Pair, Draw, Closed, NonContentPattern, Dora, Base, Pattern)
        {
            this.ListOfPair = ListOfPair;
        }
        public Set6Hand() : this(DefaultSet6HandList(), new Tile()) { }
        public static List<Tile> DefaultSet6HandList()
        {
            List<Tile> setOfPairs = new()
            {
                new Tile(Tile.TYPE.CIRCLE, 1),
                new Tile(Tile.TYPE.CIRCLE, 2),
                new Tile(Tile.TYPE.CIRCLE, 3),
                new Tile(Tile.TYPE.CIRCLE, 4),
                new Tile(Tile.TYPE.CIRCLE, 5),
                new Tile(Tile.TYPE.CIRCLE, 6),
            };
            return setOfPairs;
        }
        public Set6Hand(Set6Hand C) : this(CloneList(C.ListOfPair), C.Pair, C.Draw, C.Closed, C.NonContentPattern, C.Dora, C.Base, C.PatternCount) { }
        public static List<Tile> CloneList(List<Tile> C)
        {
            List<Tile> list = new();
            foreach (Tile i in C) 
                list.Add(i.Clone());
            return list;
        }
        public override void CalculateBase() { Base = 5; }
        /// <summary>
        /// The hand's validity is assumed to be already determined in the hand forming process. The hand is already qualified for 7 Pairs
        /// </summary>
        /// <returns>Pattern value of the hand. Add 1 from 7 Pairs</returns>
        public override void CalculatePattern(out string PatternList)
        {
            int res = Pattern.SEVEN_PAIRS.OutputValue(Closed);
            PatternList = Pattern.SEVEN_PAIRS.OutputString(Closed);
            bool AllSimple = true;
            bool PureMiddle = true;
            bool MixTerminal = true;
            bool MixedFlush = true;
            bool Flush = true;
            bool AllHonors = true;
            HashSet<Tile.TYPE> nonhonor = new();
            HashSet<Tile.TYPE> honor = new();
            if (Pair.Type == Tile.TYPE.WIND || Pair.Type == Tile.TYPE.DRAGON)
            {
                AllSimple = false;
                PureMiddle = false;
                honor.Add(Pair.Type);
            }
            else
            {
                nonhonor.Add(Pair.Type);
                if (Pair.TypeIndex == 0 || Pair.TypeIndex == 8)
                {
                    PureMiddle = false;
                    AllSimple = false;
                }
                else if (Pair.TypeIndex == 1 || Pair.TypeIndex == 7)
                {
                    PureMiddle = false;
                    MixTerminal = false;
                }
                else
                {
                    MixTerminal = false;
                }
            }
            foreach (Tile t in this.ListOfPair)
            {
                if (t.Type == Tile.TYPE.WIND || t.Type == Tile.TYPE.DRAGON)
                {
                    AllSimple = false;
                    PureMiddle = false;
                    honor.Add(t.Type);
                }
                else
                {
                    nonhonor.Add(t.Type);
                    if (t.TypeIndex == 0 || t.TypeIndex == 8)
                    {
                        PureMiddle = false;
                        AllSimple = false;
                    }
                    else if (t.TypeIndex == 1 || t.TypeIndex == 7)
                    {
                        PureMiddle = false;
                        MixTerminal = false;
                    }                        
                    else
                    {
                        MixTerminal = false;
                    }
                }
            }
            int countNonHonorType = nonhonor.Count;
            int countHonorType = honor.Count;
            if (countNonHonorType > 1)
            {
                MixedFlush = false;
                Flush = false;
                AllHonors = false;
            }
            else if (countNonHonorType == 1)
            {
                AllHonors = false;
                if (countHonorType > 0)
                    Flush = false;
            }
            else
            {
                MixedFlush = false;
                Flush = false;
            }

            if (PureMiddle) { res += Pattern.PURE_MIDDLE.OutputValue(Closed); PatternList += Pattern.PURE_MIDDLE.OutputString(Closed); }
            else if (AllSimple) { res += Pattern.ALL_SIMPLE.OutputValue(Closed); PatternList += Pattern.ALL_SIMPLE.OutputString(Closed); }
            if (Flush) { res += Pattern.FLUSH.OutputValue(Closed); PatternList += Pattern.FLUSH.OutputString(Closed); }
            else if (AllHonors) { res += Pattern.ALL_HONOR_BIG_SEVEN_STAR.OutputValue(Closed); PatternList += Pattern.ALL_HONOR_BIG_SEVEN_STAR.OutputString(Closed); }
            else
            {
                if (MixedFlush) { res += Pattern.MIXED_FLUSH.OutputValue(Closed); PatternList += Pattern.MIXED_FLUSH.OutputString(Closed); }
                if (MixTerminal) { res += Pattern.MIXED_TERMINAL.OutputValue(Closed); PatternList += Pattern.MIXED_TERMINAL.OutputString(Closed); }
            }

            PatternCount = res;
        }
        public override CompleteHand Clone() { return new Set6Hand(this); }
    } 
    /// <summary>
    /// Hand containing 1 pair and 12 sets of 1 (13 Orphans)
    /// </summary>
    internal class Set12Hand: CompleteHand
    {
        public List<Tile> ListOfTile { get; set; }
        public Set12Hand(List<Tile> ListOfTile, Tile Pair, bool Draw = true, bool Closed = true, int NonContentPattern = 0, int Dora = 0, int Base = 0, int Pattern = 0) : base(Pair, Draw, Closed, NonContentPattern, Dora, Base, Pattern)
        {
            this.ListOfTile = ListOfTile;
        }
        public Set12Hand() : this(DefaultSet12HandList(), new Tile()) { }
        public static List<Tile> DefaultSet12HandList()
        {
            List<Tile> setOfTiles = new()
            {
                new Tile(Tile.TYPE.CIRCLE, 8),
                new Tile(Tile.TYPE.BAMBOO, 0),
                new Tile(Tile.TYPE.BAMBOO, 8),
                new Tile(Tile.TYPE.CHARACTER, 0),
                new Tile(Tile.TYPE.CHARACTER, 8),
                new Tile(Tile.TYPE.WIND, 0),
                new Tile(Tile.TYPE.WIND, 1),
                new Tile(Tile.TYPE.WIND, 2),
                new Tile(Tile.TYPE.WIND, 3),
                new Tile(Tile.TYPE.DRAGON, 0),
                new Tile(Tile.TYPE.DRAGON, 1),
                new Tile(Tile.TYPE.DRAGON, 2),
            };
            return setOfTiles;
        }
        public Set12Hand(Set12Hand C) : this(CloneList(C.ListOfTile), C.Pair, C.Draw, C.Closed, C.NonContentPattern, C.Dora, C.Base, C.PatternCount) { }
        public static List<Tile> CloneList(List<Tile> C)
        {
            List<Tile> list = new();
            foreach (Tile i in C) list.Add(i.Clone());
            return list;
        }
        public override void CalculateBase() { Base = 5; }
        public override void CalculatePattern(out string PatternList) 
        {
            PatternCount = Pattern.THIRTEEN_ORPHANS.OutputValue(Closed);
            PatternList = Pattern.THIRTEEN_ORPHANS.OutputString(Closed);
        }
        public override CompleteHand Clone() { return new Set12Hand(this); }
    }
}
