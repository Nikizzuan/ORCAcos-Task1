using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    [Header("Players")]
    public GameObject player1;
    public GameObject player2;

    // instance
    public static GameManager instance;
    void Awake()
    {
        // instance
        instance = this;
    }

    void Start()
    {

        if (PhotonNetwork.IsMasterClient == true)
        {
            PhotonNetwork.Instantiate(player1.name, player1.transform.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(player2.name, player2.transform.position, Quaternion.identity);
        }


    }
}
