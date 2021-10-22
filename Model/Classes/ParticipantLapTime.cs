using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    public class ParticipantLapTime
    {
        // Properties
        public string Name { get; set; }
        public int Lap { get; set; }
        public TimeSpan Time { get; set; }

        // Constructor
        public ParticipantLapTime(string name, int lap, TimeSpan time)
        {
            Name = name;
            Lap = lap;
            Time = time;
        }
    }
}
