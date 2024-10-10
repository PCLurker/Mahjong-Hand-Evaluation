using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace Mahjong_Hand_Evaluation
{
    public partial class Form1 : Form
    {
        public List<System.Drawing.Bitmap> TileList { get; set; } = new() {
            Properties.Resources.Circle_1,
            Properties.Resources.Circle_2,
            Properties.Resources.Circle_3,
            Properties.Resources.Circle_4,
            Properties.Resources.Circle_5,
            Properties.Resources.Circle_6,
            Properties.Resources.Circle_7,
            Properties.Resources.Circle_8,
            Properties.Resources.Circle_9,

            Properties.Resources.Bamboo_1,
            Properties.Resources.Bamboo_2,
            Properties.Resources.Bamboo_3,
            Properties.Resources.Bamboo_4,
            Properties.Resources.Bamboo_5,
            Properties.Resources.Bamboo_6,
            Properties.Resources.Bamboo_7,
            Properties.Resources.Bamboo_8,
            Properties.Resources.Bamboo_9,

            Properties.Resources.Character_1,
            Properties.Resources.Character_2,
            Properties.Resources.Character_3,
            Properties.Resources.Character_4,
            Properties.Resources.Character_5,
            Properties.Resources.Character_6,
            Properties.Resources.Character_7,
            Properties.Resources.Character_8,
            Properties.Resources.Character_9,

            Properties.Resources.Wind_East,
            Properties.Resources.Wind_South,
            Properties.Resources.Wind_West,
            Properties.Resources.Wind_North,

            Properties.Resources.Dragon_White,
            Properties.Resources.Dragon_Green,
            Properties.Resources.Dragon_Red,
        };

        public List<ComboBox> SetList { get; set; }
        public List<ComboBox> SetTypeList { get; set; }
        public List<ComboBox> HandSet { get; set; }

        public Form1()
        {
            InitializeComponent();
            comboBox_Hand1.SelectedIndex = 17;
            comboBox_Hand2.SelectedIndex = 17;
            comboBox_Hand3.SelectedIndex = 24;
            comboBox_Hand4.SelectedIndex = 25;
            comboBox_Hand5.SelectedIndex = 26;
            comboBox_Hand6.SelectedIndex = 0;
            comboBox_Hand7.SelectedIndex = 1;
            comboBox_Hand8.SelectedIndex = 2;
            comboBox_Hand9.SelectedIndex = 33;
            comboBox_Hand10.SelectedIndex = 33;
            comboBox_Hand11.SelectedIndex = 27;
            comboBox_Hand12.SelectedIndex = 27;
            comboBox_Hand13.SelectedIndex = 27;
            comboBox_RoundWind.SelectedIndex = 0;
            comboBox_SeatWind.SelectedIndex = 0;
            comboBox_Set1Tile.SelectedIndex = 0;
            comboBox_Set2Tile.SelectedIndex = 0;
            comboBox_Set3Tile.SelectedIndex = 0;
            comboBox_Set4Tile.SelectedIndex = 0;
            comboBox_WinningTile.SelectedIndex = 33;
            comboBox_Set1Type.SelectedIndex = 0;
            comboBox_Set2Type.SelectedIndex = 0;
            comboBox_Set3Type.SelectedIndex = 0;
            comboBox_Set4Type.SelectedIndex = 0;
            comboBox_NumOfSet.SelectedIndex = 0;
            SetList = new() {
                comboBox_Set1Tile,
                comboBox_Set2Tile,
                comboBox_Set3Tile,
                comboBox_Set4Tile,
            };
            SetTypeList = new() {
                comboBox_Set1Type,
                comboBox_Set2Type,
                comboBox_Set3Type,
                comboBox_Set4Type,
            };
            HandSet = new()
            {
                comboBox_Hand1,
                comboBox_Hand2,
                comboBox_Hand3,
                comboBox_Hand4,
                comboBox_Hand5,
                comboBox_Hand6,
                comboBox_Hand7,
                comboBox_Hand8,
                comboBox_Hand9,
                comboBox_Hand10,
                comboBox_Hand11,
                comboBox_Hand12,
                comboBox_Hand13,
            };

            foreach (Pattern p in Pattern.ListOfPattern)
            {
                richTextBox_PatternRule.Text += $"    {p.Name} - {p.Value}\n{p.Description}\n";
                if (p.Close == Pattern.CLOSE.YES)
                    richTextBox_PatternRule.Text += "Hand must be closed\n";
                else if (p.Close == Pattern.CLOSE.BONUS)
                    richTextBox_PatternRule.Text += "If closed hand, +1\n";
                richTextBox_PatternRule.Text += "\n";
            }

            richTextBox_PatternRule.Text += "\nOptional Yaku:\n\n\n";

            foreach (Pattern p in Pattern.ListOfPatternOptional)
            {
                richTextBox_PatternRule.Text += $"    {p.Name} - {p.Value}\n{p.Description}\n";
                if (p.Close == Pattern.CLOSE.YES)
                    richTextBox_PatternRule.Text += "Hand must be closed\n";
                else if (p.Close == Pattern.CLOSE.BONUS)
                    richTextBox_PatternRule.Text += "If closed hand, +1\n";
                richTextBox_PatternRule.Text += "\n";
            }
        }

        private void ComboBox_Set1Tile_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Set1.Image = TileList[comboBox_Set1Tile.SelectedIndex];
        }

        private void comboBox_Set2Tile_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Set2.Image = TileList[comboBox_Set2Tile.SelectedIndex];
        }

        private void comboBox_Set3Tile_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Set3.Image = TileList[comboBox_Set3Tile.SelectedIndex];
        }

        private void comboBox_Set4Tile_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Set4.Image = TileList[comboBox_Set4Tile.SelectedIndex];
        }

        private void comboBox_WinningTile_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_WinningTile.Image = TileList[comboBox_WinningTile.SelectedIndex];
        }

        private void comboBox_Hand1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Hand1.Image = TileList[comboBox_Hand1.SelectedIndex];
        }

        private void comboBox_Hand2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Hand2.Image = TileList[comboBox_Hand2.SelectedIndex];
        }

        private void comboBox_Hand3_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Hand3.Image = TileList[comboBox_Hand3.SelectedIndex];
        }

        private void comboBox_Hand4_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Hand4.Image = TileList[comboBox_Hand4.SelectedIndex];
        }

        private void comboBox_Hand5_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Hand5.Image = TileList[comboBox_Hand5.SelectedIndex];
        }

        private void comboBox_Hand6_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Hand6.Image = TileList[comboBox_Hand6.SelectedIndex];
        }

        private void comboBox_Hand7_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Hand7.Image = TileList[comboBox_Hand7.SelectedIndex];
        }

        private void comboBox_Hand8_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Hand8.Image = TileList[comboBox_Hand8.SelectedIndex];
        }

        private void comboBox_Hand9_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Hand9.Image = TileList[comboBox_Hand9.SelectedIndex];
        }

        private void comboBox_Hand10_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Hand10.Image = TileList[comboBox_Hand10.SelectedIndex];
        }

        private void comboBox_Hand11_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Hand11.Image = TileList[comboBox_Hand11.SelectedIndex];
        }

        private void comboBox_Hand12_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Hand12.Image = TileList[comboBox_Hand12.SelectedIndex];
        }

        private void comboBox_Hand13_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox_Hand13.Image = TileList[comboBox_Hand13.SelectedIndex];
        }

        void SetDeterminationLoop() { }

        List<Tile> Dummy7Pairs()
        {
            return new List<Tile>()
            {
                new Tile(comboBox_Hand1.SelectedIndex),
                new Tile(comboBox_Hand3.SelectedIndex),
                new Tile(comboBox_Hand5.SelectedIndex),
                new Tile(comboBox_Hand7.SelectedIndex),
                new Tile(comboBox_Hand9.SelectedIndex),
                new Tile(comboBox_Hand11.SelectedIndex)
            };
        }

        List<SetOfTile> Dummy4Sets()
        {
            return new List<SetOfTile>()
            {
                new SetOfTile(new Tile(comboBox_Set1Tile.SelectedIndex), comboBox_Set1Type.SelectedIndex / 3 == 1, (SetOfTile.SETTYPE)(comboBox_Set1Type.SelectedIndex % 3)),
                new SetOfTile(new Tile(comboBox_Set2Tile.SelectedIndex), comboBox_Set2Type.SelectedIndex / 3 == 1, (SetOfTile.SETTYPE)(comboBox_Set2Type.SelectedIndex % 3)),
                new SetOfTile(new Tile(comboBox_Set3Tile.SelectedIndex), comboBox_Set3Type.SelectedIndex / 3 == 1, (SetOfTile.SETTYPE)(comboBox_Set3Type.SelectedIndex % 3)),
                new SetOfTile(new Tile(comboBox_Set4Tile.SelectedIndex), comboBox_Set4Type.SelectedIndex / 3 == 1, (SetOfTile.SETTYPE)(comboBox_Set4Type.SelectedIndex % 3)),
            };
        }

        Set12Hand? Check13Orphans(int[] Circle, int[] Bamboo, int[] Character, int[] Wind, int[] Dragon, int NCP, int D)
        {
            for (int i = 1; i < 8; i++)
            {
                if (Circle[i] > 0) return null;
                if (Bamboo[i] > 0) return null;
                if (Character[i] > 0) return null;
            }
            if (Circle[0] > 0 && Circle[8] > 0 && Bamboo[0] > 0 && Bamboo[8] > 0 && Character[0] > 0 && Character[8] > 0
                && Wind[0] > 0 && Wind[1] > 0 && Wind[2] > 0 && Wind[3] > 0
                && Dragon[0] > 0 && Dragon[1] > 0 && Dragon[2] > 0)
            {
                //Found. Form hand
                List<Tile> list = new List<Tile>();
                Tile Pair = null;
                if (Circle[0] == 1) list.Add(new Tile(Tile.TYPE.CIRCLE, 0));
                else Pair = new Tile(Tile.TYPE.CIRCLE, 0);
                if (Circle[8] == 1) list.Add(new Tile(Tile.TYPE.CIRCLE, 8));
                else Pair = new Tile(Tile.TYPE.CIRCLE, 8);
                if (Bamboo[0] == 1) list.Add(new Tile(Tile.TYPE.BAMBOO, 0));
                else Pair = new Tile(Tile.TYPE.BAMBOO, 0);
                if (Bamboo[8] == 1) list.Add(new Tile(Tile.TYPE.BAMBOO, 8));
                else Pair = new Tile(Tile.TYPE.BAMBOO, 8);
                if (Character[0] == 1) list.Add(new Tile(Tile.TYPE.CHARACTER, 0));
                else Pair = new Tile(Tile.TYPE.CHARACTER, 0);
                if (Character[8] == 1) list.Add(new Tile(Tile.TYPE.CHARACTER, 8));
                else Pair = new Tile(Tile.TYPE.CHARACTER, 8);

                if (Wind[0] == 1) list.Add(new Tile(Tile.TYPE.WIND, 0));
                else Pair = new Tile(Tile.TYPE.WIND, 0);
                if (Wind[1] == 1) list.Add(new Tile(Tile.TYPE.WIND, 1));
                else Pair = new Tile(Tile.TYPE.WIND, 1);
                if (Wind[2] == 1) list.Add(new Tile(Tile.TYPE.WIND, 2));
                else Pair = new Tile(Tile.TYPE.WIND, 2);
                if (Wind[3] == 1) list.Add(new Tile(Tile.TYPE.WIND, 3));
                else Pair = new Tile(Tile.TYPE.WIND, 3);

                if (Dragon[0] == 1) list.Add(new Tile(Tile.TYPE.DRAGON, 0));
                else Pair = new Tile(Tile.TYPE.DRAGON, 0);
                if (Dragon[1] == 1) list.Add(new Tile(Tile.TYPE.DRAGON, 1));
                else Pair = new Tile(Tile.TYPE.DRAGON, 1);
                if (Dragon[2] == 1) list.Add(new Tile(Tile.TYPE.DRAGON, 2));
                else Pair = new Tile(Tile.TYPE.DRAGON, 2);

                return new Set12Hand(list, Pair, checkBox_SelfDraw.Checked, true, NCP, D, 0, 0);
            }

            return null;
        }

        Set6Hand? Check7Pairs(int[] Circle, int[] Bamboo, int[] Character, int[] Wind, int[] Dragon, int NCP, int D)
        {
            for (int i = 0; i < 9; i++)
            {
                if ((Circle[i] == 0 || Circle[i] == 2) == false) return null;
                if ((Bamboo[i] == 0 || Bamboo[i] == 2) == false) return null;
                if ((Character[i] == 0 || Character[i] == 2) == false) return null;
            }
            for (int i = 0; i < 4; i++)
                if ((Wind[i] == 0 || Wind[i] == 2) == false) return null;
            for (int i = 0; i < 3; i++)
                if ((Dragon[i] == 0 || Dragon[i] == 2) == false) return null;
            //Found. Form hand
            bool First = true;
            List<Tile> list = new();
            Tile Pair = null;

            for (int i = 0; i < 9; i++)
                if (Circle[i] == 2)
                    if (First) { Pair = new Tile(Tile.TYPE.CIRCLE, i); First = false; }
                    else list.Add(new Tile(Tile.TYPE.CIRCLE, i));

            for (int i = 0; i < 9; i++)
                if (Bamboo[i] == 2)
                    if (First) { Pair = new Tile(Tile.TYPE.BAMBOO, i); First = false; }
                    else list.Add(new Tile(Tile.TYPE.BAMBOO, i));

            for (int i = 0; i < 9; i++)
                if (Character[i] == 2)
                    if (First) { Pair = new Tile(Tile.TYPE.CHARACTER, i); First = false; }
                    else list.Add(new Tile(Tile.TYPE.CHARACTER, i));

            for (int i = 0; i < 4; i++)
                if (Wind[i] == 2)
                    if (First) { Pair = new Tile(Tile.TYPE.WIND, i); First = false; }
                    else list.Add(new Tile(Tile.TYPE.WIND, i));

            for (int i = 0; i < 3; i++)
                if (Dragon[i] == 2)
                    if (First) { Pair = new Tile(Tile.TYPE.DRAGON, i); First = false; }
                    else list.Add(new Tile(Tile.TYPE.DRAGON, i));

            return new Set6Hand(list, Pair, checkBox_SelfDraw.Checked, true, NCP, D, 0, 0);
        }

        void CheckStandardHand_PairStage(int[] Circle, int[] Bamboo, int[] Character, int[] Wind, int[] Dragon, int NCP, int D, int H, bool Closed, List<CompleteHand> ListOfHand, List<int> ListOfScore, List<string> ListOfPatternList, List<SetOfTile> presetlist, Tile Pair)
        {
            if (presetlist.Count == 4)
            {
                Tile Winning = new(comboBox_WinningTile.SelectedIndex);
                //Check if a set contain the winning tile. For each such set, a separate complete hand is generated
                foreach (SetOfTile s in presetlist)
                {
                    if (s.StartTile.Type == Winning.Type)
                    {
                        if (s.Type == SetOfTile.SETTYPE.SEQUENCE)
                        {
                            if (s.StartTile.TypeIndex >= Winning.TypeIndex - 2 && s.StartTile.TypeIndex <= Winning.TypeIndex)
                            {
                                int Complete = 0;
                                bool IsSet = false;
                                if (!checkBox_SelfDraw.Checked) { s.Close = false; IsSet = true; }
                                if (s.StartTile.TypeIndex == Winning.TypeIndex - 1 || (s.StartTile.TypeIndex == 6 && Winning.TypeIndex == 6) || (s.StartTile.TypeIndex == 0 && Winning.TypeIndex == 2))
                                    Complete = 1;
                                Set4Hand Hand = new(Set4Hand.CloneList(presetlist), Pair.Clone(), checkBox_SelfDraw.Checked, Closed, NCP, D, 0, 0, Complete, comboBox_SeatWind.SelectedIndex, comboBox_RoundWind.SelectedIndex);
                                Hand.CalculateBase();
                                Hand.CalculatePattern(out string PatternList);
                                ListOfHand.Add(Hand);
                                ListOfPatternList.Add(PatternList);
                                ListOfScore.Add(Hand.BaseValue(H));
                                if (IsSet) s.Close = true;
                            }
                        }
                        else
                        {
                            if (s.StartTile.TypeIndex == Winning.TypeIndex)
                            {
                                bool IsSet = false;
                                if (!checkBox_SelfDraw.Checked) { s.Close = false; IsSet = true; }
                                Set4Hand Hand = new(Set4Hand.CloneList(presetlist), Pair.Clone(), checkBox_SelfDraw.Checked, Closed, NCP, D, 0, 0, 0, comboBox_SeatWind.SelectedIndex, comboBox_RoundWind.SelectedIndex);
                                Hand.CalculateBase();
                                Hand.CalculatePattern(out string PatternList);
                                ListOfHand.Add(Hand);
                                ListOfPatternList.Add(PatternList);
                                ListOfScore.Add(Hand.BaseValue(H));
                                if (IsSet) s.Close = true;
                            }
                        }
                    }
                }
                if (Pair.Type == Winning.Type && Pair.TypeIndex == Winning.TypeIndex)
                {
                    Set4Hand Hand = new(Set4Hand.CloneList(presetlist), Pair.Clone(), checkBox_SelfDraw.Checked, Closed, NCP, D, 0, 0, 1, comboBox_SeatWind.SelectedIndex, comboBox_RoundWind.SelectedIndex);
                    Hand.CalculateBase();
                    Hand.CalculatePattern(out string PatternList);
                    ListOfHand.Add(Hand);
                    ListOfPatternList.Add(PatternList);
                    ListOfScore.Add(Hand.BaseValue(H));
                }
                return;
            }

            for (int i = 0; i < 9; i++)
            {
                if (Circle[i] != 0)
                {
                    if (Circle[i] == 3)
                    {
                        presetlist.Add(new SetOfTile(new Tile(Tile.TYPE.CIRCLE, i), true, SetOfTile.SETTYPE.TRIPLET));
                        Circle[i] -= 3;
                        CheckStandardHand_PairStage(Circle, Bamboo, Character, Wind, Dragon, NCP, D, H, Closed, ListOfHand, ListOfScore, ListOfPatternList, presetlist, Pair);
                        presetlist.RemoveAt(presetlist.Count - 1);
                        Circle[i] += 3;
                    }
                    if (i > 6) return;
                    else if ((Circle[i + 1] > 0 && Circle[i + 2] > 0) == false) return;
                    else
                    {
                        presetlist.Add(new SetOfTile(new Tile(Tile.TYPE.CIRCLE, i), true, SetOfTile.SETTYPE.SEQUENCE));
                        Circle[i]--;
                        Circle[i + 1]--;
                        Circle[i + 2]--;
                        CheckStandardHand_PairStage(Circle, Bamboo, Character, Wind, Dragon, NCP, D, H, Closed, ListOfHand, ListOfScore, ListOfPatternList, presetlist, Pair);
                        presetlist.RemoveAt(presetlist.Count - 1);
                        Circle[i]++;
                        Circle[i + 1]++;
                        Circle[i + 2]++;
                    }
                    return;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (Bamboo[i] != 0)
                {
                    if (Bamboo[i] == 3)
                    {
                        presetlist.Add(new SetOfTile(new Tile(Tile.TYPE.BAMBOO, i), true, SetOfTile.SETTYPE.TRIPLET));
                        Bamboo[i] -= 3;
                        CheckStandardHand_PairStage(Circle, Bamboo, Character, Wind, Dragon, NCP, D, H, Closed, ListOfHand, ListOfScore, ListOfPatternList, presetlist, Pair);
                        presetlist.RemoveAt(presetlist.Count - 1);
                        Bamboo[i] += 3;
                    }
                    if (i > 6) return;
                    else if ((Bamboo[i + 1] > 0 && Bamboo[i + 2] > 0) == false) return;
                    else
                    {
                        presetlist.Add(new SetOfTile(new Tile(Tile.TYPE.BAMBOO, i), true, SetOfTile.SETTYPE.SEQUENCE));
                        Bamboo[i]--;
                        Bamboo[i + 1]--;
                        Bamboo[i + 2]--;
                        CheckStandardHand_PairStage(Circle, Bamboo, Character, Wind, Dragon, NCP, D, H, Closed, ListOfHand, ListOfScore, ListOfPatternList, presetlist, Pair);
                        presetlist.RemoveAt(presetlist.Count - 1);
                        Bamboo[i]++;
                        Bamboo[i + 1]++;
                        Bamboo[i + 2]++;
                    }
                    return;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (Character[i] != 0)
                {
                    if (Character[i] == 3)
                    {
                        presetlist.Add(new SetOfTile(new Tile(Tile.TYPE.CHARACTER, i), true, SetOfTile.SETTYPE.TRIPLET));
                        Character[i] -= 3;
                        CheckStandardHand_PairStage(Circle, Bamboo, Character, Wind, Dragon, NCP, D, H, Closed, ListOfHand, ListOfScore, ListOfPatternList, presetlist, Pair);
                        presetlist.RemoveAt(presetlist.Count - 1);
                        Character[i] += 3;
                    }
                    if (i > 6) return;
                    else if ((Character[i + 1] > 0 && Character[i + 2] > 0) == false) return;
                    else
                    {
                        presetlist.Add(new SetOfTile(new Tile(Tile.TYPE.CHARACTER, i), true, SetOfTile.SETTYPE.SEQUENCE));
                        Character[i]--;
                        Character[i + 1]--;
                        Character[i + 2]--;
                        CheckStandardHand_PairStage(Circle, Bamboo, Character, Wind, Dragon, NCP, D, H, Closed, ListOfHand, ListOfScore, ListOfPatternList, presetlist, Pair);
                        presetlist.RemoveAt(presetlist.Count - 1);
                        Character[i]++;
                        Character[i + 1]++;
                        Character[i + 2]++;
                    }
                    return;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (Wind[i] != 0)
                {
                    if (Wind[i] == 3)
                    {
                        presetlist.Add(new SetOfTile(new Tile(Tile.TYPE.WIND, i), true, SetOfTile.SETTYPE.TRIPLET));
                        Wind[i] -= 3;
                        CheckStandardHand_PairStage(Circle, Bamboo, Character, Wind, Dragon, NCP, D, H, Closed, ListOfHand, ListOfScore, ListOfPatternList, presetlist, Pair);
                        presetlist.RemoveAt(presetlist.Count - 1);
                        Wind[i] += 3;
                    }
                    return;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (Dragon[i] != 0)
                {
                    if (Dragon[i] == 3)
                    {
                        presetlist.Add(new SetOfTile(new Tile(Tile.TYPE.DRAGON, i), true, SetOfTile.SETTYPE.TRIPLET));
                        Dragon[i] -= 3;
                        CheckStandardHand_PairStage(Circle, Bamboo, Character, Wind, Dragon, NCP, D, H, Closed, ListOfHand, ListOfScore, ListOfPatternList, presetlist, Pair);
                        presetlist.RemoveAt(presetlist.Count - 1);
                        Dragon[i] += 3;
                    }
                    return;
                }
            }

        }

        void CheckStandardHand_Main(int[] Circle, int[] Bamboo, int[] Character, int[] Wind, int[] Dragon, int NCP, int D, int H, bool Closed, List<CompleteHand> ListOfHand, List<int> ListOfScore, List<string> ListOfPatternList, List<SetOfTile> presetlist)
        {
            Tile Pair;
            //Seek the pair. The an honor pair is found, then this is the only pair, i.e. no other possible complete standard hand can be found.
            for (int i = 0; i < 3; i++)
            {
                if (Dragon[i] == 2)
                {
                    Dragon[i] = 0;
                    Pair = new Tile(Tile.TYPE.DRAGON, i);
                    CheckStandardHand_PairStage(Circle, Bamboo, Character, Wind, Dragon, NCP, D, H, Closed, ListOfHand, ListOfScore, ListOfPatternList, presetlist, Pair);
                    Dragon[i] = 2;
                    return;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (Wind[i] == 2)
                {
                    Wind[i] = 0;
                    Pair = new Tile(Tile.TYPE.WIND, i);
                    CheckStandardHand_PairStage(Circle, Bamboo, Character, Wind, Dragon, NCP, D, H, Closed, ListOfHand, ListOfScore, ListOfPatternList, presetlist, Pair);
                    Wind[i] = 2;
                    return;
                }
            }

            for (int i = 0; i < 9; i++)
            {
                if (Circle[i] >= 2)
                {
                    Circle[i] -= 2;
                    Pair = new Tile(Tile.TYPE.CIRCLE, i);
                    CheckStandardHand_PairStage(Circle, Bamboo, Character, Wind, Dragon, NCP, D, H, Closed, ListOfHand, ListOfScore, ListOfPatternList, presetlist, Pair);
                    Circle[i] += 2;
                }
                if (Bamboo[i] >= 2)
                {
                    Bamboo[i] -= 2;
                    Pair = new Tile(Tile.TYPE.BAMBOO, i);
                    CheckStandardHand_PairStage(Circle, Bamboo, Character, Wind, Dragon, NCP, D, H, Closed, ListOfHand, ListOfScore, ListOfPatternList, presetlist, Pair);
                    Bamboo[i] += 2;
                }
                if (Character[i] >= 2)
                {
                    Character[i] -= 2;
                    Pair = new Tile(Tile.TYPE.CHARACTER, i);
                    CheckStandardHand_PairStage(Circle, Bamboo, Character, Wind, Dragon, NCP, D, H, Closed, ListOfHand, ListOfScore, ListOfPatternList, presetlist, Pair);
                    Character[i] += 2;
                }
            }
        }

        private void button_Evaluate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_NonContentPattern.Text, out int NCP))
            {

            }
            else return;
            if (int.TryParse(textBox_Dora.Text, out int D))
            {

            }
            else return;
            if (int.TryParse(textBox_Honba.Text, out int H))
            {

            }
            else return;

            //Identify set
            int preset = 0;
            List<SetOfTile> presetlist = new();
            if (checkBox_Set.Checked) preset = comboBox_NumOfSet.SelectedIndex + 1;

            if (preset > 0)
            {
                for (int i = 0; i < preset; i++)
                {
                    presetlist.Add(new SetOfTile(new Tile(SetList[i].SelectedIndex), SetTypeList[i].SelectedIndex / 3 == 1, (SetOfTile.SETTYPE)(SetTypeList[i].SelectedIndex % 3)));
                }
            }

            // Unformed/free tiles. Tiles from hand + the winning tile
            int[] Circle = new int[9];
            int[] Bamboo = new int[9];
            int[] Character = new int[9];
            int[] Wind = new int[4];
            int[] Dragon = new int[3];

            Tile.GetTileDetail(comboBox_WinningTile.SelectedIndex, out int t1, out int ti1);
            if (t1 == 0) Circle[ti1]++;
            else if (t1 == 1) Bamboo[ti1]++;
            else if (t1 == 2) Character[ti1]++;
            else if (t1 == 3) Wind[ti1]++;
            else Dragon[ti1]++;

            for (int i = 0; i < 13 - preset * 3; i++)
            {
                Tile.GetTileDetail(HandSet[i].SelectedIndex, out int t, out int ti);
                if (t == 0) Circle[ti]++;
                else if (t == 1) Bamboo[ti]++;
                else if (t == 2) Character[ti]++;
                else if (t == 3) Wind[ti]++;
                else Dragon[ti]++;
            }

            List<CompleteHand> ListOfHand = new();
            List<int> ListOfScore = new();
            List<string> ListOfPatternList = new();

            //Check for 13 orphans/7 pairs
            if (preset == 0)
            {
                Set12Hand? tempHand = Check13Orphans(Circle, Bamboo, Character, Wind, Dragon, NCP, D);
                if (tempHand != null)
                {
                    tempHand.CalculateBase();
                    tempHand.CalculatePattern(out string tempPatternList);
                    ShowHandScore(NCP, D, H, tempHand, tempPatternList);

                    return;
                }

                Set6Hand? tempHand2 = Check7Pairs(Circle, Bamboo, Character, Wind, Dragon, NCP, D);
                if (tempHand2 != null)
                {
                    tempHand2.CalculateBase();
                    tempHand2.CalculatePattern(out string tempPatternList1);
                    ListOfHand.Add(tempHand2);
                    ListOfPatternList.Add(tempPatternList1);
                    ListOfScore.Add(tempHand2.BaseValue(H));
                }

            }

            //Standard hand
            bool Closed = true;
            foreach (SetOfTile s in presetlist)
                if (!s.Close) { Closed = false; break; }

            CheckStandardHand_Main(Circle, Bamboo, Character, Wind, Dragon, NCP, D, H, Closed, ListOfHand, ListOfScore, ListOfPatternList, presetlist);

            if (ListOfScore.Count > 0)
            {
                label1.Text = "***";
                int index = ListOfScore.IndexOf(ListOfScore.Max());
                ShowHandScore(NCP, D, H, ListOfHand[index], ListOfPatternList[index]);
            }
            else
            {
                label1.Text = "No complete hand can be found.";
            }
        }

        private void ShowHandScore(int NCP, int D, int H, CompleteHand Hand, string PatternList)
        {
            textBox_Base.Text = (5 + H + (Hand.Draw ? 1 : 0) + (Hand.Closed ? 1 : 0) + Hand.Base).ToString();
            textBox_Pattern.Text = (Hand.PatternCount + NCP + D).ToString();
            richTextBox_PatternList.Text = PatternList;
            textBox_BaseValue.Text = Hand.BaseValue(H).ToString();
        }
    }
}