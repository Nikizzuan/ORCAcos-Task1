using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
public class Ball : MonoBehaviour
{

    public InputAction PlayerControl;
    public InputAction.CallbackContext Context;
    Vector3 moveDirection;

    public float lunchForce;
    public float shootPointSpeed;
    public GameObject shootPoint;
    public GameObject playBall;
    [SerializeField] private PhotonView view;

    private void Start()
    {
        if (!view.IsMine) return;
        shootPoint = this.gameObject.transform.GetChild(0).gameObject;
        playBall = GameObject.FindGameObjectWithTag("Ball");
    }
    private void OnEnable()
    {
       // if (!view.IsMine) return;
        PlayerControl.Enable();

    }

    private void OnDisable()
    {
       // if (!view.IsMine) return;
        PlayerControl.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        if (!view.IsMine) return;
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Shoot();
        }

        moveDirection = PlayerControl.ReadValue<Vector2>();

      
    }

    private void FixedUpdate()
    {
        if (!view.IsMine) return;
        Vector2 ballPosition = transform.position;
        Vector2 ArrowPositon = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        //Mouse.current.position.ReadValue()
        Vector2 direction = ArrowPositon * shootPointSpeed - ballPosition;
        shootPoint.transform.right = direction ;
 
    }

    void Shoot() {

        this.GetComponentInParent<PlayerMovement>().isholdingball = false;
        PlayerControl.Disable();
        this.gameObject.SetActive(false);
        playBall.GetComponent<Rigidbody2D>().velocity = shootPoint.transform.right * lunchForce;

    }


  }
