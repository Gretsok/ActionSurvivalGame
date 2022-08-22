using UnityEngine;

namespace ASG.Gameplay.Building
{
    [CreateAssetMenu(fileName = "BuildingElementData", menuName = "ASG/Gameplay/Building/BuildingElementData")]
    public class BuildingElementData : ScriptableObject
    {
        [SerializeField, HideInInspector]
        private int m_dataId = -1;
        public int DataID => m_dataId;

        [System.Serializable]
        public struct BuildingElementMetaData
        {
            public string name;
            public Sprite icon;
            public string description;
            public EBuildingType buildingType;
        }

        [SerializeField]
        private BuildingElementMetaData m_metaData = default;
        public BuildingElementMetaData MetaData => m_metaData;

        [SerializeField]
        private Fusion.NetworkPrefabRef m_buildingElementPrefab = default;
        public Fusion.NetworkPrefabRef buildingElementPrefab => m_buildingElementPrefab;

#if UNITY_EDITOR
        public void EditorOnly_GenerateRandomDataID()
        {
            m_dataId = Random.Range(int.MinValue, int.MaxValue);
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(BuildingElementData), true)]
    public class BuildingElementDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(30f);
            if (GUILayout.Button("Generate Random Data ID"))
            {
                (target as BuildingElementData).EditorOnly_GenerateRandomDataID();
            }
            GUILayout.Label($"Data ID : {(target as BuildingElementData).DataID}");

        }
    }
#endif
}