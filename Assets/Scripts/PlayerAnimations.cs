using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour

{

    private Animator animator; 

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis ("Horizontal") !=0|| Input.GetAxis("Vertical") !=0)
        {
            Debug.Log("didthing");
            animator.SetBool("isWalking", true);
        }
    }
}
