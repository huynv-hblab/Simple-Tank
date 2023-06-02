using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;

    private Vector2 startPosition;
    private float distance;
    private Rigidbody2D rb2d;

    public UnityEvent onHit = new UnityEvent();

    private void Awake()
    {
        rb2d= GetComponent<Rigidbody2D>();
    }

    public void Init(BulletData bulletData)
    {
        this.bulletData = bulletData;
        startPosition = transform.position;
        rb2d.velocity = transform.up * bulletData.speed;
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, startPosition);
        if(distance > bulletData.maxDistance)
        {
            DisableBullet();
        }
    }

    private void DisableBullet()
    {
        rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onHit?.Invoke();
        var damagable = collision.GetComponent<Damagable>();
        if(damagable != null)
        {
            Debug.Log("name " + collision.name);
            damagable.Hit(bulletData.damage);
        }
        else
        {
            Debug.Log("cant be damagable");
        }
        DisableBullet();
    }
}
