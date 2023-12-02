using UnityEngine;

public class SelfRotator : MonoBehaviour
{
    [SerializeField, Range(1.0f, 100.0f)] private float _speed;

    private void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * -_speed, Space.Self);
    }
}
