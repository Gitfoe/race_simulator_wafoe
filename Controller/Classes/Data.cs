using Model;
using Model.Classes;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller.Classes
{
    public static class Data
    {
        // Properties
        public static Competition GrandPrix { get; set; }
        // Methods
        public static void Initialize() // Initialize a new competition
        {
            Competition GrandPrix = new Competition();
            AddParticipants(); // Call the AddParticipant method to add partiticpants to the Grand Prix
            AddTracks(); // Call the AddTracks method to add tracks to the Grand Prix
        }
        public static void AddParticipants() // Add 3 participants to the race
        {
            IParticipant[] DriverList = // Create list of drivers
            {
                new Driver("Mario"),
                new Driver("Luigi"),
                new Driver("Yoshi")
            };
            GrandPrix.Participants.AddRange(DriverList); // Add the list of new Driver classes to the Participants IParticipant list
        }
        public static void AddTracks()
        {
            SectionTypes[] rainbowRoad = { SectionTypes.Finish, SectionTypes.Straight, SectionTypes.LeftCorner }; // Create sections for the track
            SectionTypes[] marioCircuit = { SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.LeftCorner };// Create sections for the track
            Track[] TrackList = // Create list of tracks
            {
                new Track("Rainbow Road", rainbowRoad ),
                new Track("Mario Circuit", marioCircuit )
            };
            foreach (Track track in TrackList) // Enqueue all the tracks
            {
                GrandPrix.Tracks.Enqueue(track);
            }
        }
}
}
