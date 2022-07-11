using System;
using Model.Options;
using Providers;
using Providers.Previews;
using UnityEngine;
using UnityEngine.UI;

namespace UI.OptionDrawers
{
    public class PreviewToggleDrawer : MonoBehaviour, IOptionDrawer
    {
        [SerializeField] private RawImage Preview;
        [SerializeField] private Toggle Toggle;
        [SerializeField] private GameObject OptionalIndicator;
        
        public event Action<Option, bool> OptionChanged;

        private IPreviewProvider _previewProvider;
        
        public Option Option { get; private set; }
        
        public void Init(Option option)
        {
            _previewProvider = ServiceLocator.Instance.GetService<IPreviewProvider>();
            Option = option;
            LoadPreview();
        }

        private async void LoadPreview()
        {
            Preview.texture = await _previewProvider.GetPreview(Option);
        }
        
        public void OnToggleChanged(bool active)
        {
            OptionChanged?.Invoke(Option, active);
        }
        
        public void SetOptionState(OptionDrawingState drawingState)
        {
            switch (drawingState)
            {
                case OptionDrawingState.Selected:
                    this.gameObject.SetActive(true);
                    Toggle.SetIsOnWithoutNotify(true);
                    break;
                case OptionDrawingState.Unselected:
                    this.gameObject.SetActive(true);
                    Toggle.SetIsOnWithoutNotify(false);
                    break;
                case OptionDrawingState.Hidden:
                default:
                    Toggle.SetIsOnWithoutNotify(false);
                    this.gameObject.SetActive(false);
                    break;
            }
        }

        public bool IsOptional 
        {
            set => OptionalIndicator.SetActive(value);
        }   
        
        private void OnDestroy()
        {
            OptionChanged = null;
            _previewProvider.DisposeResource((Texture2D)Preview.texture);
        }
    }
}