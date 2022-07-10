using System;
using UnityEngine;

namespace Model.Options
{
    /// <summary>
    /// Option represents any selectable choice for configuration,
    /// from base model to any selectable individual option.
    /// The <see cref="OptionNode">Option Node</see> describes the relation between options.
    /// </summary>
    [Serializable]
    public class Option
    {
        public string Code;
        public string Name;
        [TextArea]
        public string Description;
        // Add other fields of required
    }
}