using UnityEngine;

namespace player
{
    public class DestroyBullet : MonoBehaviour
    {
        public float timeToDestroy = 5f;
        public float Timer;
        public bool Active;
        public int damage = 2;
        public bool onPlayer, onEnemy;
        void Update()
        {
            if (Active)
            {
                Timer += Time.deltaTime;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<SphereCollider>().enabled = true;
            }
            if (Timer <= timeToDestroy && gameObject.GetComponent<MeshRenderer>().enabled == true) Active = true;
            //add a if statment to activate the compenents if active
            if (Timer >= timeToDestroy)
            {
                //Destroy(gameObject);
                gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                Active = false;

                if(onEnemy)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<SphereCollider>().enabled = false;
                }
                //gameObject.SetActive(false);
                Timer = 0;
            }
        }
        private void OnTriggerEnter(Collider coll)
        {

            if(coll.CompareTag("Enemy") && onPlayer)
            {
                coll.gameObject.GetComponent<EnemyHP>().TakeDamage(damage);
            }
        }
    }
}
