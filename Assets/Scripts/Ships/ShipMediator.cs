using System.Collections;
using UnityEngine;
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(WeaponController))]
public class ShipMediator: MonoBehaviour, IShip
{
    [SerializeField] private MovementController movementController;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private HealthController healthController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool isEnemy;
    GameController _gameController;
    public void Configure(GameController gameController)
    {
        _gameController = gameController;
    }
    private IInput _input;
    private void Start()
    {
        ObjectPool pool;
        if (isEnemy)
        {
            pool = GameObject.Find("BulletEnemyPool").GetComponent<ObjectPool>();
            _input = new EnemyInputAdapter();
        }
        else
        {
            pool = GameObject.Find("BulletPlayerPool").GetComponent<ObjectPool>();
            _input = new UnityInputAdapter();
        }
        movementController.Configure(this);
        weaponController.Configure(this,pool);
        healthController.Configure(this);
    }
    // Update is called once per frame
    void Update()
    { 
        movementController.Move(_input.GetDirection());
        if (_input.IsFireActionPressed())
        {
            weaponController.Shoot();
        }
    }
    private void OnEnable()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        if (isEnemy)
            StartCoroutine(DestroyAfterSeconds());
    }
    private IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(30);
        DestroyShip();
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void DestroyShip()
    {
        gameObject.SetActive(false);
        if (isEnemy)
        {
            _gameController.EnemyDead();
        }
        else
        {
            _gameController.GameOver();
        }
    }

    public void TakeDamage(int damage)
    {
        DestroyShip();
    }

    public void OnDamageReceived(bool isDeath, float health)
    {
        if (!isEnemy)
        {
            _gameController.setHealth(health);
        }
        StartCoroutine(ShowDamage());
        if (isDeath)
        {
            DestroyShip();
        }
    }

    IEnumerator ShowDamage()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 1);
            yield return new WaitForSeconds(0.4f);
            spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.4f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnemy)
        {
            if (other.CompareTag("Bullet") || other.CompareTag("Player"))
            {
                var damageable = other.GetComponent<IDamageable>();
                damageable?.TakeDamage(1);
            }
        }
        else
        {
            if (other.CompareTag("EnemyBullet") || other.CompareTag("Enemy"))
            {
                var damageable = other.GetComponent<IDamageable>();
                damageable?.TakeDamage(1);
            }
        }
    }
    
}
