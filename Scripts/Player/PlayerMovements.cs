using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;


public class PlayerMovements : MonoBehaviourPun
{
    private Rigidbody rb;
    private Animator anim;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float dashPower = 20f; //대쉬 힘
    public float dashDuration = 0.1f; //대쉬 유지
    public float dashDrag = 5f; // 대쉬 후 감속
    private Vector2 movementInput = Vector2.zero;
    private bool canDash = true;

    private Vector3 respawnPosition = new Vector3(10, 3, 4);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // 플레이어가 Y축 기준 -10 이하로 떨어지면 재배치
        if (transform.position.y < -10)
        {
            RespawnPlayer();
        }
    }

    private void FixedUpdate()
    {       
        if (photonView.IsMine || !PhotonNetwork.InRoom)
        {
            Movement();
        }
    }

    private void Movement()
    {
        if (canDash)
        {
            Vector3 movement = new Vector3(movementInput.x, 0.0f, movementInput.y) * moveSpeed;
            movement.y = rb.velocity.y;
            rb.velocity = movement;

            anim.SetFloat("MoveX", movementInput.x);
            anim.SetFloat("MoveY", movementInput.y);

            if (movementInput != Vector2.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(movementInput.x, 0f, movementInput.y));
                rb.rotation = targetRotation;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (photonView.IsMine || !PhotonNetwork.InRoom)
        {
            movementInput = context.ReadValue<Vector2>();            
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && canDash && (photonView.IsMine || !PhotonNetwork.InRoom))
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        canDash = false;
        rb.drag = 0;                

        Vector3 dashDirection = new Vector3(movementInput.x, 0.0f, movementInput.y).normalized;
        rb.AddForce(dashDirection * dashPower, ForceMode.Impulse);


        yield return new WaitForSeconds(dashDuration); // 대쉬 지속 시간 대기

        rb.drag = dashDrag; // 대쉬 후 감속 적용
        yield return new WaitForSeconds(0.35f); // 감속 시간 대기
        rb.drag = 0; // 원래 상태로 복원

        canDash = true;                     
    }

    private void RespawnPlayer()
    {
        transform.position = respawnPosition;
        rb.velocity = Vector3.zero; 
    }
}
