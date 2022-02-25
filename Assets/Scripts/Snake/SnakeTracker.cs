using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTracker : MonoBehaviour
{
    [SerializeField] private Snake _snake;
    [SerializeField] private float _speed;
    [SerializeField] private float offsetY;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, GetTargetPosition(), _speed * Time.fixedDeltaTime);        
    }

    private Vector3 GetTargetPosition()
    {
        return new Vector3(transform.position.x, _snake.transform.position.y + offsetY, transform.position.z);
    }
}
