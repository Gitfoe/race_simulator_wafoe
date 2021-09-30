using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    public class DriversChangedEventArgs : EventArgs
    {       
        // Properties
        public Track Track { get; set; }
    }
}
