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
        public Race(Track track, List<IParticipant> participants)
        {
            // Initializes the properties of this class
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
            PlaceParticipantsOnStartGrids(Track, Participants);
        }

        // Methods
        public SectionData GetSectionData(Section section)
        {
            // Checks if the key "section" exists in the _positions dictionary, and if it does not exist, adds the position and creates a SectionData for it
            if (!_positions.ContainsKey(section))
            {
                _positions.Add(section, new SectionData());
            }
            return _positions[section];
        }

        public void PlaceParticipantsOnStartGrids(Track track, List<IParticipant> participants)
        {
            List<IParticipant> tempParticipants = new List<IParticipant>(participants); // Make copy of participants list
            foreach (Section section in track.Sections)
            {
                SectionData currentSectionData = GetSectionData(section); // Fills the _positions dictionary with the track and empty SectionData instances
                // Find sections that are start grids and add them, together with the driver SectionData positions, to the _positions field
                if (section.SectionType == Section.SectionTypes.StartGrid)
                {
                    _positions[section] = AddParticipantsToStartGridSectionData(tempParticipants, currentSectionData);
                    if (tempParticipants.Count > 1) // Make sure to remove participants from the temporary list, so it doesn't place the participants twice
                    {
                        tempParticipants.RemoveRange(0, 2);
                    }
                    else if (tempParticipants.Count == 1) // If only 1 participant is left, remove the last one
                    {
                        tempParticipants.RemoveAt(0);
                    }
                }
            }
        }

        public SectionData AddParticipantsToStartGridSectionData(List<IParticipant> participants, SectionData currentSectionData)
        {
            // Adds all the participants to the section for the start grid
            for (int i = 0; i < participants.Count; i++) // Loop through all the participants
            {
                if (i == 0) // If it's the first participant for a section, add it to the left participant position
                {
                    currentSectionData.Left = participants[i];
                }
                else // If it's not the first participant for a section, add it to the right participant position
                {
                    currentSectionData.Right = participants[i];
                    break; // The brake stops the for loop continuing for no reason
                }
            }
            return currentSectionData;
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
