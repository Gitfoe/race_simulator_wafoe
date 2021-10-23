namespace Model.Classes
{
    public class Section // Sectie
    {
        // Properties
        public SectionTypes SectionType { get; set; }

        // Constructors
        public Section(SectionTypes sectionType)
        {
            SectionType = sectionType;
        }

        // Enumerations
        public enum SectionTypes { Straight, LeftCorner, RightCorner, StartGrid, Finish };
    }
    
}
