using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public Camera normalCam;
    public float SprintModifier;
    public float jumpForce;
    public Transform GroundDetector;
    public LayerMask Ground;
    public LayerMask Wood;

    public Transform weaponParent;
    private Vector3 targetShakePosition;
    private Vector3 weaponParentOrigin;

    float idleShake;
    float MovementShake;

    [SerializeField] private float speed;
    private Rigidbody rb;

    float FOV;
    float SprintFOVModifier = 1.2f;
    void Start()
    {
        FOV = normalCam.fieldOfView;
        //Camera.main.enabled = false;
        rb = GetComponent<Rigidbody>();
        GameManager.ins.PlayerCam = normalCam.gameObject;
        GameManager.ins.Player = gameObject;
        weaponParentOrigin = weaponParent.localPosition;
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        float h_move = Input.GetAxisRaw("Horizontal");
        float v_move = Input.GetAxisRaw("Vertical");
        bool sprint = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);

        bool isGround = Physics.Raycast(GroundDetector.position, Vector3.down, 0.1f, Ground);
        bool isWood = Physics.Raycast(GroundDetector.position, Vector3.down, 0.1f, Wood);
        bool isJumping = jump & isGround || jump & isWood;
        if (isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }

        if(h_move == 0 && v_move == 0)
        {
            Shaking(idleShake, 0.025f, 0.025f);
            idleShake += Time.deltaTime;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetShakePosition, Time.deltaTime *0.5f);
        }
        else if(!sprint)
        {
            Shaking(MovementShake, 0.035f, 0.035f);
            MovementShake += Time.deltaTime * 3f;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetShakePosition, Time.deltaTime * 1.5f);
        }
        else
        {
            Shaking(MovementShake, 0.15f, 0.15f);
            MovementShake += Time.deltaTime * 7f;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetShakePosition, Time.deltaTime * 2f);
        }
    }
    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        float h_move = Input.GetAxisRaw("Horizontal");
        float v_move = Input.GetAxisRaw("Vertical");
        bool sprint = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);

        bool isGround = Physics.Raycast(GroundDetector.position, Vector3.down, 0.1f, Ground);
        bool isJumping = jump & isGround;
        bool isPrinting = sprint & v_move > 0;
        Vector3 dir = new Vector3(h_move, 0f, v_move);
        dir.Normalize();

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

    #region methods
    void Shaking(float p_z, float x_intensity, float y_intensity) 
    {
        targetShakePosition = weaponParentOrigin + new Vector3(Mathf.Cos(p_z) * x_intensity, Mathf.Sin(p_z * 2f) * y_intensity, 0);
    }
    #endregion
}
