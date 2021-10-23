using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    public class CompetitionInfo
    {
        // Properties
        public List<ParticipantTimes> ParticipantRaceTimes { get; set; }

        // Constructor
        public CompetitionInfo()
        {
            ParticipantRaceTimes = new List<ParticipantTimes>();
        }
    }
}
