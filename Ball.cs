using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
public class Ball : MonoSingleton<Ball>
{
    public Text scoreText,scoretext,hiscoretext, amountBallsText;
    private const float DEADZONE = 80.0f;
    private const float MAXIMUM_PULL = 50.0f;   
    private bool isBreakingStuff;
    private bool firstBallLanded;
    private Vector2 landingPositions; 
    private Rigidbody2D rigid;
    public int amountBalls = 1;
    private int amountBallsLeft;
    private float speed = 4.0f;
    public Transform ballsPreview;
    public GameObject tutorialContainer;
    public Transform rowContainer;
    public GameObject rowPrefab;
    private float currentSpawnY;
    private float score;
    public float hiscore;
    private int ponitstoadd;
	public BlockContainer Container;

	public MobileInput Input;
    private void Start()
    {
        if (PlayerPrefs.HasKey("HighScore")) {
            hiscore = PlayerPrefs.GetFloat("HighScore");
        }
        rigid = GetComponent<Rigidbody2D>();
        ballsPreview.parent.gameObject.SetActive(false);
        amountBallsLeft = amountBalls;
        UpdateText();
		Container.GenerateNewRow();
    }
    private void Update()
    {
        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("HighScore", hiscore);
        }
        scoretext.text = " " + Mathf.Round(score);
        hiscoretext.text = " " + Mathf.Round(hiscore);
        if (!isBreakingStuff)
            PoolInput();      
    }
    public void addscore(int pointstoadd)
    {
        score += pointstoadd;
    }
    // Drag the ball around , visualize the trajectory
    private void PoolInput()
    {
		Vector3 sd = Input.swipeDelta;
        sd.Set(-sd.x, -sd.y, sd.z);
        if (sd != Vector3.zero)
        {
          
            if (sd.y < 0)
            {
                ballsPreview.parent.gameObject.SetActive(false);
            }
            else
            {
                ballsPreview.parent.up = sd.normalized;
                ballsPreview.parent.gameObject.SetActive(true);
                ballsPreview.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(1, 3, 1), sd.magnitude / MAXIMUM_PULL);
                if (Input.release)
                {
                    tutorialContainer.SetActive(false);
                    isBreakingStuff = true;
                    SendBallInDirection(sd.normalized);
                    ballsPreview.parent.gameObject.SetActive(false);
                    amountBallsText.gameObject.SetActive(false);
                    rigid.simulated = true;
                }
            }
        }
    }
    private void SendBallInDirection(Vector3 dir)
    {
        rigid.velocity = dir * speed;
    }
    private void TouchFloor()
    {
        amountBallsLeft--;
        if (!firstBallLanded)
        {
            firstBallLanded = true;
            rigid.velocity = Vector2.zero;
            rigid.simulated = false;
        }
        if (amountBallsLeft == 0)
            AllBallLanded();
    }
    private void AllBallLanded()
    {
        isBreakingStuff = false;
        firstBallLanded = false;
        amountBallsLeft = 1;
		Container.GenerateNewRow();
        score++;
        ponitstoadd++;
        UpdateText();
        amountBallsText.gameObject.SetActive(true);
    }
    public void UpdateText()
    {
        scoreText.text = score.ToString();
        amountBallsText.text = 'x'+amountBalls.ToString();
        amountBallsText.rectTransform.position = Camera.main.WorldToScreenPoint(transform.position)+new Vector3(25,10,0);
    }
    public void CollectBall()
    {
        amountBalls++;
        Debug.Log(amountBalls); 
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Floor")
        {
            TouchFloor();
        }
        else if (coll.gameObject.tag == "Block")
        {
            coll.transform.parent.SendMessage("ReceiveHit");
        }     
    }


}
