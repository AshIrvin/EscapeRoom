using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform _playerObjectHolder;
    [SerializeField] private GameObject _heldObject;
    [SerializeField] private Transform _originalParent;
    [SerializeField] private Transform _placeObject;

    private PlayerController _playerController;
    private Raycast _raycast;

    public static Action OnButtonRelease;

    private void Start()
    {
        _raycast = GetComponent<Raycast>();
        _playerController = GetComponent<PlayerController>();
        
        Raycast.OnPointClickMoveRaycastHit += PointClickMoveRaycast;
        Raycast.OnInteractableRaycastHit += InteractableRaycast;
        PlayerController.OnPlayerRotate += ToggleHeldObjectMesh;

        if (_playerObjectHolder == null) Debug.LogError("Missing _targetHolder");
    }

    private void Update()
    {
        UseInteraction();

        if (Input.GetMouseButtonDown(0))
        {
            _raycast.PointClickMoveRaycast();
        }
    }

    private void UseInteraction()
    {
        SendRaycastOnButtonPress();
    }

    private void InteractableRaycast(RaycastHit hit)
    {
        GetObject(hit.collider);
    }

    private void PointClickMoveRaycast(RaycastHit hit)
    { // TODO - This should be it's own door script with a locked bool
        if (hit.transform.name.Contains("Locked"))
        {
            LockedDoor(hit.transform.GetComponent<EventManager>());
            return;
        }

        _playerController.SetPlayerPosition(hit.transform);
    }

    private void LockedDoor(EventManager eventManager)
    {
        eventManager.StartEvent();
    }

    private void SendRaycastOnButtonPress()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _raycast.InteractableRaycast();
        }
    }

    private void GetObject(Collider collider)
    {
        if (collider == null) return;

        switch (collider.tag)
        {
            case "HeldObject":
                if (_heldObject != null) return;
                _heldObject = collider.gameObject;
                HoldObject();
                break;
            case "PlaceObject":
                if (_heldObject == null) return;
                _placeObject = collider.transform;
                PlaceObjectInHolder(_placeObject);
                break;
        }
    }

    private void PlaceObjectInHolder(Transform holder)
    {
        if (_placeObject == null) return;

        var pos = holder.position;
        _heldObject.transform.SetParent(_originalParent, false);
        _heldObject.transform.SetPositionAndRotation(pos, holder.rotation);
        _heldObject = null;
        _originalParent = null;
        _placeObject = null;
    }

    private void HoldObject()
    {
        if (_heldObject == null) return;

        _originalParent = _heldObject.transform.parent;
        
        _heldObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        _heldObject.transform.SetParent(_playerObjectHolder.transform, false);

        ToggleHeldObjectMesh();
    }

    internal void ToggleHeldObjectMesh()
    { // TODO - Temp fix. Object disappears from view randomly? 
        if (_heldObject == null ) return;

        var meshRenderer = _heldObject.GetComponent<MeshRenderer>();
        meshRenderer = meshRenderer != null ? meshRenderer : _heldObject.GetComponentInChildren<MeshRenderer>();

        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
            meshRenderer.enabled = true;
        }
    }
}
