using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float width;
    private Vector3 _startPosition;
    public static bool Enable;

    private void Start()
    {
        _startPosition = transform.position;
    }
    
    private void Update()
    {
        if (Enable)
            transform.position = _startPosition + direction * ((speed * Time.time) % width);
    }
}