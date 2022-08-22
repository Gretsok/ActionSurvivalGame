using System.Collections.Generic;
using UnityEngine;

namespace ASG.Gameplay.Building
{
    public class BuildingElementsManager : MonoBehaviour
    {
        #region Singleton
        private static BuildingElementsManager s_instance = null;
        public static BuildingElementsManager Instance
        {
            get
            {
                if(s_instance == null)
                {
                    var managerPrefab = Resources.Load<BuildingElementsManager>("BuildingElementsManager");
                    s_instance = Instantiate(managerPrefab);
                    DontDestroyOnLoad(s_instance);
                }
                return s_instance;
            }
        }
        #endregion

        [SerializeField]
        private List<BuildingElementData> m_allBuildingElementsData = null;
        public List<BuildingElementData> AllBuildingElementsData => m_allBuildingElementsData;
    }
}