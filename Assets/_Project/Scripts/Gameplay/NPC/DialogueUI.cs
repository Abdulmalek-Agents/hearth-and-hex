using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InventixGames.Core;

namespace HearthAndHex.NPC
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup root;
        [SerializeField] private TMP_Text speakerName;
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private TMP_InputField playerInputField;
        [SerializeField] private Button sendButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private float typewriterCps = 50f;
        private AICopilotPersonaSO _persona;
        private IAICopilotService _copilot;
        private Coroutine _typing;

        private void Awake()
        {
            sendButton.onClick.AddListener(OnSend);
            closeButton.onClick.AddListener(Close);
            Close();
        }
        public void OpenWith(AICopilotPersonaSO persona)
        {
            _persona = persona;
            _copilot = ServiceLocator.Get<IAICopilotService>();
            speakerName.text = persona.displayName;
            dialogueText.text = "..."; playerInputField.text = "";
            root.alpha = 1; root.interactable = true; root.blocksRaycasts = true;
            playerInputField.Select(); playerInputField.ActivateInputField();
            _copilot.Ask(persona.systemPrompt, "The player has just walked up. Greet them briefly in character.", OnReply);
        }
        public void Close() { root.alpha = 0; root.interactable = false; root.blocksRaycasts = false; _persona = null; }

        private void OnSend()
        {
            if (_persona == null) return;
            string playerLine = playerInputField.text.Trim();
            if (string.IsNullOrEmpty(playerLine)) return;
            playerInputField.text = "";
            string memorySummary = "";
            if (_persona.useShortTermMemory)
            {
                var mem = _copilot.GetMemory(_persona.npcId);
                if (mem.Count > 0) memorySummary = "Recent facts you remember: " + string.Join("; ", mem);
            }
            string sys = _persona.systemPrompt + (string.IsNullOrEmpty(memorySummary) ? "" : "\n\n" + memorySummary);
            dialogueText.text = "...";
            _copilot.Ask(sys, playerLine, OnReply);
            _copilot.RememberFact(_persona.npcId, $"Player said: {playerLine}");
        }

        private void OnReply(string text)
        {
            if (_typing != null) StopCoroutine(_typing);
            _typing = StartCoroutine(Typewriter(text));
        }

        private IEnumerator Typewriter(string text)
        {
            dialogueText.text = "";
            float delay = 1f / typewriterCps;
            for (int i = 0; i < text.Length; i++) { dialogueText.text += text[i]; yield return new WaitForSeconds(delay); }
        }
    }
}
