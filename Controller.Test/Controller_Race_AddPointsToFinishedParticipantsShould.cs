using NUnit.Framework;
using Model.Classes;
using Model.Interfaces;
using Controller.Classes;
using System.Collections.Generic;
using System.Linq;

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
                new Driver("Mario", new Kart(10), TeamColors.Mario),
                new Driver("Toad", new Kart(10), TeamColors.Toad),
                new Driver("Luigi", new Kart(10), TeamColors.Luigi),
                new Driver("Peach", new Kart(10), TeamColors.Peach),
                new Driver("Wafoe", new Kart(10), TeamColors.Wafoe),
                new Driver("Bowser", new Kart(10), TeamColors.Bowser),
                new Driver("Koopa", new Kart(10), TeamColors.Koopa),
                new Driver("DK Junior", new Kart(10), TeamColors.DKJunior),
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
            Race race = new Race(new Track("Rainbow Road", 4, new Section.SectionTypes[] { }), _competition.Participants); // Setup faux race
            
            List<IParticipant> copyParticipants = new List<IParticipant>();
            foreach (var item in _competition.Participants) // Make copy without reference type of the Participants list
            {
                copyParticipants.Add(new Driver(item.Name, item.Equipment, item.TeamColor));
            }

            for (int i = 0; i < copyParticipants.Count; i++)
            { // First add the points to all cloned participants in the list order (check Setup)
                copyParticipants[i].Points = copyParticipants[i] != copyParticipants[0] ? achievedPointsByOtherRacers[i] : toAchievePoints;
            }

            for (int i = 0; i < copyParticipants.Count; i++)
            { // Then add the points to all participants in the list order (check Setup) but skip the participant
                if (i >= 1 && copyParticipants[i].Points != 0)
                {
                    race._raceParticipantPoints.Add(_competition.Participants[i], copyParticipants[i].Points);
                }
            }

            // Calculate the needed points for the participant with the method
            race.DeterminePointsForFinishedParticipant(_competition.Participants[0]);
            race.AddPointsToAllParticipants();

            Assert.AreEqual(true, _competition.Participants[0].Points == copyParticipants[0].Points);
        }

        [Test]
        public void AddPointsToFinishedParticipant_CheckIfTopThreeCorrect()
        { // This test case always uses the first participant (in this case, Mario)
            Race race = new Race(new Track("Rainbow Road", 4, new Section.SectionTypes[] { }), _competition.Participants); // Setup faux race
            int[] achievedPointsByOtherRacers = new int[] { 0, 10, 15, 6, 5, 7, 8, 12 }; // Declare some random point winners
            List<IParticipant> correctList = new List<IParticipant>() { _competition.Participants[1], _competition.Participants[2], _competition.Participants[7] }; // Toad, Luigi and DK Junior
            List<IParticipant> executedListFromMethod = new List<IParticipant>();

            for (int i = 0; i < _competition.Participants.Count; i++)
            { // First add the points to all participants in the list order (check Setup)
                _competition.Participants[i].Points = achievedPointsByOtherRacers[i];
            }

            foreach (IParticipant participant in _competition.Participants)
            {
                if (participant.Points >= 10 && participant.Points <= 15)
                {
                    executedListFromMethod.Add(participant);
                }
            }

            Assert.AreEqual(correctList, executedListFromMethod);
        }
    }
}
