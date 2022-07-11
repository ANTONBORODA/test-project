using Controllers;
using Providers;
using TinyMessenger;
using TinyMessenger.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SaveConfigurationUI : MonoBehaviour
    {
        public Button SaveButton;
        public TMP_InputField Name;

        public void OnSaveButtonClick()
        {
            if (string.IsNullOrWhiteSpace(Name.text)) return;
            ServiceLocator.Instance.GetService<ConfiguratorController>().SaveConfiguration(Name.text);
            ServiceLocator.Instance.GetService<ITinyMessengerHub>().Publish(new SetStartPageUIStateEvent(this));
        }

        public void OnCancelButtonClick()
        {
            ServiceLocator.Instance.GetService<ITinyMessengerHub>().Publish(new SetConfigurationUIStateEvent(this));
        }

        public void Update()
        {
            SaveButton.interactable = !string.IsNullOrWhiteSpace(Name.text);
        }
    }
}