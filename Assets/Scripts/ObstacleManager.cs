using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera{
	public class ObstacleManager : MonoBehaviour {

		public PlayerManager manager;
	    public GameObject[] Obstacles;
		public GameObject[] Obstacles_array;
		public int obstacle_Index;
		bool move = false;
		private bool obstacle_moving = false;

		public void setupManager(PlayerManager player){
			manager = player;
		}

	    // Use this for initialization
	    public void SetupObstacles()
	    {
			obstacle_Index = 0;
			Obstacles_array = new GameObject[13];

	        for (int x = -1; x < 21; x++)
	        {
	            for(int y = -1; y < 20; y++)
	            {
	                if (CheckObstacleTile(x, y))
	                {
	                    GameObject Obstacle = Instantiate(Obstacles[0], new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
						Obstacle.AddComponent<ObstacleBehaviour>();
						Obstacles_array [obstacle_Index] = Obstacle;
						obstacle_Index++;
	                }
	            }

	        }
	    }

	    public static bool CheckObstacleTile(int x, int y)
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

        public bool CheckObstacle(int x, int y)
        {
            foreach(GameObject obstacle in Obstacles_array)
            {
                if (obstacle.transform.position.x == x && obstacle.transform.position.y == y)
                    return true;
            }
            return false;
        }

		public void UpdateObstacle()
		{
			//Debug.Log (move);
			if (move)
			{ 
				Debug.Log (obstacle_Index);
				Obstacles_array[obstacle_Index].GetComponent<ObstacleBehaviour>().Movement();
				//Debug.Log("Moving obstacle");
				if (Input.GetKeyDown(KeyCode.O))
				{
					move = false;
				}

			}
			else
			{
				//Debug.Log("Choose pawn, press space when ready to move");
				if (Input.GetKeyDown(KeyCode.O) && manager.OnObstacles[manager.index])
				{
					obstacle_moving = true;
					Debug.Log ("obstacle is moving");
					move = true;
					Vector3 pawnPosition = manager.getCurrentObject().transform.position;
					for (int i = 0; i < 13; i++) 
					{
						if (Obstacles_array [i].transform.position == pawnPosition) 
						{
							obstacle_Index = i;
						}
					}
					//Debug.Log (obstacle_Index);
				}
					
			}
		}

		public bool getIfobstaclemove()
		{
			return obstacle_moving;
		}
	}
}
