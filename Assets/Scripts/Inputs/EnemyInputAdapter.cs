
using UnityEngine;
public class EnemyInputAdapter : IInput
{
    private float _directionY;
    private float _timer;
    // ReSharper disable Unity.PerformanceAnalysis
    public Vector2 GetDirection()
    {
        _timer +=1* Time.deltaTime;
        if (_timer >= 2)
        {
            _directionY = Random.Range(-0.3f, 0.3f);
            _timer = 0;
        }
        return new Vector2(1, _directionY).normalized;
    }

    public bool IsFireActionPressed()
    {
        return Random.Range(0f, 1f) < 0.35f;
    }
}