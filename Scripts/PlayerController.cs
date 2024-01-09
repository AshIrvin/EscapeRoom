using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 8;
    [SerializeField] private float _playerRotationSpeed = 230;
    [SerializeField] private float _waypointDistanceOffset = 1.32f;
    [SerializeField] private float _rotationDistanceOffset = 0.1f;

    public float distance;
    public float rotDistance;

    public static Action OnPlayerRotate;

    private void Start()
    {
        UiManager.OnLeftButton += RotatePlayerLeft;
        UiManager.OnRightButton += RotatePlayerRight;
    }

    internal void SetPlayerPosition(Transform hit)
    {
        transform.SetPositionAndRotation(hit.position, hit.rotation);
    }

    private void RotatePlayerLeft()
    {
        transform.rotation = Quaternion.LookRotation(-transform.right, Vector3.up);
        OnPlayerRotate?.Invoke();
    }

    private void RotatePlayerRight() 
    {
        transform.rotation = Quaternion.LookRotation(transform.right, Vector3.up);
        OnPlayerRotate?.Invoke();
    }
}
