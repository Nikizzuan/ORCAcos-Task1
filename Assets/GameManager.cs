using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

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
    // instance

    void Awake()
    {
        view = this.GetComponent<PhotonView>();

    }

    void Start()
    {

        if (PhotonNetwork.IsMasterClient == true)
        {
          
            PhotonNetwork.Instantiate(player1.name, player1.transform.position, Quaternion.identity);
            PhotonNetwork.Instantiate(Playball.name, player1.transform.GetChild(0).gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(player2.name, player2.transform.position, Quaternion.identity);
          
        }

        ball = GameObject.FindGameObjectWithTag("Ball");
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player1Text = GameObject.Find("Player1Score");
        player1Goal = GameObject.Find("Player 1 Goal");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
        player2Text = GameObject.Find("Player2Score");
        player2Goal = GameObject.Find("Player 2 Goal");
        if (PhotonNetwork.IsMasterClient == true)
        {
            player1.GetComponent<PlayerMovement>().isholdingball = true;
            player1.transform.GetChild(0).transform.gameObject.SetActive(true);
        }

    }

    public void Player1Score()
    {
        if (view.IsMine)
        {
            player1 = GameObject.FindGameObjectWithTag("Player 1");
            player2 = GameObject.FindGameObjectWithTag("Player 2");
            ball = GameObject.FindGameObjectWithTag("Ball");
            player1score++;
            player1Text.GetComponent<TextMeshProUGUI>().text = player1score.ToString();
            // view.RPC("ResetPostion", RpcTarget.All, player1, player2);
            ResetPostion(player1, player2);
        }
        else { }
      

    }

    public void Player2Score()
    {
        if (view.IsMine)
        {
            player1 = GameObject.FindGameObjectWithTag("Player 1");
            player2 = GameObject.FindGameObjectWithTag("Player 2");
            ball = GameObject.FindGameObjectWithTag("Ball");
            player2score++;
            player2Text.GetComponent<TextMeshProUGUI>().text = player2score.ToString();
            /// view.RPC("ResetPostion", RpcTarget.All, player2, player1);
            ResetPostion(player2, player1);
        }
        else { }

    }

 
    public void ResetPostion(GameObject winner, GameObject loser)
    {
        // ball.GetComponent<Ball>().Resetball();
        if (winner)
        {
            winner.GetComponent<PlayerMovement>().Resetpaddle();
            winner.transform.GetChild(0).gameObject.SetActive(true);
            winner.GetComponent<PlayerMovement>().isholdingball = true;
        }

        if (loser)
        {
            loser.GetComponent<PlayerMovement>().Resetpaddle();
        //    loser.transform.GetChild(0).gameObject.SetActive(true);
          //  loser.GetComponent<PlayerMovement>().isholdingball = true;
        }

        if (ball)
        {
            ball.GetComponent<ResetBall>().Resetball(winner);
        }
       
    }

    void updateplaye2(GameObject player2)
    {
        
    }
}
