using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public static event Action OnAvoidObstacleEvent;
    public static event Action OnGameOverEvent;

    [SerializeField] private float speed;
    [SerializeField] private float deactivationPoint;

    private PooledObject pooledObject;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < deactivationPoint)
        {
            if (pooledObject == null)
            {
                pooledObject = gameObject.GetComponent<PooledObject>();
            }
            pooledObject.Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnAvoidObstacleEvent?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.enabled = true;
            animator.SetTrigger("OnGameover");

            OnGameOverEvent?.Invoke();
        }
    }
}