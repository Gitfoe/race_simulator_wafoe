using NUnit.Framework;
using Model.Classes;
using static Model.Classes.Section;
using Model.Interfaces;
using Controller.Classes;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_CountCertainSectionTypesShould
    {
        // Fields
        private Competition _competition;

        // Methods
        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
            _competition.Participants.AddRange(new IParticipant[] { });
        }

        [TestCase(SectionTypes.LeftCorner, 4)]
        [TestCase(SectionTypes.RightCorner, 0)]
        [TestCase(SectionTypes.StartGrid, 3)]
        [TestCase(SectionTypes.Finish, 1)]
        [TestCase(SectionTypes.Straight, 0)]
        public void CountCertainSectionTypes_CheckIfCorrectSectionCount(SectionTypes sectionTypeToCount, int correctSectionTypeCount)
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

            int countedSectionTypes = race.CountCertainSectionTypes(race.Track.Sections, sectionTypeToCount); // Call the method and count the types

            Assert.AreEqual(correctSectionTypeCount, countedSectionTypes); // Compare it to the actual correct result
        }
    }
}
