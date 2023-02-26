using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject bulletSpawner;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fire()
    {
        //print(bulletSpawner.transform.position);

        Instantiate(bullet, bulletSpawner.transform.position, bulletSpawner.transform.rotation);

    }
}
