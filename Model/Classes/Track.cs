using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    public class Track
    {
        // Properties
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
        
        // Constructors
        public Track(string name, SectionTypes[] sections)
        {

        }
    }
}
