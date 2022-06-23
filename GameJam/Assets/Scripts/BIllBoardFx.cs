using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIllBoardFx : MonoBehaviour
{
    private Transform camTransform;
    
    void Start() {
        camTransform = Camera.main.gameObject.transform;
    }

    void Update()
    {
        transform.LookAt(camTransform,Vector3.up);
        
    }
    
}
