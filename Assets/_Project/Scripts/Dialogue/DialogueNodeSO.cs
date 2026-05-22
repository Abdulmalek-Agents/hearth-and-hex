using UnityEngine;

namespace InventixGames.Core.Dialogue
{
    /// <summary>
    /// Hand-authored branching dialogue node.
    /// Replaces the previous LLM-driven runtime dialogue (deleted in v0.2).
    /// Writers iterate freely — no recompile required.
    /// </summary>
    [CreateAssetMenu(menuName = "Inventix/Dialogue/Node", fileName = "Node_")]
    public class DialogueNodeSO : ScriptableObject
    {
        public string nodeId;
        public string speakerId;
        public string speakerDisplayName = "Stranger";
        [TextArea(2, 6)] public string speakerLine;
        public AudioClip voiceOver;
        public Reply[] replies = System.Array.Empty<Reply>();

        [System.Serializable]
        public class Reply
        {
            [TextArea(1, 3)] public string playerLine;
            public DialogueNodeSO next; // null = end of conversation
            public string objectiveIdToReport; // optional — reported to MissionService when chosen
        }
    }
}
