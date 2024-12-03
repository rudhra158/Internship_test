using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData; 
    public GameObject obstaclePrefab; 
    public float gridSize = 1f;
    


    private void Start()
    {
        GenerateObstacles();
    }

    private void GenerateObstacles()
    {
        if (obstacleData == null || obstaclePrefab == null)
        {
            Debug.LogError("ObstacleData or ObstaclePrefab is not assigned!");
            return;
        }

        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                int index = y * 10 + x;

                if (obstacleData.obstacleGrid[index])
                {
                    Vector3 position = new Vector3(x ,1 , y );
                    Instantiate(obstaclePrefab, position, Quaternion.identity, transform);
                }
            }
        }
    }
}
