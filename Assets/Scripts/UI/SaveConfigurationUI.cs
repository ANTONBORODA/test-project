using Controllers;
using Providers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SaveConfigurationUI : MonoBehaviour
    {
        public StartPageUI StartPageUI;
        public ConfigurationUI ConfigurationUI;
        public Button SaveButton;
        public TMP_InputField Name;

        public void OnSaveButtonClick()
        {
            if (string.IsNullOrWhiteSpace(Name.text)) return;
            ServiceLocator.Instance.GetService<ConfiguratorController>().SaveConfiguration(Name.text);
            TransitionToStartPage();
        }

        public void OnCancelButtonClick()
        {
            this.gameObject.SetActive(false);
        }

        private void TransitionToStartPage()
        {
            ConfigurationUI.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            StartPageUI.gameObject.SetActive(true);
        }

        public void Update()
        {
            SaveButton.interactable = !string.IsNullOrWhiteSpace(Name.text);
        }
    }
}