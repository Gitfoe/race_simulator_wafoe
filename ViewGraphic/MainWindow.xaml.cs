using System;
using Model;
using Model.Classes;
using Controller;
using Controller.Classes;
using ViewGraphic.Classes;
using System.Windows;
using System.Windows.Media;
using ViewGraphic;
using DispatcherPriority = System.Windows.Threading.DispatcherPriority;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Menu items
        private CurrentRaceStatistics _currentRaceStatistics = new CurrentRaceStatistics();
        private CompetitionStatistics _competitionStatistics = new CompetitionStatistics();

        public MainWindow()
        {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(TrackScreen, BitmapScalingMode.NearestNeighbor); // Set the scaling to the lowest quality for SNES vibes

            GraphicsCache.Initialize();
            Data.Initialize();
            Data.NextRaceEvent += OnNextRaceEvent;
            Data.CompetitionFinished += OnCompetitionFinished;
            Data.NextRace();
        }

        // Event handlers
        private void OnNextRaceEvent(object sender, NextRaceEventArgs args)
        { // Link events and clear cache
            GraphicsCache.ClearCache();
            args.Race.DriversChanged += OnDriversChanged;

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

        public void OnCompetitionFinished(object sender, NextRaceEventArgs args)
        {
            GraphicsCache.ClearCache();
            this.TrackScreen.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.TrackScreen.Source = null;
                    this.TrackScreen.Source = Visualisation.DrawCelebration(args.Race.Participants); ;
                }));

        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_CurrentRaceStatistics_Click(object sender, RoutedEventArgs e)
        {
            Data.NextRaceEvent += ((RaceInfoDataContext)_currentRaceStatistics.DataContext).OnNextRaceEvent;
            ((RaceInfoDataContext)_currentRaceStatistics.DataContext).OnNextRaceEvent(null, new NextRaceEventArgs() { Race = Data.CurrentRace }); // Fix to update every time

            _currentRaceStatistics.Show();
        }

        private void MenuItem_ParticipantStatistics_Click(object sender, RoutedEventArgs e)
        {
            Data.NextRaceEvent += ((CompetitionInfoDataContext)_competitionStatistics.DataContext).OnNextRaceEvent;
            ((CompetitionInfoDataContext)_competitionStatistics.DataContext).OnNextRaceEvent(null, new NextRaceEventArgs() { Race = Data.CurrentRace }); // Fix to update every time
            Data.CompetitionFinished += ((CompetitionInfoDataContext)_competitionStatistics.DataContext).OnCompetitionFinished;

            _competitionStatistics.Show();
        }
    }
}