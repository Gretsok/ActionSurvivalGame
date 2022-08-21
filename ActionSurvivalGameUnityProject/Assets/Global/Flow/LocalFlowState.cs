using System.Collections.Generic;
using UnityEngine;

namespace ASG.Global.Flow
{
    public class LocalFlowState : MonoBehaviour
    {
        public List<GameObject> AssociatedGameObjects = new List<GameObject>();

        #region Life Cycle
        protected virtual void Awake()
        {
            AssociatedGameObjects?.ForEach(obj => obj.SetActive(false));
        }

        public void Enter()
        {
            SetUpReferences();
            RegisterEvents();
        }

        protected virtual void SetUpReferences()
        {

        }

        protected virtual void RegisterEvents()
        {

        }
        

        public void DoUpdate()
        {
            HandleUpdate();
        }

        protected virtual void HandleUpdate()
        {

        }

        public void Exit()
        {
            UnregisterEvents();
            CleanUpReferences();
        }

        protected virtual void CleanUpReferences()
        {

        }

        protected virtual void UnregisterEvents()
        {

        }
        #endregion

        #region Specific Accessors
        public T GetFirstAssociatedObjectComponent<T>() where T : MonoBehaviour
        {
            for(int i = 0; i < AssociatedGameObjects.Count; ++i)
            {
                if(AssociatedGameObjects[i].TryGetComponent<T>(out T l_comp))
                {
                    return l_comp;
                }
            }
            return (T) null;
        }

        public List<T> GetAllAssociatedObjectComponents<T>()
        {
            List<T> comps = new List<T>();
            for (int i = 0; i < AssociatedGameObjects.Count; ++i)
            {
                if (AssociatedGameObjects[i].TryGetComponent<T>(out T l_comp))
                {
                    comps.Add(l_comp);
                }
            }
            return comps;
        }
        #endregion
    }
}