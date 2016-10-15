using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Sturgeon
{
    public class TeamModel
    {
        public string ErrorMessage { get; set; }

        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string Password { get; set; }

        public int Slot1 { get; set; }

        public int Slot2 { get; set; }

        public int Slot3 { get; set; }

        public int Slot4 { get; set; }

        public int Slot5 { get; set; }

        public int Slot6 { get; set; }

        public int Slot7 { get; set; }

        public int Slot8 { get; set; }

        public int Slot9 { get; set; }

        public int Slot10 { get; set; }

        public int Score
        {
            get
            {
                return Slot1 + Slot2 + Slot3 + Slot4 + Slot5 + Slot6 + Slot7 + Slot8 + Slot9;
            }
        }

        public void SetScore(int slot, int score)
        {
            switch (slot)
            {
                case 1:
                    Slot1 = score;
                    break;
                case 2:
                    Slot2 = score;
                    break;
                case 3:
                    Slot3 = score;
                    break;
                case 4:
                    Slot4 = score;
                    break;
                case 5:
                    Slot5 = score;
                    break;
                case 6:
                    Slot6 = score;
                    break;
                case 7:
                    Slot7 = score;
                    break;
                case 8:
                    Slot8 = score;
                    break;
                case 9:
                    Slot9 = score;
                    break;
                case 10:
                    Slot10 = score;
                    break;
            }
        }

        public int GetScore(int slot)
        {
            switch (slot)
            {
                case 1:
                    return Slot1;
                case 2:
                    return Slot2;
                case 3:
                    return Slot3;
                case 4:
                    return Slot4;
                case 5:
                    return Slot5;
                case 6:
                    return Slot6;
                case 7:
                    return Slot7;
                case 8:
                    return Slot8;
                case 9:
                    return Slot9;
                case 10:
                    return Slot10;
            }
            return 0;
        }
    }
}
