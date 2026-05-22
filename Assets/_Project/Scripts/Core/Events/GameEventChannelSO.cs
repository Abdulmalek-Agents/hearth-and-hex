using UnityEngine;
using UnityEngine.Events;

namespace InventixGames.Core.Events
{
    [CreateAssetMenu(menuName = "Inventix/Events/Void", fileName = "VoidEventChannel_")]
    public class VoidEventChannelSO : ScriptableObject { public UnityAction OnEventRaised; public void Raise() => OnEventRaised?.Invoke(); }

    [CreateAssetMenu(menuName = "Inventix/Events/String", fileName = "StringEventChannel_")]
    public class StringEventChannelSO : ScriptableObject { public UnityAction<string> OnEventRaised; public void Raise(string s) => OnEventRaised?.Invoke(s); }

    [CreateAssetMenu(menuName = "Inventix/Events/Int", fileName = "IntEventChannel_")]
    public class IntEventChannelSO : ScriptableObject { public UnityAction<int> OnEventRaised; public void Raise(int i) => OnEventRaised?.Invoke(i); }
}
