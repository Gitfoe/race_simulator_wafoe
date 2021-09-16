using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    class Driver : IParticipant
    {
        // Constructors
        public Driver(string _name, int _points, IEquipment _equipment, TeamColors _teamColor)
        {
            Name = _name;
            Points = _points;
            Equipment = _equipment;
            TeamColor = _teamColor;
        }
        // Properties
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }
    }
}
