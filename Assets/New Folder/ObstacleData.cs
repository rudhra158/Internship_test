using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/ObstacleData", order = 1)]
public class ObstacleData : ScriptableObject
{
    
    public bool[] obstacleGrid = new bool[100];

    public void ResetGrid()
    {
        for (int i = 0; i < obstacleGrid.Length; i++)
        {
            obstacleGrid[i] = false;
        }
    }
}
