using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    public class ParticipantTimes
    {
        // Properties
        public string Name { get; set; }
        public int Lap { get; set; }
        public TimeSpan LapTime { get; set; }
        public TimeSpan RaceTime { get; set; }
        public Track Track { get; set; }

        // Constructors
        public ParticipantTimes(string name, int lap, TimeSpan lapTime) // For RaceInfo
        {
            Name = name;
            Lap = lap;
            LapTime = lapTime;
        }
        public ParticipantTimes(string name, Track track, TimeSpan raceTime) // For CompetitionInfo
        {
            Name = name;
            Track = track;
            RaceTime = raceTime;
        }
    }
}
