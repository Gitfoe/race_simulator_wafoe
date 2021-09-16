using Model.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Interfaces
{
    public interface IParticipant // Deelnemer
    {
        string Name { get; }
        int Points { get; }
        IEquipment Equipment { get; }
        TeamColors TeamColor { get; }
    }
    // Enmumerations
    public enum TeamColors { Red, Green, Yellow, Grey, Blue };
}
