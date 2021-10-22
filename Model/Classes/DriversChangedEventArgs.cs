using System;

namespace Model.Classes
{
    public class DriversChangedEventArgs : EventArgs
    {       
        // Properties
        public Track Track { get; set; }
    }
}
