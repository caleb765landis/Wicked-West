using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    public Transform player;
    public GameObject EndGameUI;

    private void Awake()
    {
        player = GameObject.Find("PlayerModels").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
		{
			player.GetComponent<PlayerController>().isDead = true;
			player.GetComponent<PlayerController>().playeranim.SetBool("isWalking", false);
            EndGameUI.SetActive(true);
		}
    }
}
