using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform[] Positions;
    [SerializeField] Transform[] EnemyLookAt;
    [SerializeField] float ObjectSpeed;

    int NextPosIndex;
    Transform NextPos;
    public float Speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        NextPos = Positions[0];
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Positions[1].position, ObjectSpeed * Time.deltaTime);
    }

    void MoveGameObject()
    {
        if (transform.position == NextPos.position)
        {
            NextPosIndex++;
            if (NextPosIndex >= Positions.Length)
            {
                return;
            }
            NextPos = Positions[NextPosIndex];
            Debug.Log("vao if");
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, NextPos.position, ObjectSpeed * Time.deltaTime);
            RotatePlayer();
        }
    }
    private void FixedUpdate()
    {
        MoveGameObject();
    }

    void RotatePlayer()
    {
        Quaternion lookRotation = Quaternion.LookRotation(NextPos.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
        Debug.Log("lerp" + transform.rotation);
    }
}
