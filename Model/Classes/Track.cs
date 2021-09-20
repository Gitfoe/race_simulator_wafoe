using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Classes
{
    public class Track // Racebaan
    {
        // Properties
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
        
        // Constructors
        public Track(string name, Section.SectionTypes[] sections)
        {
            Name = name;
            Sections = new LinkedList<Section>();
            foreach (Section.SectionTypes section in sections) // Add all the sections to the LinkedList
            {
                Sections.AddLast(new Section(section));
            }
        }
    }
}