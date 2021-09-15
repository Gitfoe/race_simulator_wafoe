using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Interfaces
{
    interface IEquipment
    {
        int Quality { get; }
        int Performance { get; }
        int Speed { get; }
        bool IsBroken { get; }
    }
}
