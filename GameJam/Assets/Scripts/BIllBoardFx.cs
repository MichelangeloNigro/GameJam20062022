using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIllBoardFx : MonoBehaviour
{
    private Transform camTransform;

    Quaternion originalRotation;

    void Start() {
        camTransform=FindObjectOfType<Camera>().transform;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = camTransform.rotation * originalRotation;   
    }
    
}
