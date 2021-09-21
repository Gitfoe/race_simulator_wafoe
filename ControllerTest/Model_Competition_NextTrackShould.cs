using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Model.Classes;
using static Model.Classes.Section;
using Model.Interfaces;
using Controller;
using Controller.Classes;
using View;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        // Fields
        private Competition _competition;

        // Methods
        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            var result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Track _track = new Track("Test Track", new Section.SectionTypes[] {
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.Finish,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid });
            Competition _competition = new Competition();
            _competition.Tracks.Enqueue(_track);
            var result = _competition.NextTrack();
            Assert.AreEqual(result, _track);
        }
        
    }
}
