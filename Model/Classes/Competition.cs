using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    public class Competition
    {
        // Properties
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        // Methods
        public Track NextTrack()
        {

        }
    }
}
