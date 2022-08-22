using Fusion;
using UnityEngine;

namespace ASG.Gameplay.Building
{
    [RequireComponent(typeof(BuildingInventory))]
    public class Builder : NetworkBehaviour
    {
        private BuildingInventory m_buildingInventory = null;

        private void Awake()
        {
            m_buildingInventory = GetComponent<BuildingInventory>();
        }

        public void PlaceCurrentBuilding()
        {

        }
    }
}