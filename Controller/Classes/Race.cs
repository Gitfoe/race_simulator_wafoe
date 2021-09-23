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

        private void PlaceParticipantsOnStartGrids(Track track, List<IParticipant> participants)
        {
            List<IParticipant> tempParticipants = FixParticipentOrder(participants, track); // Make fixed copy of participants list

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

        private int CountCertainSectionTypes(LinkedList<Section> sectionList, Section.SectionTypes toCount)
        { // Counts a given SectionType from a (linked)list of Section objects
            int counter = 0;
            foreach (Section section in sectionList)
            {
                if (section.SectionType == toCount)
                {
                    counter++;
                }
            }
            return counter;
        }

        private List<IParticipant> FixParticipentOrder(List<IParticipant> participants, Track track)
        { // Fixes the order of participents on the grid so they will start at the front positions near the finish line and not at the last positions
            List<IParticipant> outputParticipants = new List<IParticipant>(participants);
            int countOfStartGridPositions = CountCertainSectionTypes(track.Sections, Section.SectionTypes.StartGrid) * 2;
            IParticipant tempOutputParticipant; // Use the temporary participant variable for swapping

            if (outputParticipants.Count > countOfStartGridPositions)
            { // Checks if there are more participants than start grid positions, and if so, remove these participants
                outputParticipants.RemoveRange(countOfStartGridPositions, outputParticipants.Count - countOfStartGridPositions);
            }

            if (outputParticipants.Count % 2 == 1)
            { // If the count of participants is not even, add a null value to not break the flipping in the next for loop
                outputParticipants.Add(null); 
            }

            for (int i = 0; i < outputParticipants.Count - 1; i++)
            {
                if (i % 2 == 0) // Check if the order of the participant is even
                { // Swap the participant with the place right behind it
                    tempOutputParticipant = outputParticipants[i];
                    outputParticipants[i] = outputParticipants[i + 1];
                    outputParticipants[i + 1] = tempOutputParticipant;
                }
            }

            for (int i = outputParticipants.Count; i < countOfStartGridPositions; i++)
            { // Add null spaces to the participants list if some grid positions are not filled
                outputParticipants.Add(null);
            }

            outputParticipants.Reverse(); // Reverse the participant order, because the sections follow themselves and otherwise would be in a wrong order

            return outputParticipants;
        }

        private SectionData AddParticipantsToStartGridSectionData(List<IParticipant> participants, SectionData currentSectionData)
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
