using NUnit.Framework;
using System.Collections.Generic;
using Model.Classes;
using static Model.Classes.Section;
using Model.Interfaces;
using Controller.Classes;
using System.Linq;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_CheckIfEveryoneFinishedRaceShould
    {
        // Fields
        private Competition _competition;

        // Methods
        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
            _competition.Participants.AddRange(new IParticipant[] { // Add the list of new Driver classes to the Participants list
                new Driver("Mario", new Kart(10), TeamColors.Mario),
                new Driver("Toad", new Kart(10), TeamColors.Toad),
                new Driver("Luigi", new Kart(10), TeamColors.Luigi),
                new Driver("Peach", new Kart(10), TeamColors.Peach),
                new Driver("Wafoe", new Kart(10), TeamColors.Wafoe),
                new Driver("Bowser", new Kart(10), TeamColors.Bowser)
            });
        }

        [TestCase(false, 1, 1)]
        [TestCase(false, 2, 2)]
        [TestCase(true, 3, 3)]
        [TestCase(false, 1, 3)]
        [TestCase(false, 2, 3)]
        [TestCase(false, 3, 2)]
        [TestCase(false, 4, 4)]
        [TestCase(false, 0, 0)]
        public void CheckIfEveryoneFinishedRace_CheckForTrueAndFalse(bool boolShould, int lapsOfParticipants, int lapsOfFirstParticipant)
        {
            Race race = new Race(new Track("Rainbow Road", 2, new Section.SectionTypes[] { // Setup a new temporary race
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.Finish, }), _competition.Participants); // Setup faux race

            race._roundsFinished = new Dictionary<IParticipant, int>();

            foreach (IParticipant participant in _competition.Participants)
            { // Add all participants to the _roundsFinished dictionary and set it to the lapsOfParticipants value
                race._roundsFinished.Add(participant, lapsOfParticipants);

            }

            race._roundsFinished[race._roundsFinished.First().Key] = lapsOfFirstParticipant; // Set the laps of the first participant to lapsOfFirstParticipant

            bool output = race.CheckIfEveryoneFinishedRace(); // Execute the method

            Assert.AreEqual(boolShould, output);
        }
    }
}
