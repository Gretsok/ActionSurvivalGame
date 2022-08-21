using Fusion;
using UnityEngine;

namespace ASG.Gameplay.Utils
{
    public class CallbacksManagerCatcher : NetworkBehaviour
    {
        private NetworkCallbacksManager m_callbacksManager = null;
        public override void Spawned()
        {
            base.Spawned();
            Debug.Log("Spawned");
            m_callbacksManager = FindObjectOfType<NetworkCallbacksManager>();
            if(m_callbacksManager)
            {
                m_callbacksManager.GetComponent<NetworkRunner>().AddCallbacks(m_callbacksManager);
                m_callbacksManager.CheckAllPlayersHaveACharacter(m_callbacksManager.GetComponent<NetworkRunner>());
            }
            else
            {
                Debug.LogError("Could not find NetworkCallbacksManager");
            }
        }
    }
}