﻿using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidbody;

    public Transform MyTarget { get; set; }

    void Start() 
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() 
    {
        if (MyTarget != null)
        {
            Vector2 direction = MyTarget.position - transform.position;
            rigidbody.velocity = direction.normalized * speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
        }
    }

    private void Update() 
    {
        StartCoroutine(RemoveProjectile());
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("HitBox") && other.transform == MyTarget)
        {
            GetComponent<Animator>().SetTrigger("impact");
            rigidbody.velocity = Vector2.zero;
            MyTarget = null;
        }
    }

    private IEnumerator RemoveProjectile()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
