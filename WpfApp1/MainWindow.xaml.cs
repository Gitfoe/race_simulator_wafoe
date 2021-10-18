﻿using System;
using Model;
using Model.Classes;
using Model.Interfaces;
using Controller;
using Controller.Classes;
using ViewGraphic.Classes;
using System;
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

            GraphicsCache.Initialize();
            Data.Initialize();
            Data.NextRaceEvent += OnNextRaceEvent; // tell data about visualization event.
            Data.NextRace();
        }

        private void OnNextRaceEvent(object sender, NextRaceEventArgs args)
        { // Link events and draw track for the first time
            args.Race.DriversChanged += Visualisation.OnDriversChanged;
            GraphicsCache.ClearCache();
            
            // Dispatcher priority
            this.FirstLook.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.FirstLook.Source = null;
                    this.FirstLook.Source = Visualisation.DrawTrack(args.Race.Track); ;
                }));
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        { // ?

        }
    }
}
