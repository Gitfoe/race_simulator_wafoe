using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    public class Driver : IParticipant
    {
        // Properties
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }

        // Constructors
        public Driver(string name, IEquipment equipment, TeamColors teamColor)
        {
            Name = name;
            Points = 0;
            Equipment = equipment;
            TeamColor = teamColor;
        }
    }
}
