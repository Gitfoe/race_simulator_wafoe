﻿using System;
using Model;
using Model.Classes;
using Model.Interfaces;
using Controller;
using Controller.Classes;
using View.Classes;
using System.Threading;
using System.Collections.Generic;

namespace View
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            //Visualisation_Extra.MakeTrack();
            Data.NextRace();
            Data.NextRaceEvent += Visualisation.OnNextRaceEvent;

            for (; ; ) // Unlimited loop to not immediately close the races
            {
                Thread.Sleep(100); // Sleeps 100 milliseconds
            }
        }
    }
}
