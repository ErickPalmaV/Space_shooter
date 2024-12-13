using System;
using System.Collections;
using UnityEngine;

public class LinealProjectile : MonoBehaviour, IDamageable
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float lifeTime = 3f;
    
    void Update()
    {
        transform.Translate(Vector2.right * (bulletSpeed * Time.deltaTime));
    }

    private IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyProjectile();
    }

    private void OnEnable()
    { 
        StartCoroutine(DestroyAfterSeconds());
    }
    
    private void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        DestroyProjectile();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
            {
                var damageable = other.GetComponent<IDamageable>();
                damageable?.TakeDamage(1);
            }
    }
}