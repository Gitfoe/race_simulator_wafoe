using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Interfaces
{
    public interface IEquipment // Racemiddel
    {
        public int Quality { get; set; }
        public int Performance { get; set; }
        public int Speed { get; set; }
        public bool IsBroken { get; set; }
    }
}
