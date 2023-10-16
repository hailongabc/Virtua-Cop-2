using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;
    private Rigidbody rb;
    public BulletTrailScripttableObject TrailConfig;
    protected TrailRenderer Trail;
    [SerializeField]
    private Renderer Renderer;
    public ParticleSystem BulletImpact;
    private bool IsDisabling = false;
    protected const string DO_DISABLE_METHOD_NAME = "DoDisable";
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        Trail = GetComponent<TrailRenderer>();
    }
   protected virtual void OnEnable()
    {
        Renderer.enabled = true;
        IsDisabling = false;
        ConfigureTrail();
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
        else if(other.CompareTag("wall"))
        {
            Destroy(gameObject);
            BulletImpact.Play();
        }
        Disable();


    }

    private void ConfigureTrail()
    {
        if(Trail != null && TrailConfig != null)
        {
            TrailConfig.SetupTrail(Trail);
        }
    }
    protected void Disable()
    {
        CancelInvoke(DO_DISABLE_METHOD_NAME);
        Renderer.enabled = false;
        if(Renderer != null)
        {
            Renderer.enabled = true;
        }
        if (Trail != null && TrailConfig != null)
        {
            IsDisabling = true;
            Invoke(DO_DISABLE_METHOD_NAME, TrailConfig.Time);
        }
        else
        {
            DoDisable();
        }
    }
    protected void DoDisable()
    {
        if(Trail != null && TrailConfig != null)
        {
            Trail.Clear();
        }
        gameObject.SetActive(false);
    }
}
