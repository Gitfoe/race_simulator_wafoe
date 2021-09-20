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
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }
    }
    // Enmumerations
    public enum TeamColors { Red, Green, Yellow, Grey, Blue };
}
