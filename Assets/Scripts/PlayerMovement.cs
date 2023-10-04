using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;
    void Start()
    {
        Camera.main.enabled = false;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h_move = Input.GetAxisRaw("Horizontal");
        float v_move = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h_move, 0f, v_move);
        dir.Normalize();

        rb.velocity = transform.TransformDirection(dir) * speed * Time.deltaTime;
    }
}
