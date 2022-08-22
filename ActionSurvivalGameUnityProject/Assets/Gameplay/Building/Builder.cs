using Fusion;
using UnityEngine;

namespace ASG.Gameplay.Building
{
    [RequireComponent(typeof(BuildingInventory))]
    public class Builder : NetworkBehaviour
    {
        [SerializeField]
        private Character.AInputsHandler m_inputsHandler = null;
        private BuildingInventory m_buildingInventory = null;
        [Networked]
        private NetworkBool m_isActivated { get; set; }

        [Networked]
        private int m_currentIndex { get; set; }

        private void Awake()
        {
            m_buildingInventory = GetComponent<BuildingInventory>();
        }

        public override void Spawned()
        {
            base.Spawned();
            m_isActivated = false;
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            m_inputsHandler.OnPlaceCurrentBuildingAsked += PlaceCurrentBuilding;
            m_inputsHandler.OnSwitchToNextBuildingAsked += SwitchToNextBuilding;
            m_inputsHandler.OnSwitchToPreviousBuildingAsked += SwitchToPreviousBuilding;
            m_inputsHandler.OnToggleBuildingModeAsked += ToggleBuildingMode;
        }

        public void PlaceCurrentBuilding()
        {
            Debug.Log($"Place current building as Player :{Object.InputAuthority}");
            if(m_isActivated)
            {
                Vector3 positionToBuild = transform.TransformPoint(BuildingUtils.BUILDING_OFFSET_TO_CHARACTER);
                var data = m_buildingInventory.GetBuildingElementData(m_currentIndex);
                Runner.Spawn(data.buildingElementPrefab,
                    position: BuildingUtils.GetClosestValidPositions(data.MetaData.buildingType, positionToBuild),
                    rotation: BuildingUtils.GetValidRotation(data.MetaData.buildingType, transform.rotation));
            }
        }

        public void ToggleBuildingMode()
        {
            m_isActivated = !m_isActivated;
            Debug.Log($"Toggle building mode (is now activated: {m_isActivated}) as Player :{Object.InputAuthority}");
        }

        public void SwitchToNextBuilding()
        {
            m_currentIndex = (m_currentIndex + 1) % m_buildingInventory.HeldBuildElementsCount;
            Debug.Log($"Switch to next building (new id {m_currentIndex} as Player :{Object.InputAuthority}");
        }

        public void SwitchToPreviousBuilding()
        {
            m_currentIndex--;
            if(m_currentIndex < 0)
            {
                m_currentIndex = m_buildingInventory.HeldBuildElementsCount - 1;
            }
        }
    }
}