using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHP : MonoBehaviour
{
    public float health = 2;
    //float MaxHealth;
    float time;
    [SerializeField]
    float ImunityTime = 0.25f;
    public bool EnemyDead, boss;
    public string LoadLevel;
    Collider Collider;
    MeshRenderer MR;
    SkinnedMeshRenderer[] SMR;
    Rigidbody rb;

    void Start()
    {
        Collider = gameObject.GetComponent<Collider>();
        MR = gameObject.GetComponent<MeshRenderer>();
        SMR = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (EnemyDead)
        {
            if (MR != null) MR.enabled = false;
            if (Collider != null) Collider.enabled = false;
            if(rb != null) rb.useGravity = false;
        }
        else
        {
            if (MR != null) MR.enabled = true;
            if (Collider != null) Collider.enabled = true;
            if (rb != null) rb.useGravity = true;
        }
        if (SMR != null)
        {
            SkinnedMeshRenderer[] meshRenderers = SMR;
            foreach (SkinnedMeshRenderer thisMeshRenderer in meshRenderers)
            {
                if (EnemyDead)
                {
                    thisMeshRenderer.enabled = false;
                }
                else
                {
                    thisMeshRenderer.enabled = true;
                }
            }
        }
    }
    public void TakeDamage(int damage)
    {
        if (time >= ImunityTime)
        {
            health -= damage;
            if (health <= 0)
            {
                if(boss)
                {
                    SceneManager.LoadScene(LoadLevel);
                }
                time = 0;
                if (Collider != null) Collider.enabled = false;
                if (MR != null) MR.enabled = false;
                if (rb != null) rb.useGravity = false;
                EnemyDead = true;
            }
            time = 0;
        }
    }
}