using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    public class Section
    {
        // Properties
        public SectionTypes SectionType { get; set; }
    }
    // Enumerations
    public enum SectionTypes { Straight, LeftCorner, RightCorner, StartGrid, Finish };
}
