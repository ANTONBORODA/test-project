using System.Collections.Generic;
using Model.Options;
using UI.OptionDrawers;

namespace UI.GroupDrawers
{
    public interface IGroupDrawer
    {
        IEnumerable<IOptionDrawer> CreateOptionDrawers(OptionGroup group);
    }
}