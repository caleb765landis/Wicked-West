using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    public Transform player;
    public GameObject EndGameUI;
    public bool doRestart = true;
    public float fadeDuration = 3f;
    public float displayImageDuration = 3f;
    public float m_Timer = 0f;

    public bool endLevel = false;

    private void Awake()
    {
        player = GameObject.Find("PlayerModels").transform;
    }

    void Update() {
        if (endLevel) {
            m_Timer += Time.deltaTime;

            if (m_Timer > fadeDuration + displayImageDuration)
            {
                if (doRestart)
                {
                    SceneManager.LoadScene (2);
                }
                else
                {
                    Application.Quit ();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
		{
			player.GetComponent<PlayerController>().isDead = true;
			player.GetComponent<PlayerController>().playeranim.SetBool("isWalking", false);
            EndGameUI.SetActive(true);

            endLevel = true;


		}
    }
}
