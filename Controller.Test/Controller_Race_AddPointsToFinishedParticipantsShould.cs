using NUnit.Framework;
using Model.Classes;
using Model.Interfaces;
using Controller.Classes;
using System.Collections.Generic;
using static Model.Classes.Section;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_DeterminePositionsOnTrackShould
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
            });
        }

        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(3, false)]
        [TestCase(4, false)]
        [TestCase(5, false)]
        [TestCase(6, false)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(4, true)]
        [TestCase(5, true)]
        public void DeterminePositionsOnTrack_CheckForCorrectOutput(int roundsFinished, bool oneParticipantLapFurther)
        { // This test case always uses the first participant (in this case, Mario)
            #region Preparation
            Race race = new Race(new Track("Rainbow Road", 6, new Section.SectionTypes[] {
                    SectionTypes.Finish,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.LeftCorner,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.StartGrid,
                    SectionTypes.StartGrid}), _competition.Participants); // Setup faux race

            // First clear the _positions because by default it gets filled, but we want to manually control it
            foreach (KeyValuePair<Section, SectionData> item in race._positions)
            {
                item.Value.Left = null;
                item.Value.Right = null;
            }

            // Enqueue the participants so we can place them nicely
            Queue<IParticipant> tempQueue = new Queue<IParticipant>(_competition.Participants);

            // Fill the participants on straight sections
            foreach (KeyValuePair<Section, SectionData> item in race._positions)
            {
                if (item.Key.SectionType == Section.SectionTypes.Straight)
                {
                    item.Value.Left = tempQueue.Dequeue();
                    item.Value.Right = tempQueue.Dequeue();
                }
                if (tempQueue.Count == 0) // Break out of the loop if the queue is empty to save on resources
                {
                    break;
                }
            }
            #endregion

            string firstDriverPlacement = "6th";
            string secondDriverPlacement = "1st";
            // Add all the participant to the _roundsFinished dictionary
            foreach (IParticipant participant in race.Participants)
            {
                if (oneParticipantLapFurther) // If we want one participant to be a lap further to test the lap handling part of the function
                {
                    race._roundsFinished.Add(participant, roundsFinished + 1);
                    firstDriverPlacement = "1st";
                    secondDriverPlacement = "2nd";
                    oneParticipantLapFurther = false;
                }
                else // If not, just add normally
                {
                    race._roundsFinished.Add(participant, roundsFinished);
                }
                
            }

            // Execute the method we want to test
            race.DeterminePositionsOnTrack();

            // Now, we test if the first one is last and the last one is first (reversed, because we didn't use the method to fix the positions by reversing the order of placement)
            Assert.IsTrue(race.Participants[0].PositionOnTrack == firstDriverPlacement);
            Assert.IsTrue(race.Participants[5].PositionOnTrack == secondDriverPlacement);
        }
    }
}
