using UnityEngine;

namespace ASG.Gameplay.Building
{
    [CreateAssetMenu(fileName = "BuildingElementData", menuName = "ASG/Gameplay/Building/BuildingElementData")]
    public class BuildingElementData : ScriptableObject
    {
        [System.Serializable]
        public struct BuildingElementMetaData
        {
            public string name;
            public Sprite icon;
            public string description;
        }

        [SerializeField]
        private BuildingElementData m_metaData = null;
        public BuildingElementData MetaData => m_metaData;

        [SerializeField]
        private Fusion.NetworkPrefabRef m_buildingElementPrefab = default;
        public Fusion.NetworkPrefabRef buildingElementPrefab => m_buildingElementPrefab;

        public Vector3 GetClosestValidPositions(Vector3 a_positions)
        {
            return default;
        }
    }
}