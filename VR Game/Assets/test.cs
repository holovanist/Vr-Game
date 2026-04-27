using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class test : MonoBehaviour
{
    public XRSocketInteractor Belt;
    public void SnapBack()
    {
        transform.position = Belt.gameObject.transform.position;
    }
}
