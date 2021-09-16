﻿using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    class SectionData
    {
        public IParticipant Left { get; set; }
        public int DistanceLeft { get; set; }
        public IParticipant Right { get; set; }
        public int DistanceRight { get; set; }
    }
}