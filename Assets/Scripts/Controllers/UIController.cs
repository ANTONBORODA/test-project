using System.Collections.Generic;
using Providers;
using TinyMessenger;
using TinyMessenger.Events;
using UI;
using UnityEngine;

namespace Controllers
{
    public class UIController : MonoBehaviour
    {
        public GameObject StartPage;
        public GameObject ConfigurationPage;
        public GameObject SavePage;
        public GameObject MessageboxUI;

        private ITinyMessengerHub _eventBus;
        private List<TinyMessageSubscriptionToken> _subscriptions = new();

        private void Start()
        {
            _eventBus = ServiceLocator.Instance.GetService<ITinyMessengerHub>();
            _subscriptions.Add(_eventBus.Subscribe<SetConfigurationUIStateEvent>(OnSetConfigurationUIStateEvent));
            _subscriptions.Add(_eventBus.Subscribe<SetSaveConfigurationUIStateEvent>(OnSetSaveConfigurationUIStateEvent));
            _subscriptions.Add(_eventBus.Subscribe<SetStartPageUIStateEvent>(OnSetStartPageUIStateEvent));
            _eventBus.Publish(new SetStartPageUIStateEvent(this));
        }

        private void OnSetStartPageUIStateEvent(SetStartPageUIStateEvent obj)
        {
            if (obj.Sender.GetType() == typeof(ConfigurationUI))
            {
                MessageboxUI.SetActive(true);
            }
            else
            {
                StartPage.SetActive(true);
                SavePage.SetActive(false);
                ConfigurationPage.SetActive(false);
                MessageboxUI.SetActive(false);       
            }
        }

        private void OnSetSaveConfigurationUIStateEvent(SetSaveConfigurationUIStateEvent obj)
        {
            StartPage.SetActive(false);
            SavePage.SetActive(true);
            MessageboxUI.SetActive(false);
        }

        private void OnDestroy()
        {
            foreach (var token in _subscriptions)
            {
                _eventBus.Unsubscribe(token);
            }
        }

        private void OnSetConfigurationUIStateEvent(SetConfigurationUIStateEvent obj)
        {
            MessageboxUI.SetActive(false);
            StartPage.SetActive(false);
            ConfigurationPage.SetActive(true);
            SavePage.SetActive(false);
        }
    }
}