using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform _targetHolder;

    private PlayerController _playerController;
    private readonly float _smoothTime = 0.2f;
    private Raycast _raycast;
    private Camera _cam;
    private GameObject _heldObject;
    private Vector3 _currentVelocity = Vector3.zero;
    private bool _sendToPlayer;

    public static Action OnButtonRelease;

    private void Start()
    {
        _cam = Camera.main;
        _raycast = GetComponent<Raycast>();
        _playerController = GetComponent<PlayerController>();
        
        Raycast.OnPointClickMoveRaycastHit += PointClickMoveRaycast;
        Raycast.OnInteractableRaycastHit += InteractableRaycast;

        if (_targetHolder == null)
            _targetHolder = _cam.transform.Find("TargetDragObject");
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
        if (Input.GetMouseButtonUp(0))
        {
            StopMovingObject();
            return;
        }

        if (IsHoldingObject())
        {
            return;
        }

        SendRaycastOnButtonPress();
    }

    private void InteractableRaycast(RaycastHit hit)
    {
        GetObject(hit.collider);
    }

    private void PointClickMoveRaycast(RaycastHit hit)
    {
        _playerController.SetPlayerPosition(hit.transform);
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
                _sendToPlayer = true;
                _heldObject = collider.gameObject;
                break;
            case "PlaceObject":
                if (_heldObject == null) return;
                collider.transform.position = _heldObject.transform.position;
                break;
        }
    }

    private bool IsHoldingObject()
    {
        if (!_sendToPlayer || _heldObject == null) return false;

        _heldObject.transform.position = Vector3.SmoothDamp(_heldObject.transform.position, _targetHolder.position, ref _currentVelocity, _smoothTime);
        
        if (Vector3.Distance(_heldObject.transform.position, _targetHolder.position) < 0.1f)
        {
            StopMovingObject();
        }

        return true;
    }

    private void StopMovingObject()
    {
        _sendToPlayer = false;
        _heldObject = null;
    }
}
