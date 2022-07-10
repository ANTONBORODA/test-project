using System;
using Model.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.OptionDrawers
{
    public class TextToggleDrawer : MonoBehaviour, IOptionDrawer
    {
        public event Action<Option, bool> OptionChanged;

        [SerializeField] private Toggle Toggle;
        [SerializeField] private TMP_Text Text;
        [SerializeField] private GameObject OptionalIndicator;

        public Option Option { get; private set; }
        
        public void Init(Option option)
        {
            Option = option;
            Text.text = Option.Name;
        }
        
        public void OnToggleChanged(bool active)
        {
            OptionChanged?.Invoke(Option, active);
        }

        private void OnDestroy()
        {
            OptionChanged = null;
        }
        
        public void SetOptionState(OptionDrawingState drawingState)
        {
            switch (drawingState)
            {
                case OptionDrawingState.Selected:
                    this.gameObject.SetActive(true);
                    Toggle.isOn = true;
                    break;
                case OptionDrawingState.Unselected:
                    this.gameObject.SetActive(true);
                    Toggle.isOn = false;
                    break;
                case OptionDrawingState.Hidden:
                default:
                    Toggle.isOn = false;
                    this.gameObject.SetActive(false);
                    break;
            }
        }

        public bool IsOptional
        {
            set => OptionalIndicator.SetActive(value);
        }
    }
}