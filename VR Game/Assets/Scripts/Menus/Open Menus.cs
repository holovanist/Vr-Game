using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class OpenMenus : MonoBehaviour
{
    public InputActionProperty MenuButtonAction;
    public GameObject MenuUI;
    public GameObject Camera;
    public GameObject VROrigin;
    bool MenuActive = false;
    float distance = 5;
    void Start()
    {
            MenuUI.SetActive(false);
    }
    
    void Update()
    {
        if(MenuButtonAction.action.WasCompletedThisFrame())
        {
            if(!MenuActive)
            {
                MenuUI.SetActive(true);
                // * Time.deltaTime;
                MenuUI.transform.localPosition = Camera.transform.forward + new Vector3(0, 1, 0);
                MenuUI.transform.rotation = new Quaternion(0, Camera.transform.rotation.y, 0, Camera.transform.rotation.w);
                MenuActive = true;
            }
            else
            {
                MenuUI.SetActive(false);
                MenuActive = false;
            }
        }
        
    }
}
