using UnityEngine;

    public class HealthController : MonoBehaviour, IDamageable
    {
        [SerializeField] private int health = 100;
        private IShip _ship;
        private int _currentHealth;
        private void OnEnable()
        {
            _currentHealth = health;
        }
        public void Configure(IShip ship)
        {
            _ship = ship;
        }
        public void TakeDamage(int damage)
        { 
            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            var isDead = _currentHealth <= 0;
            float healtPorcentage = (float)_currentHealth/(float)health;
            _ship.OnDamageReceived(isDead,healtPorcentage);
        }
    }
