using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
public class move : MonoBehaviour

{
    public Vector3 pointB;
    public float maxSpeed = 1;
    private CharacterController m_CharacterController;
    private CollisionFlags m_CollisionFlags;
    // this is an object, so that you can move it around in the editor.

    // units per second
 

    void Update()
    {
        var change = maxSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pointB, change);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        //dont move the rigidbody if the character is on top of it
        if (m_CollisionFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }
}
