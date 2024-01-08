using System;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    public enum Interactable
    {
        ObjectHolder,
        EnteredVolume,
        PlayAnimation
    }

    [SerializeField] private Interactable _interactable;
    [SerializeField] private GameObject _matchObject;
    [SerializeField] private UnityEvent _startEvent;
    [SerializeField] private UnityEvent _stopEvent;

    private void OnTriggerEnter(Collider other)
    {
        SwitchEvents(other);
    }

    private void SwitchEvents(Collider other)
    {
        switch (_interactable)
        {
            case Interactable.ObjectHolder:
                if (HasMatchedObject(other))
                    GrabObject(other);
                break;
            case Interactable.EnteredVolume: // TODO - this was for keypad
                //if (HasMatchedObject(other))
                break;
            case Interactable.PlayAnimation:
                if (HasMatchedObject(other))
                    StartEvent();
                break;
        }
    }

    private void StartEvent()
    {
        _startEvent?.Invoke();
    }

    private void StopEvent()
    {
        _stopEvent?.Invoke();
    }

    private bool HasMatchedObject(Collider other)
    { // No object assigned to match object, then any will do
        if (_matchObject == null || _matchObject == other.gameObject)
            return true;

        return false;
    }

    private void GrabObject(Collider other)
    {
        if (other.CompareTag("HeldObject"))
        {
            other.transform.position = transform.position;
        }
    }
}
