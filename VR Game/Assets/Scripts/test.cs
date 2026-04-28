using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class test : MonoBehaviour
{
    public XRSocketInteractor Belt;
    public void SnapBack()
    {
        transform.position = Belt.gameObject.transform.position;
    }
    public void TurnToTrigger()
    {
        GetComponent<Collider>().isTrigger = true;
    }
    public void TurnToCollider()
    {
        GetComponent<Collider>().isTrigger = false;
    }    
}
