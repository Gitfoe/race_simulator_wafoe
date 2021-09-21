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
            _competition = new Competition(); // Setup the tests with a new Competition
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            var result = _competition.NextTrack(); // Call NextTrack on _competition
            Assert.IsNull(result); // Check if the output is null, because it should be
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            // Create a new track with some info
            Track _track = new Track("Test Track", new Section.SectionTypes[] {
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.Finish,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid });
            _competition.Tracks.Enqueue(_track); // Enqueue the track
            var result = _competition.NextTrack(); // Call NextTrack
            Assert.AreEqual(result, _track); // Check if the enqueued track is the same as the one we queued
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            _competition.Tracks.Enqueue(new Track("Another Test Track", new Section.SectionTypes[] {
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.Finish,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid })); // Add the track to the queue
            var result = _competition.NextTrack(); // Queue next track, now 0 tracks should be left
            result = _competition.NextTrack(); // Queue next track, but there is none, so this should return null
            Assert.IsNull(result); // Check if the output is null, because it should be
        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            // First enqueue 2 tracks
            Track _trackOne = new Track("Test Track One", new Section.SectionTypes[] {
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.Finish,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.LeftCorner,
                    SectionTypes.StartGrid });
            Track _trackTwo = new Track("Test Track Two", new Section.SectionTypes[] {
                    SectionTypes.RightCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.RightCorner,
                    SectionTypes.Finish,
                    SectionTypes.RightCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.RightCorner,
                    SectionTypes.StartGrid });
            _competition.Tracks.Enqueue(_trackOne);
            _competition.Tracks.Enqueue(_trackTwo);
            var result = _competition.NextTrack(); // Queue next track, now _trackTwo should be left
            // Queue is FIFO (first in, first out) so it removed _trackOne (now result) and _trackTwo_ should be left
            Assert.AreEqual(result, _trackOne);
        }
    }
}
