using NUnit.Framework;
using System;
using Model.Classes;
using static Model.Classes.Section;
using Model.Interfaces;
using Controller.Classes;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_PlaceParticipantsOnStartGridsShould
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

        [Test]
        public void PlaceParticipantsOnStartGrids_CheckIfParticipantsArePlaced()
        {
            Race race = new Race(new Track("Rainbow Road", 4, new Section.SectionTypes[] { // Setup a new temporary race
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.Finish, }), _competition.Participants); // Setup faux race with 3 startgrids (6 positions)

            race.PlaceParticipantsOnStartGrids(race.Track, _competition.Participants);

            int countOfParticipants = _competition.Participants.Count;
            int countOfFoundParticipants = 0;
            foreach (var keyValuePair in race._positions)
            { // Counts the found participants in the _positions dictionary
                if (keyValuePair.Value.Left != null && _competition.Participants.Contains(keyValuePair.Value.Left))
                {
                    countOfFoundParticipants++;
                }
                if (keyValuePair.Value.Right != null && _competition.Participants.Contains(keyValuePair.Value.Right))
                {
                    countOfFoundParticipants++;
                }
            }

            Assert.AreEqual(countOfParticipants, countOfFoundParticipants);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void PlaceParticipantsOnStartGrids_CheckIfOnlyStartGridsAreOccupied(int selectRace)
        {
            Race race;
            if (selectRace == 1)
            {
                race = new Race(new Track("Rainbow Road", 4, new Section.SectionTypes[] { // Setup a new temporary race
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.Finish, }), _competition.Participants); // Setup faux race with 3 startgrids (6 positions)
                race.PlaceParticipantsOnStartGrids(race.Track, _competition.Participants);
            }
            else if (selectRace == 2)
            {
                race = new Race(new Track("Rainbow Road", 4, new Section.SectionTypes[] { // Setup a new temporary race
                    SectionTypes.LeftCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.Finish, }), _competition.Participants); // Setup faux race with 0 startgrids (0 positions)
                race.PlaceParticipantsOnStartGrids(race.Track, _competition.Participants);
            }
            else
            {
                throw new Exception();
            }

            bool success = true;
            foreach (var keyValuePair in race._positions)
            { // Loops through the _positions dictionary and sets "success" to false if it found any participants on sections other than StartGrids
                if (keyValuePair.Key.SectionType != SectionTypes.StartGrid)
                {
                    if (keyValuePair.Value.Left != null && keyValuePair.Value.Right != null)
                    {
                        success = false;
                        break;
                    }
                }
            }

            Assert.IsTrue(success);
        }
    }
}
