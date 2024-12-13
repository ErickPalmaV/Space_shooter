using System;
using UnityEngine;

public class MovementController:MonoBehaviour
{
    [SerializeField] private float speed;
    private IShip _ship;
    private Transform _transform;
    
    private void Start()
    {
        _transform = transform;
    }

    public void Configure(IShip ship)
    {
        _ship = ship;
    }
    public void Move(Vector2 direction)
    {
        _transform.Translate(direction * (speed * Time.deltaTime));
    }
}