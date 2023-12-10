using UnityEngine;

namespace Entity
{
    public class Projectile: MonoBehaviour
    {
        GameObject target; 
        [SerializeField] float duration = 5.0f;
        float timer = 0.0f;
        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= duration)
                Break(null);
            if (target is not null)
                transform.position = Vector2.Lerp(transform.position, target.transform.position, Time.deltaTime * 7f);
        }
    
        public void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Monster") || other.CompareTag("ConfusedMonster") || other.CompareTag("MapObjects") || other.CompareTag("Wall"))
            {
                Break(other);
            }
        }

        public virtual void Break(Collider2D other)
        {
            Destroy(gameObject);
        }

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }
    }
}
