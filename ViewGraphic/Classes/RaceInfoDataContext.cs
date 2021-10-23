using Controller;
using Controller.Classes;
using Model.Classes;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ViewGraphic.Classes;

namespace Model
{
    public class RaceInfoDataContext : INotifyPropertyChanged
    {
        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        // Properties
        public Race CurrentRace { get; set; }
        public List<IParticipant> Participants { get; set; }
        public List<IParticipant> ParticipantsDescendingByPosition { get; set; }
        public int AmountOfLaps { get; set; }
        public int AmountOfSections { get; set; }
        public int AmountOfRacers { get; set; }
        public List<ParticipantTimes> ParticipantLapTimes { get; set; }
        public string RaceTimer { get; set; }

        // Event methods
        public void OnNextRaceEvent(object sender, NextRaceEventArgs args)
        {
            CurrentRace = args.Race;
            args.Race.DriversChanged += OnDriversChanged;

        }
        private void OnDriversChanged(object sender, DriversChangedEventArgs args)
        {
            Participants = CurrentRace.Participants.ToList();
            ParticipantsDescendingByPosition = Participants.OrderBy(s => s.GetType().GetProperty("PositionOnTrack").GetValue(s)).ToList();
            AmountOfLaps = CurrentRace.RaceInfo.AmountOfLaps;
            AmountOfSections = CurrentRace.RaceInfo.AmountOfSections;
            AmountOfRacers = CurrentRace.RaceInfo.AmountOfRacers;
            ParticipantLapTimes = CurrentRace.RaceInfo.ParticipantLapTimes.ToList();
            RaceTimer = FixRaceTimer(CurrentRace.RaceInfo.RaceTimer);
            PropertyChanged(this, new PropertyChangedEventArgs(""));
        }

        private string FixRaceTimer(TimeSpan raceTimer)
        { // Round to 3 milliseconds and remove useless values from the string, depening on the outcome of the string length
            string returnRaceTimer = raceTimer.RoundSeconds(3).ToString();
            return returnRaceTimer.Length == 16
                ? returnRaceTimer.Remove((returnRaceTimer.Length - 4)).Remove(0, 3)
                : returnRaceTimer.Remove(0, 3) + ".000";
        }
    }
}