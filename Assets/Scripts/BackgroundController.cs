using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundcontroller : MonoBehaviour
{
    [SerializeField] private LayerSpeed[] layers;
    [SerializeField] private float startPositionX;
    [SerializeField] private float endPositionX;

    void Update()
    {
        foreach (var layer in layers)
        {
            layer.parallaxLayer.Translate(Vector2.left * layer.speed * Time.deltaTime);
            if (layer.parallaxLayer.position.x < endPositionX)
            {
                Vector2 newPosition = new Vector2(startPositionX, layer.parallaxLayer.position.y);
                layer.parallaxLayer.position = newPosition;
            }
        }
    }
}
[Serializable]
public struct LayerSpeed
{
    public Transform parallaxLayer;
    public float speed;
}