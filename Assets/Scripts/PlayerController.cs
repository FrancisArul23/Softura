using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Space]
    [Header("Player Components")]
    [Space]
    [SerializeField] private InputAction playerActions;
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private float moveSpeed;
    
    [Space]
    [Header("Camera Components")]
    [Space]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector3 cameraOffset;
    
    [Space]
    [Header("Collision Things")]
    [Space]
    [SerializeField] private List<Material> materials;
    [SerializeField] private Renderer tableMeshRenderer;

    private void OnEnable()
    {
        playerActions.Enable();
        
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    /// <summary>
    /// Calls when the Ball gets nearer to the Table
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Table"))
        {
            ChangeColor(true);
        }
    }
    
    /// <summary>
    /// Calls when the ball gets away from the Table for 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Table"))
        {
            ChangeColor(false);
        }
    }
    
    /// <summary>
    /// Change the color of the Table according to the state
    /// </summary>
    /// <param name="state">Defines the player pos whether it is near to target or not</param>
    private void ChangeColor(bool state)
    {
        tableMeshRenderer.material = state ? materials[0] : materials[1];
    }


    /// <summary>
    /// Moves the player transform according to the user inputs from the Input System
    /// </summary>
    private void FixedUpdate()
    {
        playerRigidBody.AddForce(playerActions.ReadValue<Vector3>() * moveSpeed);
    }
    
    /// <summary>
    /// Moves the camera with the player with a certain distance
    /// </summary>
    private void LateUpdate()
    {
        cameraTransform.position = transform.position + cameraOffset;
    }
}
 