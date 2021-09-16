using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    public class Kart : IEquipment
    {
        // Constructors
        public Kart(int _quality, int _performance, int _speed, bool _isBroken)
        {
            Quality = _quality;
            Performance = _performance;
            Speed = _speed;
            IsBroken = _isBroken;
        }
        // Properties
        public int Quality { get; set; }
        public int Performance { get; set; }
        public int Speed { get; set; }
        public bool IsBroken { get; set; }
    }
}
