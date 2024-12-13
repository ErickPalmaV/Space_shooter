using UnityEngine;

public class UnityInputAdapter : IInput
{
    public Vector2 GetDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        return new Vector2(horizontal, vertical).normalized;
    }
    public bool IsFireActionPressed()
    {
        return Input.GetKey(KeyCode.Space);
    }
}