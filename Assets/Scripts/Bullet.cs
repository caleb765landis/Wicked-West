using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = .1f;
    public float bulletDamage = 25f;

    private Rigidbody rb;

    //private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Destroy this object after 3 seconds
        Destroy(this.gameObject, 3f);
    }

    void FixedUpdate()
    {
        rb.MovePosition (rb.position + transform.forward * bulletSpeed);
    }

    void OnTriggerEnter(Collider other)
	{   
        Destroy(this.gameObject);
	}
}
