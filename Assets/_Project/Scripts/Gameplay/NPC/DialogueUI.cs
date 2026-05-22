using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InventixGames.Core;
using InventixGames.Core.Dialogue;

namespace HearthAndHex.NPC
{
    /// <summary>
    /// Renders a DialogueNodeSO tree.
    /// v0.2: replaced LLM streaming text with hand-authored branching nodes.
    /// Player picks a reply → optionally reports an objective → next node or close.
    /// </summary>
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup root;
        [SerializeField] private TMP_Text speakerName;
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private Transform replyContainer;
        [SerializeField] private Button replyButtonPrefab;
        [SerializeField] private Button closeButton;
        [SerializeField] private AudioSource voiceSource;
        [SerializeField] private float typewriterCps = 50f;
        private DialogueNodeSO _current;
        private Coroutine _typing;

        private void Awake()
        {
            if (closeButton) closeButton.onClick.AddListener(Close);
            Close();
        }

        public void OpenWith(DialogueNodeSO node)
        {
            root.alpha = 1; root.interactable = true; root.blocksRaycasts = true;
            ShowNode(node);
        }

        public void Close()
        {
            root.alpha = 0; root.interactable = false; root.blocksRaycasts = false;
            _current = null;
            ClearReplies();
        }

        private void ShowNode(DialogueNodeSO node)
        {
            _current = node;
            speakerName.text = node.speakerDisplayName;
            if (_typing != null) StopCoroutine(_typing);
            _typing = StartCoroutine(Typewriter(node.speakerLine));
            if (voiceSource && node.voiceOver) { voiceSource.clip = node.voiceOver; voiceSource.Play(); }
            BuildReplies(node);
        }

        private IEnumerator Typewriter(string text)
        {
            dialogueText.text = "";
            float delay = 1f / Mathf.Max(1f, typewriterCps);
            for (int i = 0; i < text.Length; i++)
            {
                dialogueText.text += text[i];
                yield return new WaitForSeconds(delay);
            }
        }

        private void ClearReplies()
        {
            if (replyContainer == null) return;
            for (int i = replyContainer.childCount - 1; i >= 0; i--)
                Destroy(replyContainer.GetChild(i).gameObject);
        }

        private void BuildReplies(DialogueNodeSO node)
        {
            ClearReplies();
            if (replyContainer == null || replyButtonPrefab == null) return;
            if (node.replies == null || node.replies.Length == 0)
            {
                var endBtn = Instantiate(replyButtonPrefab, replyContainer);
                var label = endBtn.GetComponentInChildren<TMP_Text>(); if (label) label.text = "(Goodbye.)";
                endBtn.onClick.AddListener(Close);
                return;
            }
            foreach (var r in node.replies)
            {
                var captured = r;
                var btn = Instantiate(replyButtonPrefab, replyContainer);
                var label = btn.GetComponentInChildren<TMP_Text>(); if (label) label.text = captured.playerLine;
                btn.onClick.AddListener(() => OnReplyChosen(captured));
            }
        }

        private void OnReplyChosen(DialogueNodeSO.Reply r)
        {
            if (!string.IsNullOrEmpty(r.objectiveIdToReport)
                && ServiceLocator.TryGet<InventixGames.Core.Mission.IMissionService>(out var ms))
                ms.ReportObjectiveProgress(r.objectiveIdToReport);
            if (r.next != null) ShowNode(r.next);
            else Close();
        }
    }
}
