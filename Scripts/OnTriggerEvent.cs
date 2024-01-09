using UnityEngine;

public class OnTriggerEvent : MonoBehaviour
{
    [SerializeField] private GameObject _matchObject;
    [SerializeField] private bool _matchName;
    
    private EventManager _eventManager;

    private void Start()
    {
        _eventManager = GetComponent<EventManager>();

        if (_eventManager == null) Debug.Log($"No Event Manager attached to {name} gameobject.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (HasMatchedObject(other) && _eventManager != null)
        {
            _eventManager.StartEvent(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (HasMatchedObject(other) && _eventManager != null)
        {
            _eventManager.StopEvent(gameObject);
        }
    }

    private bool HasMatchedObject(Collider other)
    { // No object assigned to match object, then any will do
        if (_matchName)
        {
            Debug.Log($"other {name}");
            if (other.name.Contains(_matchObject.name))
            {
                return true;
            }
        }

        if (_matchObject == null || _matchObject == other.gameObject)
            return true;

        return false;
    }
}
