using UnityEngine;

namespace ASG.Global.Flow
{
    public class LocalFlowMachine : MonoBehaviour
    {
        [SerializeField]
        private LocalFlowState m_defaultState = null;
        private LocalFlowState m_currentState = null;

        private void Start()
        {
            SwitchToState(m_defaultState);
        }

        private void Update()
        {
            if(m_currentState)
                m_currentState.DoUpdate();
        }

        public void SwitchToState(LocalFlowState a_newState)
        {
            if (a_newState == null) return;
            
            if(m_currentState)
            {
                var objectsToDeactivate = m_currentState.AssociatedGameObjects.FindAll(obj => !a_newState.AssociatedGameObjects.Contains(obj));
                m_currentState.Exit();
                objectsToDeactivate.ForEach(obj => obj.SetActive(false));
            }
            m_currentState = a_newState;
            m_currentState.AssociatedGameObjects.ForEach(obj => obj.SetActive(true));
            m_currentState.Enter();
        }
    }
}