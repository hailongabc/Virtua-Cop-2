using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera normalCam;
    public float SprintModifier;
    public float jumpForce; 
    [SerializeField] private float speed;
    private Rigidbody rb;

    float FOV;
    float SprintFOVModifier = 1.5f;
    void Start()
    {
        FOV = normalCam.fieldOfView;
        Camera.main.enabled = false;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h_move = Input.GetAxisRaw("Horizontal");
        float v_move = Input.GetAxisRaw("Vertical");
        bool sprint = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);

        bool isJumping = jump;
        bool isPrinting = sprint & v_move > 0;

        Vector3 dir = new Vector3(h_move, 0f, v_move);
        dir.Normalize();

        if (isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }

        float adjustedSpeed = speed;
        if (isPrinting)
        {
            adjustedSpeed *= SprintModifier;
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, FOV * SprintFOVModifier, Time.deltaTime * 8f);
        }
        else
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, FOV, Time.deltaTime * 8f);
        }

        Vector3 TargetVelocity = transform.TransformDirection(dir) * adjustedSpeed * Time.deltaTime;
        TargetVelocity.y = rb.velocity.y;

        rb.velocity = TargetVelocity;
    }
}
