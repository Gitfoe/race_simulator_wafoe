using Model.Interfaces;
using System.Collections.Generic;

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
            if (Tracks.Count != 0) // Checks if the queue is empty or not
            {
                return Tracks.Dequeue(); // Dequeues if it is not empty
            }
            else
            {
                return null; // Returns null if it is empty
            }
        }
    }
}
