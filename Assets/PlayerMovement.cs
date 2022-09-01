using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMovement : MonoBehaviourPunCallbacks
{

    public bool isholdingball;
    public float speed;
    public Rigidbody2D rb;
    Vector2 moveDirection = Vector2.zero;
    public Vector2 startpostion;
    private GameObject ball;
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
       ball = this.gameObject.transform.GetChild(0).gameObject;
     
        rb = this.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (!view.IsMine) return;
        if (isholdingball)
        {
            return;
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
        this.transform.position = startpostion;
    }
}
