using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceUIController : MonoBehaviour
{
    public GameObject CharacterObject;

    public Transform CanvasTransform;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - CharacterObject.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = CharacterObject.transform.position + offset;
        CanvasTransform.LookAt(transform.position + Camera.main.transform.forward);
    }
}

