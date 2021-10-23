using Model.Classes;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class MainDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string TrackName { get; set; }
        public Dictionary<IParticipant, TimeSpan[]> LapTime { get; set; }

        public void OnDriversChanged(object sender, DriversChangedEventArgs args)
        {
            TrackName = args.Track.Name;
            PropertyChanged(this, new PropertyChangedEventArgs(""));
        }
    }
}