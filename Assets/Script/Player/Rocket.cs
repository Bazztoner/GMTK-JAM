using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float speed, destroyTimer = 3;

    private void Awake() { _rigidbody = GetComponent<Rigidbody2D>(); }
    private void Start() { Destroy(gameObject, destroyTimer); }

    void FixedUpdate() { _rigidbody.MovePosition(transform.position + transform.right * speed * Time.deltaTime); }
    private void OnTriggerEnter2D(Collider2D c) { Destroy(gameObject); }
}