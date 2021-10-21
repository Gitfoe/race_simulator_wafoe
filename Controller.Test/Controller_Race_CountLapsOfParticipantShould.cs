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
    public class Controller_Race_CountLapsOfParticipantShould
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

        [TestCase(false, -1)]
        [TestCase(false, 0)]
        [TestCase(false, 1)]
        [TestCase(true, 2)] // Should only be true if it's the same as _amountOfLaps
        [TestCase(false, 3)]
        [TestCase(false, 4)]
        public void CountLapsOfParticipant_CheckIfParticipantFinishedRace(bool boolShould, int lapsOfParticipant)
        { // Keep the value of const "_amountOfLaps" in mind -- the default is 2
            Race race = new Race(new Track("Rainbow Road", new Section.SectionTypes[] { // Setup a new temporary race
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.Finish, }), _competition.Participants); // Setup faux race

            race._roundsFinished = new Dictionary<IParticipant, int>();
            race._roundsFinished.Add(_competition.Participants.First(), lapsOfParticipant); // Add the first participant with the laps from the test

            bool output = race.CountLapsOfParticipant(_competition.Participants.First()); // Execute the method

            Assert.AreEqual(boolShould, output);
        }

        [Test]
        public void CountLapsOfParticipant_CheckIfAddsToDictionary()
        {
            Race race = new Race(new Track("Rainbow Road", new Section.SectionTypes[] { // Setup a new temporary race
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.Finish, }), _competition.Participants); // Setup faux race

            race.CountLapsOfParticipant(_competition.Participants.First());

            Assert.AreEqual(race._roundsFinished.First().Key, _competition.Participants.First());
        }
    }
}
