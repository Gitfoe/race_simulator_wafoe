using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    public class Kart : IEquipment
    {
        // Properties
        public int Quality { get; set; }
        public int Performance { get; set; }
        public int Speed { get; set; }
        public bool IsBroken { get; set; }

        // Constructors
        public Kart(int quality)
        {
            Quality = quality;
        }
    }
}
