using System.Collections.Generic;
using System.Linq;

namespace Model.Options.Extensions
{
    public static class OptionExtensions
    {
        public static Option GetOption(this IEnumerable<Option> options, string code)
        {
            return options.FirstOrDefault(t => t.Code == code);
        }

        public static OptionState GetOptionStateInContext(this Option option, IConfiguration context,
            ConfigurationData data)
        {
            if (option.IsRootOption(data)) return OptionState.Available;

            var relations = data.OptionRelations.Where(op => op.To == option);
            foreach (var optionRelation in relations)
            {
                if (optionRelation.RelationType == RelationType.Stock ||
                    optionRelation.RelationType == RelationType.Optional)
                {
                    if (context.Options.Contains(optionRelation.This))
                        return OptionState.Available;
                }
                
            }

            return OptionState.Unavailable;
        }

        public static RelationType GetRelationType(this Option option, IConfiguration context, ConfigurationData data)
        {
            if (option.IsRootOption(data)) return RelationType.Stock;

            var relations = data.OptionRelations.Where(op => op.To == option);
            foreach (var optionRelation in relations)
            {
                if (context.Options.Contains(optionRelation.This))
                    return optionRelation.RelationType;
            }

            return RelationType.Locked;
        }

        public static IEnumerable<Option> GetRelatedGroupsDefaultOptionsInContext(this Option option, IConfiguration context, ConfigurationData data)
        {
            var result = new List<Option>();
            var groups = new List<OptionGroup>();
            GetRelationGroupsForOption(option, data, groups);
            foreach (var group in groups)
            {
                if (group.GroupType != GroupType.SingleSelect) continue;
                foreach (var groupOption in group.Options)
                {
                    if (groupOption.GetOptionStateInContext(context, data) == OptionState.Available)
                    {
                        result.Add(groupOption);
                        break;
                    }
                }
            }
            return result;
        }

        public static IEnumerable<Option> GetAllRelatedOptions(this Option option, ConfigurationData data)
        {
            var options = new List<Option>();
            GetAllRelatedOptions(option, data, options);
            return options;
        }
        
        public static Option GetDefaultRootOption(this ConfigurationData data)
        {
            foreach (var option in data.Options)
            {
                if (!option.IsRootOption(data)) continue;
                var group = data.Groups.FirstOrDefault(t => t.Options.Contains(option));
                if (group == null) continue;
                return group.Options.First();
            }

            return null;
        }

        public static IEnumerable<OptionGroup> GetOptionGroups(this Option option, ConfigurationData data)
        {
            return data.Groups.Where(t => t.Options.Contains(option));
        }
        
        private static void GetAllRelatedOptions(this Option option, ConfigurationData data, List<Option> options)
        {
            foreach (var relation in data.OptionRelations.Where(t => t.This == option))
            {
                options.Add(relation.To);
                GetAllRelatedOptions(relation.To, data, options);
            }
        }
        
        private static void GetRelationGroupsForOption(Option option, ConfigurationData data, List<OptionGroup> resolvedGroups)
        {
            var rel = data.OptionRelations.Where(op => op.This == option);
            foreach (var optionRelation in rel)
            {
                var group = data.Groups.FirstOrDefault(t => t.Options.Contains(optionRelation.To));
                if (!resolvedGroups.Contains(group)) resolvedGroups.Add(group);
                GetRelationGroupsForOption(optionRelation.To, data, resolvedGroups);
            }
        }
        
        private static bool IsRootOption(this Option option, ConfigurationData data)
        {
            return data.OptionRelations.All(t => t.To != option);
        }

        public enum OptionState
        {
            Unavailable,
            Available,
        }

    }
}