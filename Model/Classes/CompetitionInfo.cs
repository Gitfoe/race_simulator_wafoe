using System.Collections.Generic;

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
