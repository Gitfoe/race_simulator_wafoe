using Model;
using Model.Classes;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Controller.Classes
{
    public class Race
    {
        // Attributes
        private Random _random;
        private Dictionary<Section, SectionData> _positions;
        private Timer _timer;

        // Properties
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        // Events
        public event EventHandler<DriversChangedEventArgs> DriversChanged;

        // Constructors
        public Race(Track track, List<IParticipant> participants)
        {
            // Initialize the properties of this class
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
            _timer = new Timer(500); // 0.5 seconden

            // Call methods
            PlaceParticipantsOnStartGrids(Track, Participants);
            RandomizeEquipment();

            // Subscribe events
            _timer.Elapsed += OnTimedEvent; // Subscribe timer.Elapsed to OnTimedEvent
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
                participant.Equipment.Quality = _random.Next(1, 5);
                participant.Equipment.Performance = _random.Next(1, 5);
            }
        }

        // Participant moving methods
        private void MoveParticipants()
        {
            Dictionary<IParticipant, int> totalSpeedOfParticipant = CalculateTotalSpeedForParticipants(Participants);
            // Loops through the participants and adds the new total speed to the SectionData the participant is on
            foreach (IParticipant participant in Participants)
            {
                Section sectionParticipantIsOn = FindSectionOfParticipant(participant);
                if (_positions[sectionParticipantIsOn].Left == participant)
                {
                    if (_positions[sectionParticipantIsOn].DistanceLeft + totalSpeedOfParticipant[participant] < 100)
                    { // Adds the speed to the correct distance
                        _positions[sectionParticipantIsOn].DistanceLeft += totalSpeedOfParticipant[participant];
                    }
                    else
                    { // Adds the remainder of the distance, so if the distance was 90 and 20 was to be added, the outcome is 30
                        _positions[sectionParticipantIsOn].DistanceLeft = 100 % _positions[sectionParticipantIsOn].DistanceLeft + totalSpeedOfParticipant[participant];
                        bool cannotPlaceSection = PlaceParticipantOnNextSection(sectionParticipantIsOn, Placement.Left, participant);
                        if (cannotPlaceSection == true)
                        { // If it couldn't place the participant because places are occupied, hold the racer and put distance to max (100)
                            _positions[sectionParticipantIsOn].DistanceLeft = 100; 
                        }
                    }
                }
                else
                {
                    if (_positions[sectionParticipantIsOn].DistanceRight + totalSpeedOfParticipant[participant] < 100)
                    { // Adds the speed to the correct distance
                        _positions[sectionParticipantIsOn].DistanceRight += totalSpeedOfParticipant[participant];
                    }
                    else
                    { // Adds the remainder of the distance, so if the distance was 90 and 20 was to be added, the outcome is 30
                        _positions[sectionParticipantIsOn].DistanceRight = 100 % _positions[sectionParticipantIsOn].DistanceRight + totalSpeedOfParticipant[participant];
                        bool cannotPlaceSection = PlaceParticipantOnNextSection(sectionParticipantIsOn, Placement.Right, participant);
                        if (cannotPlaceSection == true)
                        { // If it couldn't place the participant because places are occupied, hold the racer and put distance to max (100)
                            _positions[sectionParticipantIsOn].DistanceRight = 100;
                        }
                    }
                }
            }
        }

        public Dictionary<IParticipant, int> CalculateTotalSpeedForParticipants(List<IParticipant> participants)
        { // Calculates the total karts speed in meters per <timeramount> by multiplying performance with speed for all participants
            Dictionary<IParticipant, int> totalSpeedOfParticipant = new Dictionary<IParticipant, int>();
            foreach (IParticipant participant in participants)
            {
                totalSpeedOfParticipant.Add(participant, participant.Equipment.Performance * participant.Equipment.Speed);
            }
            return totalSpeedOfParticipant;
        }

        public Section FindSectionOfParticipant(IParticipant participant)
        { // Returns the section a participant is on
            foreach (var item in _positions)
            {
                if (participant == item.Value.Left || participant == item.Value.Right) // Participant can be on left or right section
                {
                    return item.Key;
                }
            }
            throw new InvalidProgramException(); // If it cannot find the participant, throw an exception
        }

        private bool PlaceParticipantOnNextSection(Section sectionParticipantIsOn, Placement placement, IParticipant participant)
        { // Places a participant on the next section
            bool cannotPlaceSection = true; // Begin with true and hopefully make it false within this method
            SectionData firstSectionData = null;
            SectionData foundSectionData = null;
            foreach (var item in _positions) // Loops through every position in _positions
            {
                if (firstSectionData == null)
                { // Save the first SectionData if needed
                    firstSectionData = item.Value;
                }
                else if (item.Key == sectionParticipantIsOn)
                { // Searches the section in _positions and saves the SectionData if it is found
                    foundSectionData = item.Value;
                }
                else if (foundSectionData != null)
                { // Executes on the next loop once the section has been found and if it isn't the last section
                    cannotPlaceSection = PlaceParticipantIfPossible(placement, participant, foundSectionData, item.Value);
                    break; // Stop the loop from continuing further without reason
                }
            }
            // If the section was the last section, place the participant on the first section
            if (cannotPlaceSection == true)
            {
                bool cannotPlaceSectionAgain = PlaceParticipantIfPossible(placement, participant, foundSectionData, firstSectionData);
                if (cannotPlaceSectionAgain == true)
                { // If it couldn't place the participant again (meaning, the first section is also full), return true
                    return cannotPlaceSectionAgain;
                }
            }
            return cannotPlaceSection; // If the participant is successfully placed somewhere, return false
        }

        private bool PlaceParticipantIfPossible(Placement placement, IParticipant participant, SectionData foundSectionData, SectionData currentSectionData)
        {
            bool cannotPlaceSection = true;
            bool leftOccupied = CheckIfPositionIsOccupied(currentSectionData, Placement.Left);
            bool rightOccupied = CheckIfPositionIsOccupied(currentSectionData, Placement.Right);
            if (placement == Placement.Left && leftOccupied == false)
            { // If the participant was on the left and the next left position is not occupied, place participant there
                currentSectionData.Left = participant;
                foundSectionData.Left = null; // Remove participant from old position
                cannotPlaceSection = false; // If it placed the participant, set this to false
            }
            else if (placement == Placement.Right && rightOccupied == false)
            { // If the participant was on the right and the next right position is not occupied, place participant there
                currentSectionData.Right = participant;
                foundSectionData.Right = null; // Remove participant from old position
                cannotPlaceSection = false; // If it placed the participant, set this to false
            }
            else if (placement == Placement.Left && leftOccupied == true && rightOccupied == false)
            { // If the participant was on the left and the next left position is occupied, place participant on the right
                currentSectionData.Right = participant;
                foundSectionData.Left = null; // Remove participant from old position
                cannotPlaceSection = false; // If it placed the participant, set this to false
            }
            else if (placement == Placement.Right && leftOccupied == false && rightOccupied == true)
            { // If the participant was on the right and the next right position is occupied, place participant on the left
                currentSectionData.Left = participant;
                foundSectionData.Right = null; // Remove participant from old position
                cannotPlaceSection = false; // If it placed the participant, set this to false
            } // If none of the above if's succeeded, it returns true, otherwise false
            return cannotPlaceSection;
        }

        private bool CheckIfPositionIsOccupied(SectionData sectionData, Placement placement)
        { // Checks if a placement in SectionData is already occupied (returns true) or not (returns false)
            if (placement == Placement.Left)
            {
                if (sectionData.Left == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (sectionData.Right == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // Event handler methods
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            MoveParticipants();
            DriversChanged(source, new DriversChangedEventArgs() { Track = this.Track });
        }

        public void Start()
        { // This method starts the timer
            _timer.AutoReset = true; // Raise the Elapsed event repeatedly (true)
            _timer.Enabled = true;
        }
    }
    public enum Placement
    {
        Left,
        Right
    }
}
