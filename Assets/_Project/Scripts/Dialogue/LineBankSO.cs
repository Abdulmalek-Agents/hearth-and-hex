using UnityEngine;

namespace InventixGames.Core.Dialogue
{
    /// <summary>
    /// A pool of hand-authored one-shot lines (with optional VO).
    /// Used wherever the previous build did a single-line LLM call
    /// (radio Director, race commentator, coach, etc.).
    /// </summary>
    [CreateAssetMenu(menuName = "Inventix/Dialogue/Line Bank", fileName = "LineBank_")]
    public class LineBankSO : ScriptableObject
    {
        public string bankId;
        [TextArea(1, 4)] public string[] lines = System.Array.Empty<string>();
        public AudioClip[] voiceOver = System.Array.Empty<AudioClip>(); // parallel to lines
        public bool avoidImmediateRepeat = true;
        private int _lastIndex = -1;

        public string PickRandom(out AudioClip clip)
        {
            clip = null;
            if (lines == null || lines.Length == 0) return string.Empty;
            int idx = UnityEngine.Random.Range(0, lines.Length);
            if (avoidImmediateRepeat && lines.Length > 1 && idx == _lastIndex)
                idx = (idx + 1) % lines.Length;
            _lastIndex = idx;
            if (voiceOver != null && idx < voiceOver.Length) clip = voiceOver[idx];
            return lines[idx];
        }
        public string PickRandom() => PickRandom(out _);
    }
}
