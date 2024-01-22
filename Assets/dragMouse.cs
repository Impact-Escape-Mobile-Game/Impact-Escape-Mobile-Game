using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragMouse : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float movementRange = 50.0f;

    private float initialPosition;

    void Start()
    {
        initialPosition = transform.position.x;
    }

    void Update()
    {
        float newPosition = initialPosition + Mathf.Sin(Time.time * moveSpeed) * movementRange;
        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
    }
}
