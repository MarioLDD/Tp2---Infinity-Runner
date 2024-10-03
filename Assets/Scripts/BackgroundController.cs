using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundcontroller : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float startPositionX;
    [SerializeField] private float endPositionX;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < endPositionX)
        {
            Vector2 newPosition = new Vector2(startPositionX, transform.position.y);
            transform.position = newPosition;
        }
    }
}