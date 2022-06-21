using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    public Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim.SetBool("isRunning",false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartRunning() {
        playerAnim.SetBool("isRunning",true);
        
    }


    public void StopRunning() {
        playerAnim.SetBool("isRunning",false);
        
    }
    
}
