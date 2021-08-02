using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;

    public CharacterController controller;
    public Animator anim;
    public Transform pivot;
    public GameObject playerModel;
    public 

    public float rotateSpeed;
    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockBackCounter <= 0)
        {
            float yStore = moveDir.y;
            moveDir = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDir = Vector3.ClampMagnitude(moveDir, 1) * moveSpeed;
            moveDir.y = yStore;

            if (controller.isGrounded)
            {
                moveDir.y = 0f;

                if (Input.GetButtonDown("Jump"))
                {
                    moveDir.y = jumpForce;
                }
            }
        } else
        {
            knockBackCounter -= Time.deltaTime;
        }

        moveDir.y = moveDir.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDir * Time.deltaTime);

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDir.x, 0f, moveDir.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    }

    public void KnockBack(Vector3 dir)
    {
        knockBackCounter = knockBackTime;

        moveDir = dir * knockBackForce;
        moveDir.y = knockBackForce;
    }
}
