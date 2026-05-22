using UnityEngine;

namespace InventixGames.Core
{
    [CreateAssetMenu(menuName = "Inventix/AI Copilot/Persona", fileName = "Persona_")]
    public class AICopilotPersonaSO : ScriptableObject
    {
        public string npcId;
        public string displayName = "Stranger";
        [TextArea(6, 20)]
        public string systemPrompt = "You are a character in a video game. Stay in character. Reply in 1–2 short sentences unless asked for detail.";
        [Range(0f, 1f)] public float temperature = 0.7f;
        public bool useShortTermMemory = true;
    }
}
