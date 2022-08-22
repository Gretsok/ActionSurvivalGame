
using Fusion;
using UnityEngine;

namespace ASG.Gameplay.Building
{
    public static class BuildingUtils
    {
        public const float TILE_SIZE = 3.5f;
        public const float HEIGHT_STEP = 3.5f;
        public static Vector3 BUILDING_OFFSET_TO_CHARACTER = new Vector3(0f, 0f, 1f);


        public static Vector3 GetClosestValidPositions(EBuildingType a_type, Vector3 a_position)
        {
            if(a_type == EBuildingType.Floor)
            {
                Vector3 validPosition = default;
                validPosition.x = Mathf.Round(a_position.x / TILE_SIZE) * TILE_SIZE;
                validPosition.y = Mathf.Round(a_position.y / HEIGHT_STEP) * HEIGHT_STEP;
                validPosition.z = Mathf.Round(a_position.z / TILE_SIZE) * TILE_SIZE;
                return validPosition;
            }
            else if(a_type == EBuildingType.Wall)
            {
                Vector3 validPosition = default;
                validPosition.x = Mathf.Round(a_position.x / TILE_SIZE) * TILE_SIZE;
                validPosition.y = Mathf.Round(a_position.y / HEIGHT_STEP) * HEIGHT_STEP;
                validPosition.z = Mathf.Round(a_position.z / TILE_SIZE) * TILE_SIZE;
                return validPosition;
            }


            return a_position;
        }

        public static Quaternion GetValidRotation(EBuildingType a_type, Quaternion a_rotation)
        {
            if (a_type == EBuildingType.Floor)
            {

            }
            else if (a_type == EBuildingType.Wall)
            {

            }
            return a_rotation;
        }
    }
}