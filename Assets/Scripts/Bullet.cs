using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("head"))
        {
            other.GetComponentInParent<TestHuman1>().getShotInHead(damage);
            Destroy(gameObject);
        }
        else if(other.CompareTag("body"))
        {
            other.GetComponentInParent<TestHuman1>().getShotInBody(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
