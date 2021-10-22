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
        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        // Properties
        public List<IParticipant> Participants { get; set; }

        // Event methods
        public void OnNextRaceEvent(object sender, NextRaceEventArgs args)
        {
            Participants = args.Race.Participants.OrderByDescending(x => x.Points).ToList();
            PropertyChanged(this, new PropertyChangedEventArgs(""));
        }
    }
}