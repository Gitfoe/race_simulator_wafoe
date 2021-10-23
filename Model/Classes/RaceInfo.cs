using Model.Interfaces;
using System;
using System.Collections.Generic;

namespace Model.Classes
{
    public class RaceInfo
    {
        // Properties
        public List<IParticipant> Participants { get; set; }
        public int AmountOfLaps { get; set; }
        public int AmountOfSections { get; set; }
        public int AmountOfRacers { get; set; }
        public List<ParticipantTimes> ParticipantLapTimes { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan RaceTimer { get; set; }

        // Constructor
        public RaceInfo(List<IParticipant> participants)
        {
            Participants = participants;
            AmountOfRacers = Participants.Count; // Delcare the amount of racers from the given List<IParticipant>
            ParticipantLapTimes = new List<ParticipantTimes>();
        }
    }
}
