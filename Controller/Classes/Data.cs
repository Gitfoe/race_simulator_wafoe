using Model.Classes;
using Model.Interfaces;
using static Model.Classes.Section;
using System;


namespace Controller.Classes
{
    public static class Data // Static class, no instance can be made
    {
        // Properties
        public static Competition GrandPrix { get; set; }
        public static Race CurrentRace { get; set; }
        public static CompetitionInfo CompetitionInfo { get; set; }

        // Events
        public static event EventHandler<NextRaceEventArgs> NextRaceEvent;
        public static event EventHandler CompetitionFinished;

        // Methods
        public static void Initialize()
        { // Initialize a new competition
            GrandPrix = new Competition();
            AddParticipants(); // Call the AddParticipant method to add partiticpants to the GrandPrix
            AddTracks(); // Call the AddTracks method to add tracks to the GrandPrix
            CompetitionInfo = new CompetitionInfo();
        }
        private static void AddParticipants()
        { // Add some participants to the race
            GrandPrix.Participants.AddRange(new IParticipant[] { // Add the list of new Driver classes to the Participants list
                new Driver("Mario", new Kart(10), TeamColors.Mario),
                new Driver("Toad", new Kart(10), TeamColors.Toad),
                new Driver("Luigi", new Kart(10), TeamColors.Luigi),
                new Driver("Peach", new Kart(10), TeamColors.Peach),
                new Driver("Wafoe", new Kart(10), TeamColors.Wafoe),
                new Driver("Bowser", new Kart(10), TeamColors.Bowser),
                //new Driver("Koopa", new Kart(10), TeamColors.Koopa),
                //new Driver("DK Junior", new Kart(10), TeamColors.DKJunior)
            }); 
        }
        private static void AddTracks()
        { // Method that adds all the tracks in the list TrackList to the queue of GrandPrix
            Track[] TrackList = // Create list of tracks
            {
                new Track("Yoshi Circuit", 2, new SectionTypes[] {
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
                    SectionTypes.StartGrid,
                    SectionTypes.StartGrid
                } ),
                new Track("Figure 8 Circuit", 3, new SectionTypes[] {
                    SectionTypes.Finish,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
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
                    SectionTypes.RightCorner
                } ),
                new Track("Rainbow Road", 4, new SectionTypes[] {
                    SectionTypes.Finish,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner
                } ),
                new Track("Coconut Mall", 1, new SectionTypes[] {
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
                    SectionTypes.Straight,
                    SectionTypes.StartGrid,
                    SectionTypes.StartGrid,
                    SectionTypes.StartGrid
                } )
            };
            foreach (Track track in TrackList) // Enqueue all the tracks
            {
                GrandPrix.Tracks.Enqueue(track);
            }
        }
        public static void NextRace()
        {
            CurrentRace?.CleanUp(); // Cleans up the current race, the question mark checks if there is a current race
            Track currentTrack = GrandPrix.NextTrack();
            if (currentTrack != null)
            {
                CurrentRace = new Race(currentTrack, GrandPrix.Participants);
                CurrentRace.RaceFinished += OnRaceFinished;
                NextRaceEvent(CurrentRace, new NextRaceEventArgs() { Race = CurrentRace }); // Throw event that the next race is happening
                CurrentRace.Start(); // Start the timer for the race
            }
            else
            {
                CompetitionFinished(null, null);
            }
        }
        private static void OnRaceFinished(object sender, EventArgs e)
        { // Event catcher that calls NextRace once a race is finished
            NextRace();
        }
    }
}
