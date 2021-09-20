using Model;
using Model.Classes;
using Model.Interfaces;
using static Model.Classes.Section;
using System;
using System.Collections.Generic;
using System.Text;


namespace Controller.Classes
{
    public static class Data // Static class, no instance can be made
    {
        // Properties
        public static Competition GrandPrix { get; set; }
        public static Race CurrentRace { get; set; }

        // Methods
        public static void Initialize() // Initialize a new competition
        {
            GrandPrix = new Competition();
            AddParticipants(); // Call the AddParticipant method to add partiticpants to the Grand Prix
            AddTracks(); // Call the AddTracks method to add tracks to the Grand Prix
        }
        private static void AddParticipants() // Add 3 participants to the race
        {
            IParticipant[] DriverList = // Create list of drivers
            {
                new Driver("Mario", 0, new Kart(8, 10, 16, false), TeamColors.Red),
                new Driver("Waluigi", 0, new Kart(5, 13, 15, false), TeamColors.Blue),
                new Driver("Yoshi", 0, new Kart(4, 17, 12, false), TeamColors.Green)
            };
            GrandPrix.Participants.AddRange(DriverList); // Add the list of new Driver classes to the Participants list
        }
        private static void AddTracks()
        {
            // Method that adds all the tracks in the list TrackList to the queue of GrandPrix
            Track[] TrackList = // Create list of tracks
            {
                new Track("Rainbow Road", new Section.SectionTypes[] {
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.Finish,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid } ),
                new Track("Yoshi Circuit", new Section.SectionTypes[] {
                    SectionTypes.StartGrid,
                    SectionTypes.Finish,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.StartGrid } ),
                new Track("Coconut Mall", new Section.SectionTypes[] {
                    SectionTypes.RightCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.RightCorner,
                    SectionTypes.Finish,
                    SectionTypes.RightCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.RightCorner,
                    SectionTypes.StartGrid }
                )
            };
            foreach (Track track in TrackList) // Enqueue all the tracks
            {
                GrandPrix.Tracks.Enqueue(track);
            }
        }
        public static void NextRace()
        {
            if (GrandPrix.NextTrack() != null)
            {
                CurrentRace = new Race(GrandPrix.NextTrack(), new List<IParticipant>());
            }
        }
    }
}
