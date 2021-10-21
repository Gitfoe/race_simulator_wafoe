﻿using NUnit.Framework;
using Model.Classes;
using Model.Interfaces;
using Controller.Classes;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_AddPointsToFinishedParticipantShould
    {
        // Fields
        private Competition _competition;

        // Methods
        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
            _competition.Participants.AddRange(new IParticipant[] { // Add the list of new Driver classes to the Participants list
                new Driver("Mario", 0, new Kart(10), TeamColors.Mario),
                new Driver("Toad", 0, new Kart(10), TeamColors.Toad),
                new Driver("Luigi", 0, new Kart(10), TeamColors.Luigi),
                new Driver("Peach", 0, new Kart(10), TeamColors.Peach),
                new Driver("Wafoe", 0, new Kart(10), TeamColors.Wafoe),
                new Driver("Bowser", 0, new Kart(10), TeamColors.Bowser),
                new Driver("Koopa", 0, new Kart(10), TeamColors.Koopa),
                new Driver("DK Junior", 0, new Kart(10), TeamColors.DKJunior),
            });
        }

        [TestCase(15, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 })]
        [TestCase(12, new int[] { 0, 15, 0, 0, 0, 0, 0, 0 })]
        [TestCase(10, new int[] { 0, 12, 0, 15, 0, 0, 0, 0 })]
        [TestCase(8, new int[] { 0, 0, 15, 0, 0, 12, 0, 10 })]
        [TestCase(7, new int[] { 0, 12, 10, 15, 8, 0, 0, 0 })]
        [TestCase(6, new int[] { 0, 0, 0, 7, 8, 10, 12, 15 })]
        [TestCase(5, new int[] { 0, 0, 10, 12, 8, 7, 6, 15 })]
        [TestCase(4, new int[] { 0, 15, 10, 6, 5, 7, 8, 12 })]
        public void AddPointsToFinishedParticipant_CheckForCorrectOutput(int toAchievePoints, int[] achievedPointsByOtherRacers)
        { // This test case always uses the first participant (in this case, Mario)
            Race race = new Race(new Track("Rainbow Road", new Section.SectionTypes[] { }), _competition.Participants); // Setup faux race

            for (int i = 0; i < _competition.Participants.Count; i++)
            { // First add the points to all participants in the list order (check Setup) but skip the participant
                if (_competition.Participants[i] != _competition.Participants[0])
                {
                    _competition.Participants[i].Points = achievedPointsByOtherRacers[i];
                }
            }
            // Calculate the needed points for the participant with the method
            race.AddPointsToFinishedParticipant(_competition.Participants[0]);

            Assert.AreEqual(true, _competition.Participants[0].Points == toAchievePoints);
        }
    }
}
