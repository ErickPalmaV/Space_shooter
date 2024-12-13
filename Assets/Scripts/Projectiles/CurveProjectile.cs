using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Unity.Mathematics;

public class CurveProjectile : MonoBehaviour,IDamageable
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float lifeTime = 3f;
    [SerializeField] AnimationCurve curve;
    private float _currentTime;
    private Transform _transform;
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _currentTime = 0;
        StartCoroutine(DestroyAfterSeconds());
    }

    void Update()
    {
        Movement();
    }
    
    public void Movement()
    {
        var horinzontalMovement= bulletSpeed * Time.deltaTime;
        var verticalMovement = curve.Evaluate(_currentTime)*bulletSpeed * Time.deltaTime;
        _transform.Translate(new Vector2(horinzontalMovement,verticalMovement));
        _currentTime += Time.deltaTime;
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
        if (other.CompareTag("Player") || other.CompareTag("Bullet"))
        {
            var damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(1);
        }
    }
}
