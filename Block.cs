using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject blockObject;
    public GameObject ballObject;
    public void Spawn()
    {
        int amountBalls = Ball.Instance.amountBalls;
        hp = Random.Range(amountBalls - 2, amountBalls + 2);
        if (hp <= 0)
            hp = 1;
    }
    public void SpawnBall()
    {
        Hide();
        ballObject.SetActive(true);      
    }
    public void Hide()
    {
        Destroy(gameObject);
      //  blockObject.SetActive(false);
    }
    public void ReceiveHit()
    {
        hp--;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public int hp;
}

	
