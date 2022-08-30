using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public bool IsholdingBall;
    public float speed;
    public Rigidbody2D rb;
    Vector2 moveDirection = Vector2.zero;
    private Vector2 startpostion;
    private Vector2 startpostionchild;
    [SerializeField] private PhotonView view;


    public InputAction PlayerControl;


    private void OnEnable()
    {
        if (!view.IsMine) return;
        PlayerControl.Enable();
    }

    private void OnDisable()
    {
        if (!view.IsMine) return;
        PlayerControl.Disable();
    }

    private void Start()
    {
        if (!view.IsMine) return;
        startpostion = this.gameObject.transform.position;
        startpostionchild = this.gameObject.transform.GetChild(0).transform.position;
        rb = this.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (!view.IsMine) return;
        if (IsholdingBall)
        {

        }
        else
        {

            moveDirection = PlayerControl.ReadValue<Vector2>();

        }



    }

    private void FixedUpdate()
    {
        if (!view.IsMine) return;
        rb.velocity = new Vector2(0 * speed, moveDirection.x * speed);
    }

    public void Resetpaddle()
    {
        if (!view.IsMine) return;
        rb.velocity = Vector2.zero;
        this.gameObject.transform.GetChild(0).transform.position = startpostionchild;
        transform.position = startpostion;
    }
}
