using UnityEngine;

namespace HearthAndHex.Time
{
    public class TimeOfDay : MonoBehaviour
    {
        [SerializeField] private float gameMinutesPerRealSecond = 0.4f;
        [Range(0, 1440)][SerializeField] private float currentMinutes = 360;
        public float CurrentMinutes => currentMinutes;
        public bool IsDay => currentMinutes >= 360f && currentMinutes <= 1080f;
        private void Update() { currentMinutes += gameMinutesPerRealSecond * UnityEngine.Time.deltaTime * 60f; if (currentMinutes >= 1440f) currentMinutes -= 1440f; }
    }
}
