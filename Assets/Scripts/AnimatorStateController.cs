using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        if(!isWalking && forwardPressed)
        {
            animator.SetBool("isWalking", true);
            Debug.Log("pressed");
        }

        if(isWalking && !forwardPressed)
        {
            animator.SetBool("isWalking", false);
            Debug.Log("release");
        }

        if(!isRunning && (forwardPressed && runPressed))
        {
            animator.SetBool("isRunning", true);
            Debug.Log("pressed shift");
        }

        if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool("isRunning", false);
            Debug.Log("release shift");
        }
    }
}
