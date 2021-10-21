using NUnit.Framework;
using System.Collections.Generic;
using Model.Classes;
using ViewGraphic.Classes;

namespace ControllerTest
{
    [TestFixture]
    public class ViewGraphic_Visualisation_FixCursorPositionShould
    {
        // Fields
        private Competition _competition;

        // Methods
        [SetUp]
        public void SetUp()
        {
            _competition = new Competition(); // Setup the tests with a new Competition
        }

        [Test]
        public void FixCursorPositionShould()
        {
            List<int[]> testList = new List<int[]>();
            testList.Add(new int[] { 64, 0 });
            testList.Add(new int[] { 0, -64 });
            testList.Add(new int[] { 64, 0 });
            testList.Add(new int[] { 64, 0 });
            testList.Add(new int[] { 0, -64 });
            testList.Add(new int[] { 0, 64 });
            int[] testArray = Visualisation.FixCursorPosition(testList); // It's private now, put it on public for the test
            Assert.AreEqual(0, testArray[0]); // Should be 0
            Assert.AreEqual(128, testArray[1]); // Should be 64
        }
    }
}
