using UnityEngine;

namespace HearthAndHex.Farming
{
    [CreateAssetMenu(menuName = "Hearth & Hex/Farming/Crop Data", fileName = "Crop_")]
    public class CropDataSO : ScriptableObject
    {
        public string cropId = "wheat";
        public string displayName = "Wheat";
        public Sprite icon;
        public int growMinutes = 720;
        public GameObject[] growthStagePrefabs;
        public int sellValue = 3;
        public bool needsRain;
    }
}
