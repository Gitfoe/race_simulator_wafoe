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
            PlaceParticipantsOnStartGrids(Track, Participants);
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

        public void PlaceParticipantsOnStartGrids(Track track, List<IParticipant> participants)
        {
            List<SectionData> sectionDataStartGridList = AddParticipantsToStartGridSectionData(participants);
            int counter = 0;
            
            foreach (Section section in track.Sections)
            {
                // Find sections that are start grids and add them, together with the driver SectionData positions, to the _positions field
                // making sure in the if statement that it does not fill the StartGrid positions if there are less drivers than positions
                if (section.SectionType == Section.SectionTypes.StartGrid && sectionDataStartGridList.Count > counter)
                {
                    _positions.Add(section, sectionDataStartGridList[counter]);
                    counter++;
                }
            }
        }

        public List<SectionData> AddParticipantsToStartGridSectionData(List<IParticipant> participants)
        {
            // Adds all the participants to the section for the start grid
            List<SectionData> sectionDataStartGridList = new List<SectionData>(); // Create the SectionData list of participants
            int counter = 0;
            for (int i = 0; i < participants.Count; i++) // Loop through all the participants
            {
                if (i % 2 == 0) // If it's the first participant for a section, add it to the left participant position
                {
                    sectionDataStartGridList.Add(new SectionData() { Left = participants[i] });
                }
                else // If it's not the first participant for a section, add it to the right participant position
                {
                    sectionDataStartGridList[counter].Right = participants[i];
                }
                counter++;
            }
            return sectionDataStartGridList;
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
