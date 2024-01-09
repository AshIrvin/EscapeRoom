using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<GameObject> _startEvent;
    [SerializeField] private UnityEvent<GameObject> _stopEvent;

    internal void StartEvent(GameObject other = null)
    {
        _startEvent?.Invoke(other);
    }

    internal void StopEvent(GameObject other = null)
    {
        _stopEvent?.Invoke(other);
    }
}
