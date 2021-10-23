using Model.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Interfaces
{
    public interface IParticipant // Deelnemer
    {
        // Properties
        public string Name { get; set; }
        public int Points { get; set; }
        public string PositionOnTrack { get; set; }
        public string PositionInCompetition { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }
    }
    // Enmumerations
    // public enum TeamColors { Red, Green, Yellow, Grey, Blue }; // Original TeamColors
    public enum TeamColors { Mario, Toad, Luigi, Koopa, Peach, DKJunior, Wafoe, Bowser }; // I hope this is accepted - I did this for fun!
}
