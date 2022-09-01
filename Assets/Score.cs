using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public bool isPlayer1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {

            if (!isPlayer1)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().Player1Score();
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().Player2Score();
            }

        }
    }
}
