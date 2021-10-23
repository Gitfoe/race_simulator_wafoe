using NUnit.Framework;
using Model.Classes;
using static Model.Classes.Section;
using Model.Interfaces;
using Controller.Classes;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_RandomizeBrokenKartOfParticipantsShould
    {
        // Fields
        private Competition _competition;

        // Methods
        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
            _competition.Participants.Add(new Driver("Mario", new Kart(10), TeamColors.Mario));
        }

        [Test]
        public void RandomizeKartBreakValues_RandomValuesShould()
        { // The average of the true and false bools should fall within the range of the wanted odds (0,9% chance)
          // This test will still fail sometime!
            Race race = new Race(new Track("Rainbow Road", 4, new Section.SectionTypes[] {
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.Finish, }), _competition.Participants); // Setup faux race

            double trueCount = 0;
            double falseCount = 0;

            _competition.Participants[0].Equipment.Performance = 10;
            _competition.Participants[0].Equipment.Speed = 10;

            for (int i = 0; i < 10000; i++)
            {
                race.RandomizeKartBreakValues(_competition.Participants[0]);
                if (_competition.Participants[0].Equipment.IsBroken == true)
                {
                    trueCount += 1;
                }
                else
                {
                    falseCount += 1;
                }
            }

            bool success;
            if ((trueCount / falseCount) * 100 > 0.7 && (trueCount / falseCount) * 100 < 1.2)
            { // Checks if the value is somewhere between 0.7 and 1.2% (because it's random and not always precise)
                success = true;
            }
            else
            {
                success = false;
            }

            Assert.IsTrue(success);
        }

        [Test]
        public void RandomizeKartFixValues_RandomValuesShould()
        { // The average of the true and false bools should fall within the range of the wanted odds (10% chance)
          // This test will still fail sometime!
            Race race = new Race(new Track("Rainbow Road", 4, new Section.SectionTypes[] {
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.Finish, }), _competition.Participants); // Setup faux race

            double trueCount = 0;
            double falseCount = 0;

            _competition.Participants[0].Equipment.Performance = 10;
            _competition.Participants[0].Equipment.Speed = 10;

            for (int i = 0; i < 10000; i++)
            {
                race.RandomizeKartFixValues(_competition.Participants[0]);
                if (_competition.Participants[0].Equipment.IsBroken == true)
                {
                    trueCount += 1;
                }
                else
                {
                    falseCount += 1;
                }
            }

            bool success;
            if ((trueCount / falseCount) * 100 > 7 && (trueCount / falseCount) * 100 < 12)
            { // Checks if the value is somewhere between 7 and 12% (because it's random and not always precise)
                success = false;
            }
            else
            {
                success = true;
            }

            Assert.IsTrue(success);
        }
    }
}
