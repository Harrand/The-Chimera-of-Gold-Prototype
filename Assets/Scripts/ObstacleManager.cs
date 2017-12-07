using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

    public GameObject[] Obstacles;

    // Use this for initialization
    public void SetupObstacles()
    {
        for (int x = -1; x < 21; x++)
        {
            for(int y = -1; y < 20; y++)
            {
                if (CheckObstacleTile(x, y))
                {
                    GameObject Obstacle = Instantiate(Obstacles[0], new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    //Obstacle.AddComponent<ObstacleBehaviour>();

                }
            }

        }
    }

    bool CheckObstacleTile(int x, int y)
    {
        if (y == 2)
        {
            if (x == 0 || x == 4 || x == 8 || x == 12 || x == 16 || x == 20)
                return true;
            else
                return false;
        }else if (y == 7 || y == 10)
        {
            if (x == 8 || x == 12)
                return true;
            else
                return false;
        }
        else if (y == 12 || y == 13 || y == 18)
        {
            if (x == 10)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }  
    }

    // Update is called once per frame
    void Update () {
		
	}
}
