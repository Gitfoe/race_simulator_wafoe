using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    public class Competition // Competitie
    {
        // Properties
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        // Constructors
        public Competition() // Initialize properties
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
        }
        // Methods
        public Track NextTrack()
        {
            // Removes and returns the object at the beginning of the Queue<Track>
            return Tracks.Dequeue();
        }
    }
}
