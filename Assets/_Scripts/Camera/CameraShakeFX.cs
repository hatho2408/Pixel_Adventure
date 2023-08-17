using Cinemachine;
using UnityEngine;

public class CameraShakeFX : MonoBehaviour
{
    [SerializeField]private CinemachineImpulseSource impulseSource;
    [SerializeField] private Vector3 shakeDirection;
    [SerializeField] private float forceMultiplier;

    public void ShakeScreen(int facingDirection)
    {
        impulseSource.m_DefaultVelocity=new Vector3(shakeDirection.x,shakeDirection.y)*forceMultiplier;
        impulseSource.GenerateImpulse();
    }
   
}
