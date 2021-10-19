using System;
using Model;
using Model.Classes;
using Model.Interfaces;
using Controller;
using Controller.Classes;
using ViewGraphic.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ViewGraphic;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(TrackScreen, BitmapScalingMode.NearestNeighbor); // Set the scaling to the lowest quality for 8-bit vibes

            GraphicsCache.Initialize();
            Data.Initialize();
            Data.NextRaceEvent += OnNextRaceEvent;
            Data.NextRace();
        }

        // Event handlers
        private void OnNextRaceEvent(object sender, NextRaceEventArgs args)
        { // Link events and draw track for the first time
            GraphicsCache.ClearCache();
            args.Race.DriversChanged += OnDriversChanged;
            
            // Dispatcher priority
            this.TrackScreen.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.TrackScreen.Source = null;
                    this.TrackScreen.Source = Visualisation.DrawTrack(args.Race.Track); ;
                }));

            this.Dispatcher.Invoke(() =>
            { // Dispatcher is needed for execution of OnDriversChanged to prevent thread exceptions
                args.Race.DriversChanged += ((MainDataContext)this.DataContext).OnDriversChanged;
            });
        }

        public void OnDriversChanged(object sender, DriversChangedEventArgs args)
        {
            this.TrackScreen.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.TrackScreen.Source = null;
                    this.TrackScreen.Source = Visualisation.DrawTrack(args.Track); ;
                }));
        }

        // Menu items
        private CurrentRaceStatistics currentRaceStatistics = new CurrentRaceStatistics();
        private ParticipantStatistics participantStatistics = new ParticipantStatistics();

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_CurrentRaceStatistics_Click(object sender, RoutedEventArgs e)
        {
            currentRaceStatistics.Show();
        }

        private void MenuItem_ParticipantStatistics_Click(object sender, RoutedEventArgs e)
        {
            participantStatistics.Show();
        }
    }
}