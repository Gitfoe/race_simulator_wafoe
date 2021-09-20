using Model;
using Model.Classes;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller.Classes
{
    public class Race
    {
        // Attributes
        private Random _random;
        private Dictionary<Section, SectionData> _positions;

        // Properties
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        // Constructors
        public Race(Track track, List<IParticipant> list)
        {
            // Initializes the properties of this class
            Track = track;
            Participants = list;
            _random = new Random(DateTime.Now.Millisecond);
        }

        // Methods
        public SectionData GetSectionData(Section section)
        {
            // Checks if the key "section" exists in the dictionary and if it exists returns the SectionData for that key section
            // If it doesn't exist, it creates a new SectionData for that section and returns the SectionData for that key section
            if (!_positions.ContainsKey(section))
            {
                _positions.Add(section, new SectionData());
            }
            return _positions[section];
        }

        public void RandomizeEquipment()
        {
            // Iterates over the Participant list and generates random values for Quality and Performance for each driver
            foreach (IParticipant participant in Participants)
            {
                participant.Equipment.Quality = _random.Next();
                participant.Equipment.Performance = _random.Next();
            }
        }
    }
}
