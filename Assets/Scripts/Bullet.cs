using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = .001f;

    private Rigidbody rb;

    //private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //rb.MoveRotation (Quaternion.LookRotation(new Vector3(0f, 90f, 0f)));

        /*
        RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit))
		{
			transform.LookAt(new Vector3(hit.point.x, hit.point.y, hit.point.z));
		}
        */
    }

    void FixedUpdate()
    {
        rb.MovePosition (rb.position + transform.forward * bulletSpeed);
    }

    void OnTriggerEnter(Collider other)
	{
        Destroy(this.gameObject);
        /*
		if (other.gameObject.tag == "Ground")
		{
			isOnGround = true;
		}
        */
	}
}
