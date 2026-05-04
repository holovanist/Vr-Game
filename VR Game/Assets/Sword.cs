using Unity.Labs.SuperScience;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Vector3 enterPosition;
    Vector3 exitPosition;
    float time;
    public float MaxTime;
    public float Damage;
    public Vector3 temp;
    PhysicsTracker physicsTracker = new();
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        time = 0;
        enterPosition = transform.position;
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Trigger");
            temp = GetTotalMeshFilterBounds(other.transform).size;

        }
        //physicsTracker.
    }
    private void OnTriggerStay(Collider other)
    {
        time += Time.deltaTime;
    }
    private void OnTriggerExit(Collider other)
    {
        CalculateDamage(time);
        exitPosition = transform.position;
        
    }
    public void CalculateDamage(float timer)
    {
        float distance = Vector3.Distance(enterPosition, exitPosition);
        if(timer > MaxTime)
        {
            timer = MaxTime;
        }
        float damageDelt = Damage * (distance);
    }

    private static Bounds GetTotalMeshFilterBounds(Transform objectTransform)
    {
        var meshFilter = objectTransform.GetComponent<MeshFilter>();
        var result = meshFilter != null ? meshFilter.mesh.bounds : new Bounds();

        foreach (Transform transform in objectTransform)
        {
            var bounds = GetTotalMeshFilterBounds(transform);
            result.Encapsulate(bounds.min);
            result.Encapsulate(bounds.max);
        }
        var scaledMin = result.min;
        scaledMin.Scale(objectTransform.localScale);
        result.min = scaledMin;
        var scaledMax = result.max;
        scaledMax.Scale(objectTransform.localScale);
        result.max = scaledMax;
        return result;
    }
}
