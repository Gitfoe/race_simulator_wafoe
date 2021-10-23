using Controller;
using Controller.Classes;
using Model.Classes;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Model
{
    public class CompetitionInfoDataContext : INotifyPropertyChanged
    {
        // Fields
        private Race _currentRace;

        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        // Properties
        public List<IParticipant> Participants { get; set; }
        public List<ParticipantTimes> ParticipantRaceTimes { get; set; }

        // Event methods
        private void OnEvent()
        {
            Participants = _currentRace.Participants.OrderByDescending(x => x.Points).ToList();
            ParticipantRaceTimes = Data.CompetitionInfo.ParticipantRaceTimes.ToList();
            PropertyChanged(this, new PropertyChangedEventArgs(""));
        }

        public void OnNextRaceEvent(object sender, NextRaceEventArgs args)
        {
            _currentRace = args.Race;
            OnEvent();
        }

        public void OnCompetitionFinished(object sender, EventArgs args)
        {
            OnEvent();
        }
    }
}