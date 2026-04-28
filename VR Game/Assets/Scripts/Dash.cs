using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class VRDash : MonoBehaviour
{
    public CharacterController controller;
    public Transform head; // VR camera (direction reference)
    public InputActionProperty DashButton;

    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private float dashTime;
    private float cooldownTime;
    private Vector3 dashDirection;

    void Update()
    {
        // Example input (replace with your VR input system)
        if (DashButton.action.WasPressedThisFrame() && cooldownTime <= 0)
        {
            StartDash();
        }

        if (dashTime > 0)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            dashTime -= Time.deltaTime;
        }

        if (cooldownTime > 0)
            cooldownTime -= Time.deltaTime;
    }

    void StartDash()
    {
        dashTime = dashDuration;
        cooldownTime = dashCooldown;

        // Dash in the direction the headset is facing (flattened)
        Vector3 forward = head.forward;
        forward.y = 0;
        dashDirection = forward.normalized;
    }
}
