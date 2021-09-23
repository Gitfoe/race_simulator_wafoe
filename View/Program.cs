using System;
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
            Data.NextRace();
            Data.NextRace();
            Visualisation.DrawTrack(Data.GrandPrix.Tracks.Peek());

            for (; ; ) // Unlimited loop to not immediately close it
            {
                Thread.Sleep(100); // Sleeps 100 milliseconds
            }
        }
    }
}
