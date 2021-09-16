using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    class Section
    {
        // Properties
        public SectionTypes SectionType { get; set; }
    }
    // Enumerations
    enum SectionTypes { Straight, LeftCorner, RightCorner, StartGrid, Finish };
}
