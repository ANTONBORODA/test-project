using System;

namespace Model.Options
{
    [Serializable]
    public class ConfigurationData
    {
        public readonly Option[] Options;
        public readonly OptionGroup[] Groups;
        public readonly OptionRelation[] OptionRelations;

        public ConfigurationData(Option[] options, OptionGroup[] groups, OptionRelation[] optionRelations)
        {
            Options = options;
            Groups = groups;
            OptionRelations = optionRelations;
        }
    }
}