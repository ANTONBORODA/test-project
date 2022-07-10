using System.Collections.Generic;
using Model.Options;

namespace Model
{
    public interface IConfiguration
    {
        string Name { get; set; }
        IReadOnlyCollection<Option> Options { get; }
    }
}