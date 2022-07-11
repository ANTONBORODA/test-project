using System.Collections.Generic;

namespace Views
{
    public interface IOptionVisualizer
    {
        IEnumerable<string> SupportedOptionCodes { get; }
        void SetOptionActive(string code);
        void SetOptionInactive(string code);
    }
}