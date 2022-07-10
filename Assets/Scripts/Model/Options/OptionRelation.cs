using System;

namespace Model.Options
{
    /// <summary>
    /// Option relation defines how options are linked together, enabling building chains of options that
    /// are allowed or disallowed depending on previous selections
    /// </summary>
    [Serializable]
    public class OptionRelation
    {
        public RelationType RelationType;
        public Option This;
        public Option To;
    }
    
    public enum RelationType
    {
        Stock = 0,
        Optional = 1,
        Locked,
    }
}