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

        // Events
        public static event EventHandler<NextRaceEventArgs> NextRaceEvent;

        // Methods
        public static void Initialize() // Initialize a new competition
        {
            GrandPrix = new Competition();
            AddParticipants(); // Call the AddParticipant method to add partiticpants to the Grand Prix
            AddTracks(); // Call the AddTracks method to add tracks to the Grand Prix
        }
        private static void AddParticipants() // Add 3 participants to the race
        {
            GrandPrix.Participants.AddRange(new IParticipant[] { // Add the list of new Driver classes to the Participants list
                new Driver("Mario", 0, new Kart(10), TeamColors.Mario),
                new Driver("Toad", 0, new Kart(10), TeamColors.Toad),
                new Driver("Luigi", 0, new Kart(10), TeamColors.Luigi),
                new Driver("Koopa", 0, new Kart(10), TeamColors.Koopa),
                new Driver("Peach", 0, new Kart(10), TeamColors.Peach),
                new Driver("DK Junior", 0, new Kart(10), TeamColors.DKJunior),
                new Driver("Yoshi", 0, new Kart(10), TeamColors.Wafoe),
                new Driver("Bowser", 0, new Kart(10), TeamColors.Bowser)
            }); 
        }
        private static void AddTracks()
        {
            // Method that adds all the tracks in the list TrackList to the queue of GrandPrix
            Track[] TrackList = // Create list of tracks
            {
                new Track("Figure 8 Circuit", new Section.SectionTypes[] {
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.StartGrid,
                    SectionTypes.StartGrid,
                    SectionTypes.RightCorner,
                    SectionTypes.Finish,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                } ),
                new Track("Rainbow Road", new Section.SectionTypes[] {
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.Finish
                } ),
                new Track("Yoshi Circuit", new Section.SectionTypes[] {
                    SectionTypes.StartGrid,
                    SectionTypes.StartGrid,
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
                } ),
                new Track("Coconut Mall", new Section.SectionTypes[] {
                    SectionTypes.StartGrid,
                    SectionTypes.StartGrid,
                    SectionTypes.StartGrid,
                    SectionTypes.Finish,
                    SectionTypes.LeftCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.Straight
                } )
            };
            foreach (Track track in TrackList) // Enqueue all the tracks
            {
                GrandPrix.Tracks.Enqueue(track);
            }
        }
        public static void NextRace()
        {
            CurrentRace?.CleanUp(); // Cleans up the current race, the question mark notes it checks if there is a current race
            Track currentTrack = GrandPrix.NextTrack();
            if (currentTrack != null)
            {
                CurrentRace = new Race(currentTrack, GrandPrix.Participants);
                CurrentRace.RaceFinished += OnRaceFinished;
                NextRaceEvent(CurrentRace, new NextRaceEventArgs() { Race = CurrentRace });
            }
            CurrentRace.Start(); // Start the timer for the race
        }
        private static void OnRaceFinished(object sender, EventArgs e)
        {
            NextRace();
        }
    }
}
