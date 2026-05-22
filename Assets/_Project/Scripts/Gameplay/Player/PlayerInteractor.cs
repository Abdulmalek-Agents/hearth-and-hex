using UnityEngine;
using UnityEngine.InputSystem;
using HearthAndHex.Farming;

namespace HearthAndHex.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private float radius = 1.8f;
        [SerializeField] private LayerMask layerMask = ~0;
        [SerializeField] private UnityEngine.UI.Text promptLabel;
        private IInteractable _current;

        private void Update()
        {
            var origin = transform.position + transform.forward * 0.4f + Vector3.up * 0.5f;
            var cols = Physics.OverlapSphere(origin, radius, layerMask, QueryTriggerInteraction.Collide);
            float best = float.MaxValue;
            _current = null;
            foreach (var c in cols)
            {
                if (!c.TryGetComponent<IInteractable>(out var ix)) continue;
                float d = (c.transform.position - transform.position).sqrMagnitude;
                if (d < best) { best = d; _current = ix; }
            }
            if (promptLabel) promptLabel.text = _current != null && !string.IsNullOrEmpty(_current.Prompt) ? $"[E] {_current.Prompt}" : "";
        }
        public void OnInteract(InputAction.CallbackContext ctx) { if (ctx.performed && _current != null) _current.Interact(gameObject); }
    }
}
