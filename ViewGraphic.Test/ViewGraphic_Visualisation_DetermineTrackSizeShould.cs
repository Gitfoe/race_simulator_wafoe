using NUnit.Framework;
using System;
using System.Collections.Generic;
using Model.Classes;
using ViewGraphic.Classes;

namespace ControllerTest
{
    [TestFixture]
    public class ViewGraphic_Visualisation_DetermineTrackSizeShould
    {
        // Fields
        private Competition _competition;

        // Methods
        [SetUp]
        public void SetUp()
        {
            _competition = new Competition(); // Setup the tests with a new Competition
        }

        [TestCase(1, 192, 128)]
        [TestCase(2, 256, 256)]
        public void DetermineTrackSizeShouldReturn(int selectList, int xShould, int yShould)
        {
            List<int[]> testList;
            if (selectList == 1)
            {
                testList = new List<int[]>
                {
                    new int[] { 64, 0 },
                    new int[] { 0, -64 },
                    new int[] { 64, 0 },
                    new int[] { 64, 0 },
                    new int[] { 0, -64 },
                    new int[] { 0, 64 }
                };
            }
            else if (selectList == 2)
            {
                testList = new List<int[]>
                {
                    new int[] { 64, 0 },
                    new int[] { 64, 0 },
                    new int[] { 64, 0 },
                    new int[] { 64, 0 },
                    new int[] { 0, -64 },
                    new int[] { 0, 64 },
                    new int[] { 0, -64 },
                    new int[] { 0, -64 },
                    new int[] { 0, -64 }
                };
            }
            else
            {
                throw new Exception();
            }

            int[] testArray = Visualisation.DetermineTrackSize(testList); // It's private now, put it on public for the test
            Assert.AreEqual(xShould + 64, testArray[0]); // Should be 0
            Assert.AreEqual(yShould + 64, testArray[1]); // Should be 64
        }
    }
}
