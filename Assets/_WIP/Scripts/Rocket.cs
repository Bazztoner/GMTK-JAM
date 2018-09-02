using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	public float speed, destroyTimer = 3;

	private void Start()
	{
		Destroy(gameObject, destroyTimer);
	}

	void Update ()
	{
		transform.position += transform.right * speed * Time.deltaTime;
	}

	private void OnCollisionEnter(Collision c) { Destroy(gameObject); }
	private void OnTriggerEnter(Collider c) { Destroy(gameObject); }
	private void OnCollisionEnter2D(Collision2D c) { Destroy(gameObject); }
	private void OnTriggerEnter2D(Collider2D c) { Destroy(gameObject); }
}
