using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Interfaces
{
    public interface IEquipment // Racemiddel
    {
        int Quality { get; }
        int Performance { get; }
        int Speed { get; }
        bool IsBroken { get; }
    }
}
