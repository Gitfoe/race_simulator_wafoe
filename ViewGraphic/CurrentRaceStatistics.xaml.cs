using Controller;
using Controller.Classes;
using Model;
using Model.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DispatcherPriority = System.Windows.Threading.DispatcherPriority;

namespace ViewGraphic
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class CurrentRaceStatistics : Window
    {
        public CurrentRaceStatistics()
        {
            InitializeComponent();
            //Data.NextRaceEvent += OnNextRaceEvent;
        }

        //private void OnNextRaceEvent(object sender, NextRaceEventArgs args)
        //{ // Link events and draw track for the first time
        //    //args.Race.DriversChanged += OnDriversChanged;
        //    args.Race.StartOfRace += OnStartOfRace;
        //}

        //public void OnStartOfRace(object sender, NextRaceEventArgs args)
        //{
        //    this.Dispatcher.BeginInvoke(
        //        DispatcherPriority.Normal,
        //        new Action(() =>
        //        {
        //            args.Race.RaceInfo += ((RaceInfoDataContext)this.DataContext).OnRaceInfo;
        //        }));
        //}
    }
}