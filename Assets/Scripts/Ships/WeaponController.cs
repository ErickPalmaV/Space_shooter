
    using System.Runtime.CompilerServices;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Serialization;

    public class WeaponController:MonoBehaviour
    {
        private IShip _ship;
        [SerializeField] private float shootRatio;
        private ObjectPool bulletPool;
        [SerializeField] private GameObject bulletsSpawnPoint;
        [SerializeField] private AudioSource bulletSound;
        private float _timer;
        public void Configure(IShip ship, ObjectPool bulletPool)
        {
            _ship = ship;
            this.bulletPool = bulletPool;
        }
        
        public void Shoot()
        {
            _timer +=1* Time.deltaTime;
            if (_timer >= shootRatio)
            { 
                bulletSound.Play();
               var bullet= bulletPool.RequestGameObject();
               bullet.transform.position= bulletsSpawnPoint.transform.position;
               bullet.transform.rotation = bulletsSpawnPoint.transform.rotation;
                _timer = 0;
            }
        }
    }