using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Raycast : MonoBehaviour
{
    [SerializeField] private LayerMask _interactionLayerMask;
    [SerializeField] private LayerMask _staticCameraPositions;
    [SerializeField] private float _useDistance;
    [SerializeField] private Camera _cam;

    public static Action<RaycastHit> OnPointClickMoveRaycastHit;
    public static Action<RaycastHit> OnInteractableRaycastHit;

    public bool pointerGo;

    internal void InteractableRaycast()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (EventSystem.current.IsPointerOverGameObject() &&
            Physics.Raycast(ray, out RaycastHit hit, _useDistance, _interactionLayerMask))
        {
            OnInteractableRaycastHit?.Invoke(hit);
        }
    }

    internal void PointClickMoveRaycast()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        pointerGo = EventSystem.current.IsPointerOverGameObject();

        if (!EventSystem.current.IsPointerOverGameObject() &&
            Physics.Raycast(ray, out RaycastHit hit, _useDistance, _staticCameraPositions))
        {
            OnPointClickMoveRaycastHit?.Invoke(hit);
        }
    }
}
