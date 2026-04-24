using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class OpenMenus : MonoBehaviour
{
    public InputActionProperty MenuButtonAction;
    public GameObject MenuUI;
    public GameObject Camera;
    bool MenuActive = false;
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
                MenuUI.GetComponent<PauseMenu>().Pause();
                MenuUI.transform.position = Camera.transform.forward  + Camera.transform.position ;
                MenuUI.transform.rotation = new Quaternion(0, Camera.transform.rotation.y, 0, Camera.transform.rotation.w);
                MenuActive = true;
            }
            else
            {
                MenuUI.GetComponent<PauseMenu>().Resume();
                MenuUI.SetActive(false);
                MenuActive = false;
            }
        }
        
    }
}
