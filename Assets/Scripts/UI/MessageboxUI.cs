using Providers;
using TinyMessenger;
using TinyMessenger.Events;
using UnityEngine;

namespace UI
{
    public class MessageboxUI : MonoBehaviour
    {
        public void OnYesButtonClick()
        {
            ServiceLocator.Instance.GetService<ITinyMessengerHub>().Publish(new SetSaveConfigurationUIStateEvent(this));
        }

        public void OnNoButtonClick()
        {
            ServiceLocator.Instance.GetService<ITinyMessengerHub>().Publish(new SetStartPageUIStateEvent(this));
        }
    }
}