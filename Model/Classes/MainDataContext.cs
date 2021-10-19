using Model.Classes;
using System.ComponentModel;

namespace Model
{
    public class MainDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string TrackName { get; set; }

        public void OnDriversChanged(object sender, DriversChangedEventArgs args)
        {
            //TrackName = args.Track.Name;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}