using Fusion;
using UnityEngine;

namespace ASG.Gameplay.Building
{
    public class BuildingInventory : NetworkBehaviour
    {
        [SerializeField]
        private BuildingElementData[] m_defaultElementData = null;
        [Networked, Capacity(4)]
        NetworkLinkedList<int> m_heldBuildElementsDataID => default;

        public int HeldBuildElementsCount => m_heldBuildElementsDataID.Count;
        public BuildingElementData GetBuildingElementData(int a_index)
        {
            if(m_heldBuildElementsDataID.Count == 0)
            {
                return null;
            }

            int dataId = m_heldBuildElementsDataID[a_index % m_heldBuildElementsDataID.Count];
            return BuildingElementsManager.Instance.AllBuildingElementsData.Find(data => data.DataID == dataId);
        }

        public override void Spawned()
        {
            base.Spawned();
            for(int i = 0; i < m_defaultElementData.Length; ++i)
            {
                m_heldBuildElementsDataID.Add(m_defaultElementData[i].DataID);
            }
        }
    }
}