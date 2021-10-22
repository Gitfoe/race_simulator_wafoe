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
        public int AmountOfLaps { get; set; }
        public int AmountOfSections { get; set; }
        public int AmountOfRacers { get; set; }
        public List<ParticipantLapTime> ParticipantLapTimes { get; set; }
        public string RaceTimer { get; set; }

        // Event methods
        public void OnNextRaceEvent(object sender, NextRaceEventArgs args)
        {
            CurrentRace = args.Race;
            args.Race.DriversChanged += OnDriversChanged;

        }
        private void OnDriversChanged(object sender, DriversChangedEventArgs args)
        {
            Participants = CurrentRace.Participants.ToList(); // I don't know why, but to make it work in the ListView I have to use .ToList() even though it's already a list
            AmountOfLaps = CurrentRace.RaceInfo.AmountOfLaps;
            AmountOfSections = CurrentRace.RaceInfo.AmountOfSections;
            AmountOfRacers = CurrentRace.RaceInfo.AmountOfRacers;
            ParticipantLapTimes = CurrentRace.RaceInfo.ParticipantLapTimes.ToList();
            RaceTimer = CurrentRace.RaceInfo.RaceTimer.RoundSeconds(3).ToString(); RaceTimer = RaceTimer.Remove(RaceTimer.Length - 4); // Round to 3 milliseconds and remove useless values from the string
            PropertyChanged(this, new PropertyChangedEventArgs(""));
        }
    }
}