using UnityEngine;
using HearthAndHex.Farming;
using InventixGames.Core;
using InventixGames.Core.Dialogue;

namespace HearthAndHex.NPC
{
    [RequireComponent(typeof(Collider))]
    public class VillagerNpc : MonoBehaviour, IInteractable
    {
        [SerializeField] private string displayName = "Villager";
        [SerializeField] private DialogueNodeSO rootDialogue;
        [SerializeField] private string meetObjectiveId;
        [SerializeField] private DialogueUI dialogueUI;
        private bool _hasBeenMet;
        public string Prompt => $"Talk to {displayName}";

        public void Interact(GameObject actor)
        {
            if (dialogueUI == null) { Debug.LogWarning("DialogueUI not assigned on " + name); return; }
            if (rootDialogue == null) { Debug.LogWarning("rootDialogue not assigned on " + name); return; }
            dialogueUI.OpenWith(rootDialogue);
            if (!_hasBeenMet && !string.IsNullOrEmpty(meetObjectiveId))
            {
                _hasBeenMet = true;
                if (ServiceLocator.TryGet<InventixGames.Core.Mission.IMissionService>(out var ms))
                    ms.ReportObjectiveProgress(meetObjectiveId);
            }
        }
    }
}
