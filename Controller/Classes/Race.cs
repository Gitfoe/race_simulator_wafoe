using Model;
using Model.Classes;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
                participant.Equipment.Quality = _random.Next(1, 10);
                participant.Equipment.Performance = _random.Next(1, 10);
            }
        }

        // Participant moving methods
        private void MoveParticipants()
        {
            // Loops through the participants and adds the new total speed to the SectionData the participant is on
            foreach (IParticipant participant in Participants)
            {
                Section sectionParticipantIsOn = FindSectionOfParticipant(participant);
                int totalSpeedOfParticipant = participant.Equipment.Performance * participant.Equipment.Speed;
                if (_positions[sectionParticipantIsOn].Left == participant)
                {
                    if ((_positions[sectionParticipantIsOn].DistanceLeft + totalSpeedOfParticipant) < 100)
                    { // If the new distance is lower than the maximum track distance, just add the new distance
                        _positions[sectionParticipantIsOn].DistanceLeft += totalSpeedOfParticipant;
                    }
                    else
                    { // If the participant should move to the next section, call this method
                        PlaceParticipantOnNextSection(sectionParticipantIsOn, Placement.Left, participant);
                    }
                }
                else
                {
                    if ((_positions[sectionParticipantIsOn].DistanceRight + totalSpeedOfParticipant) < 100)
                    { // If the new distance is lower than the maximum track distance, just add the new distance
                        _positions[sectionParticipantIsOn].DistanceRight += totalSpeedOfParticipant;
                    }
                    else
                    { // If the participant should move to the next section, call this method
                        PlaceParticipantOnNextSection(sectionParticipantIsOn, Placement.Right, participant);
                    }
                }
            }
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

        private void PlaceParticipantOnNextSection(Section sectionParticipantIsOn, Placement placement, IParticipant participant)
        { // Places a participant on the next section
            bool cannotPlaceSection = true; // Begin with true and hopefully make it false within this method
            bool executedElseIf = false; // Bugfix to not place participants at the start if section is full, check if the else if has executed
            SectionData foundSectionData = null;
            foreach (var item in _positions) // Loops through every position in _positions
            {
                if (item.Key == sectionParticipantIsOn)
                { // Searches the section in _positions and saves the Section (key) and SectionData (value) if the participant on the section is found
                    foundSectionData = item.Value;
                }
                else if (foundSectionData != null)
                { // Executes on the next loop, if the loop is not at the end, once the section has been found
                    cannotPlaceSection = PlaceParticipantIfPossible(placement, participant, foundSectionData, item.Value);
                    executedElseIf = true;
                    break; // Stop the loop from continuing further without reason
                }
            }
            // If the loop ended prematurely, it means it should check the first section instead
            if (cannotPlaceSection == true && executedElseIf == false)
            { // Check if the participant couldn't be placed because the next section was full, then try to place the participant on the first section
                cannotPlaceSection = PlaceParticipantIfPossible(placement, participant, foundSectionData, _positions.First().Value);
            }
            // Check if the participant couldn't be placed even on the first section because it was full, and then max out the distance
            if (cannotPlaceSection == true)
            {
                foundSectionData.DistanceLeft = 100;
            }
        }

        private bool PlaceParticipantIfPossible(Placement placement, IParticipant participant, SectionData previousSectionData, SectionData nextSectionData)
        { // Places the participant on the next track and also fixes the distance data
            bool leftOccupied = CheckIfPositionIsOccupied(nextSectionData, Placement.Left);
            bool rightOccupied = CheckIfPositionIsOccupied(nextSectionData, Placement.Right);
            if (placement == Placement.Left && leftOccupied == false)
            { // If the participant was on the left and the next left position is not occupied, place participant there
                nextSectionData.Left = participant;
                previousSectionData.Left = null; // Remove participant from old position
                AddKartDistanceToSectionData(previousSectionData, nextSectionData, Placement.Left, Placement.Left, participant); // Finally, add the kart distance to the correct section
            }
            else if (placement == Placement.Right && rightOccupied == false)
            { // If the participant was on the right and the next right position is not occupied, place participant there
                nextSectionData.Right = participant;
                previousSectionData.Right = null; // Remove participant from old position
                AddKartDistanceToSectionData(previousSectionData, nextSectionData, Placement.Right, Placement.Right, participant); // Finally, add the kart distance to the correct section
            }
            else if (placement == Placement.Left && leftOccupied == true && rightOccupied == false)
            { // If the participant was on the left and the next left position is occupied, place participant on the right
                nextSectionData.Right = participant;
                previousSectionData.Left = null; // Remove participant from old position
                AddKartDistanceToSectionData(previousSectionData, nextSectionData, Placement.Left, Placement.Right, participant); // Finally, add the kart distance to the correct section
            }
            else if (placement == Placement.Right && leftOccupied == false && rightOccupied == true)
            { // If the participant was on the right and the next right position is occupied, place participant on the left
                nextSectionData.Left = participant;
                previousSectionData.Right = null; // Remove participant from old position
                AddKartDistanceToSectionData(previousSectionData, nextSectionData, Placement.Right, Placement.Left, participant); // Finally, add the kart distance to the correct section
            }
            else
            {
                return true; // If none of the above if's succeeded, it returns true
            }
            return false; // If something succeeded, it returns false
        }

        private void AddKartDistanceToSectionData(SectionData previousSectionData, SectionData nextSectionData, Placement previousPlacement, Placement nextPlacement, IParticipant participant)
        { // Adds the new distance to the correct SectionData
          // If the participant was placed on the next section, adds the remainder of the distance to the next section, so if the distance was 90 and 20 was to be added, the outcome is 30
            if (previousPlacement == Placement.Left)
            {
                if (nextPlacement == Placement.Left)
                {
                    nextSectionData.DistanceLeft = (previousSectionData.DistanceLeft + (participant.Equipment.Performance * participant.Equipment.Speed)) % 100; // Uses the formula for speed calc
                }
                else // Placement.Right
                {
                    nextSectionData.DistanceRight = (previousSectionData.DistanceLeft + (participant.Equipment.Performance * participant.Equipment.Speed)) % 100;
                }
            }
            else // Placement.Right
            {
                if (nextPlacement == Placement.Left)
                {
                    nextSectionData.DistanceLeft = (previousSectionData.DistanceRight + (participant.Equipment.Performance * participant.Equipment.Speed)) % 100;
                }
                else // Placement.Right
                {
                    nextSectionData.DistanceRight = (previousSectionData.DistanceRight + (participant.Equipment.Performance * participant.Equipment.Speed)) % 100;
                }
            }
        }

        private bool CheckIfPositionIsOccupied(SectionData sectionData, Placement placement)
        { // Checks if a placement in SectionData is already occupied (returns true) or not (returns false)
            if (placement == Placement.Left)
            {
                if (sectionData.Left == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (sectionData.Right == null)
                {
                    return false;
                }
                else
                {
                    return true;
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
