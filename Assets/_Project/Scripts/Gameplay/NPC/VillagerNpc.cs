using UnityEngine;
using HearthAndHex.Farming;
using InventixGames.Core;

namespace HearthAndHex.NPC
{
    [RequireComponent(typeof(Collider))]
    public class VillagerNpc : MonoBehaviour, IInteractable
    {
        [SerializeField] private AICopilotPersonaSO persona;
        [SerializeField] private string meetObjectiveId;
        [SerializeField] private DialogueUI dialogueUI;
        private bool _hasBeenMet;
        public string Prompt => persona ? $"Talk to {persona.displayName}" : "Talk";

        public void Interact(GameObject actor)
        {
            if (dialogueUI == null) { Debug.LogWarning("DialogueUI not assigned on " + name); return; }
            dialogueUI.OpenWith(persona);
            if (!_hasBeenMet && !string.IsNullOrEmpty(meetObjectiveId))
            {
                _hasBeenMet = true;
                if (ServiceLocator.TryGet<InventixGames.Core.Mission.IMissionService>(out var ms))
                    ms.ReportObjectiveProgress(meetObjectiveId);
            }
        }
    }
}
