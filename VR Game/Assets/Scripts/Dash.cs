using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
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
    
    
    
    [SerializeField] VolumeProfile volumeProfile;
    [SerializeField] VolumeSettings volumeSettings;
    private bool m_HasRetrievedVolumeComponent;
    private Vignette m_VolumeComponent;

    [Serializable]
    private struct VolumeSettings
    {
        public bool active;
        public ColorParameter color;
        public Vector2Parameter center;
        public ClampedFloatParameter intensity;
        public ClampedFloatParameter smoothness;
        public BoolParameter rounded;


        public void SetVolumeComponentSettings(ref Vignette volumeComponent)
        {
            volumeComponent.active = active;
            volumeComponent.color = color;
            volumeComponent.center = center;
            volumeComponent.intensity = intensity;
            volumeComponent.smoothness = smoothness;
            volumeComponent.rounded = rounded;
        }

        public void GetVolumeComponentSettings(ref Vignette volumeComponent)
        {
            active = volumeComponent.active;
            color = volumeComponent.color;
            center = volumeComponent.center;
            intensity = volumeComponent.intensity;
            smoothness = volumeComponent.smoothness;
            rounded = volumeComponent.rounded;
        }
    }
    private static bool GetVolumeComponent(in VolumeProfile volumeProfile, ref Vignette volumeComponent)
    {
        if (volumeComponent != null)
            return true;

        if (volumeProfile == null)
        {
            Debug.LogError("ModifyVolumeComponent.GetVolumeComponent():\nvolumeProfile has not been assigned.");
            return false;
        }

        volumeProfile.TryGet(out Vignette component);
        if (component == null)
        {
            Debug.LogError($"ModifyVolumeComponent.GetVolumeComponent():\nMissing component in the \"{volumeProfile.name}\" VolumeProfile ");
            return false;
        }

        volumeComponent = component;
        return true;
    }

    private void Start()
    {
        m_HasRetrievedVolumeComponent = GetVolumeComponent(in volumeProfile, ref m_VolumeComponent);
        if (m_HasRetrievedVolumeComponent)
            volumeSettings.GetVolumeComponentSettings(ref m_VolumeComponent);
    }
    void Update()
    {
        if (!m_HasRetrievedVolumeComponent)
            return;

        volumeSettings.SetVolumeComponentSettings(ref m_VolumeComponent);


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
        else if(dashTime <= 0)
        {
            volumeSettings.active = false;
        }

        if (cooldownTime > 0)
            cooldownTime -= Time.deltaTime;
    }

    void StartDash()
    {
        volumeSettings.active = true;
        //vignette.intensity.value = .6f;
        dashTime = dashDuration;
        cooldownTime = dashCooldown;

        // Dash in the direction the headset is facing (flattened)
        Vector3 forward = head.forward;
        forward.y = 0;
        dashDirection = forward.normalized;
        //vignette.intensity.value = 0f;
    }
}
