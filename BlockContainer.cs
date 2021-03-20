using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class BlockContainer : MonoSingleton<BlockContainer>
{
    private const float DISTANCE_BETWEEN_BLOCKS = 0.38f;
    public GameObject rowPrefab;
    public Transform rowContainer;
    private float currentSpawnY;
    private Vector2 rowContainerStartingPosition;
    private Vector2 desiredPosition;
    private int lastBallSpawn;
    private List<Block> blocks = new List<Block>();
	public List<Transform> Positions=new List<Transform>();
	public bool over=false;
	public Panel  UIManager;

    public BlockContainer Container2;


    public GameObject parentObj;
    public GameObject GameOverPanel;


    private void Start()
    {
        rowContainerStartingPosition = rowContainer.transform.position;
        desiredPosition = rowContainerStartingPosition;
    }

    private void Update()
    {
		if (!over){
			Control ();
			if ((Vector2)rowContainer.position != desiredPosition)
				rowContainer.transform.position = Vector3.MoveTowards(rowContainer.transform.position, desiredPosition, Time.deltaTime);
		}

     

    }

	public void Control()
	{
        List<Transform> silinecekler = new List<Transform>();
        for (int i = 0; i < Positions.Count; i++)
        {
          
                if (Positions[i].transform.childCount<=0)
                {
                silinecekler.Add(Positions[i]);
                }
            
        }
        for (int i = 0; i < silinecekler.Count; i++)
        {
            Positions.Remove(silinecekler[i]);
        }
        for (int i = 0; i < Positions.Count; i++) 
		{
			if (Positions[i].position.y<=-0.8f) 
			{
				UIManager.SetGameOverPanelActive (true);
				over = true; 
				Debug.Log ("Game Over");


                break;

            }
        }
	}

	public void GenerateNewRow()
    {
		Debug.Log ("Row Created");
        GameObject go = Instantiate(rowPrefab) as GameObject;
		Positions.Add (go.transform);
        go.transform.SetParent(rowContainer);
        go.transform.localPosition = Vector2.down * currentSpawnY;
        currentSpawnY -= DISTANCE_BETWEEN_BLOCKS;
        desiredPosition = rowContainerStartingPosition + (Vector2.up * currentSpawnY);
        Block[] blockArray = go.GetComponentsInChildren<Block>();
        int ballSpawnIndex = -1;
        if (lastBallSpawn * 0.3f > 1)
        {
            // Force spawn a ball
            ballSpawnIndex = Random.Range(0, 7);
            lastBallSpawn = 0;
        }
        for (int i = 0; i < blockArray.Length; i++)
        {
            if (ballSpawnIndex == i)
            {
                blockArray[i].SpawnBall();
                return;
            }

            if (Random.Range(0f, 1f) > 0.5f)
                blockArray[i].Spawn();
            else
                blockArray[i].Hide();
        }
        lastBallSpawn++;
    }


   
}
