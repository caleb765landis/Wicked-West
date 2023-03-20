using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
	public int maxAmmo = 12;
    public int ammo;

    public GameObject bulletSpawner;
    public GameObject bullet;

	public TextMeshProUGUI ammoText;

    public AudioSource audioSource;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;

        SetAmmoText();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fire()
    {
        if (ammo > 0)
        {
            Instantiate(bullet, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
            ammo--;
	        SetAmmoText();
            audioSource.Play();
        }

    }

    public void reload()
    {
        ammo = maxAmmo;
        SetAmmoText();
    }

    public void SetAmmoText()
	{
		ammoText.text = ammo.ToString() + "/" + maxAmmo.ToString();
	}
}
