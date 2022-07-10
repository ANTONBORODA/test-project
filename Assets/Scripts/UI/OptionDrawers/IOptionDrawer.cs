using System;
using Model.Options;

namespace UI.OptionDrawers
{
    public interface IOptionDrawer
    {
        Option Option { get; }
        void SetOptionState(OptionDrawingState drawingState);
        bool IsOptional { set; }
        event Action<Option, bool> OptionChanged;
    }

    public enum OptionDrawingState
    {
        Unselected,
        Selected,
        Locked,
        Hidden
    }
}