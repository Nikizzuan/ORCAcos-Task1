using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{

    [Header("Players")]
    public GameObject player1;
    public GameObject player1Text;
    public GameObject player1Goal;
    public GameObject player2;
    public GameObject player2Text;
    public GameObject player2Goal;
    public GameObject Playball;
    private PhotonView view;
    private int player1score;
    private int player2score;
    public GameObject ball;
    public Camera mainCamera;
    // instance

    Vector2 player1Location;
    Vector2 player2Location;
    Vector2 player1BallLocation;
    Vector2 player2BallLocation;

    //extra
    public GameObject winpannel;
    public TextMeshProUGUI WinMsg;



    void Awake()
    {
        view = this.GetComponent<PhotonView>();

    }

    void Start()
    {

        if (PhotonNetwork.IsMasterClient == true)
        {

            player1 = PhotonNetwork.Instantiate(player1.name, player1.transform.position, Quaternion.Euler(0, 0, 90));
         
        }
        else
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            mainCamera.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            player2 = PhotonNetwork.Instantiate(player2.name, player2.transform.position, Quaternion.Euler(0,0,270));
          
        }


     //   player1.transform.gameObject.tag = "Player 1";
      //  player2.transform.gameObject.tag = "Player 2";
        player1Location = player1.transform.position;
        player2Location = player2.transform.position;
        player1BallLocation = player1.transform.GetChild(0).gameObject.transform.position;
        player2BallLocation = player2.transform.GetChild(0).gameObject.transform.position;
        if (PhotonNetwork.IsMasterClient == true)
        {
         
            view.RPC("ResetPostion", RpcTarget.All, "Player 1", "Ball");
        }

    }

    public void Player1Score()
    {

        if (!PhotonNetwork.IsMasterClient) return;

        player1Text = GameObject.Find("Player1Score");
        player2Text = GameObject.Find("Player2Score");
        player1score++;

      
        view.RPC("UpdataGuiScore", RpcTarget.All, player1score, player2score);
        view.RPC("ResetPostion", RpcTarget.All, "Player 1", "Ball");
       

    }

    public void Player2Score()
    {


        if (!PhotonNetwork.IsMasterClient) return;

        player1Text = GameObject.Find("Player1Score");
        player2Text = GameObject.Find("Player2Score");
        player2score++;

      
            view.RPC("UpdataGuiScore", RpcTarget.All, player1score, player2score);
            view.RPC("ResetPostion", RpcTarget.All, "Player 2", "Ball");
        
       

    }
    [PunRPC]
    void UpdataGuiScore(int player1score, int player2score)
    {
        if (player1score >= 10)
        {
            winpannel.SetActive(true);
            WinMsg.text = "Player 1 Win";
        }
        else if (player2score >= 10)
        {
            winpannel.SetActive(true);
            WinMsg.text = "Player 2 Win";
        }
        else
        {
            player1Text = GameObject.Find("Player1Score");
            player2Text = GameObject.Find("Player2Score");
            player1Text.GetComponent<TextMeshProUGUI>().text = player1score.ToString();
            player2Text.GetComponent<TextMeshProUGUI>().text = player2score.ToString();
        }
    }


    [PunRPC]
    public void ResetPostion(string winner, string balls)
    {
 

        ball = GameObject.FindGameObjectWithTag(balls);
        GameObject GameWinner = GameObject.FindGameObjectWithTag(winner);
      
        if (GameWinner)
        {

            GameWinner.transform.GetChild(0).gameObject.SetActive(true);
            GameWinner.GetComponent<PlayerMovement>().isholdingball = true;
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (GameWinner.gameObject.CompareTag("Player 1")) { 

            GameWinner.transform.position = player1Location;
                ball.transform.position = GameWinner.transform.GetChild(0).gameObject.transform.position;
            }
            else {
            GameWinner.transform.position = player2Location;
                ball.transform.position = GameWinner.transform.GetChild(0).gameObject.transform.position;
            }
                
        }


    }

    public void LeaveRoom() {

        PhotonNetwork.Disconnect();
        
      }



    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel("Lobby Scene");
    }


}
