using System;

namespace Model.Options
{
    public enum GroupType
    {
        SingleSelect = 0,
        MultiSelect = 1
    }

    public enum PreviewType
    {
        Name = 0,
        NameWithDescription = 1,
        Image = 2
    }
    
    /// <summary>
    /// Option group defines how some options rely to each other as well as how they are grouped and displayed in UI
    /// </summary>
    [Serializable]
    public class OptionGroup
    {
        public string Name;
        public Option[] Options;
        public GroupType GroupType;
        public PreviewType PreviewType;
    }
}