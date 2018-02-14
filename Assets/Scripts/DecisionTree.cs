using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* this script is written by Yutian and Zibo */

namespace Chimera
{
    public class DecisionTree
    {
		int value;
		public ObstacleManager obstaclescript;
		public DecisionTree()
		{
			
		}
			
		public DecisionTree(ObstacleManager obstaclescript)
		{
			this.obstaclescript = obstaclescript;
		}

		//this method is to find all possibilities of paths after rolling a dice.
		public List<Vector2> BFS_Find_Path(int moves,Vector2 startPosition)
		{
			// four direction: up,right,down,left 
			int[,] dir = new int[4,2]
			{  
				{0, 1}, {1, 0},  
				{0, -1}, {-1, 0}  
			}; 

			var path = new List<Vector2> ();
			var visited = new HashSet<Vector2> (); //store node that has been visited

			var waitList = new Queue<Vector2> ();  //store node that need to be visited
			waitList.Enqueue (startPosition);

			while (waitList.Count > 0 && moves > 0) 
			{
				Vector2 current = waitList.Dequeue ();
				Vector2 neighbour = new Vector2 ();

				Debug.Log("x position = " + current.x + "  y position = " + current.y);  //use to debug

				visited.Add(current);  // add the current to the visited list

				moves--;
				for(int i = 0; i < 4; ++i){
					neighbour.x = current.x+dir[i,0]; //search the neighbour node 
					neighbour.y = current.y+dir[i,1];
				
					if ((isvalid(neighbour)) && !visited.Contains(neighbour))
					{
						if(ObstacleManager.CheckObstacle((int)neighbour.x,(int)neighbour.y))
						{
							if(moves!=0)
							{
								if (!path.Contains (current)) 
								{
									path.Add(current);
								}
								continue;
							}
							else if(moves == 0)
							{
								if (!path.Contains (current)) 
								{
									path.Add(current);
								}
								continue;
							}
							else
							{
								Debug.Log("moves less than 0!!!!");	
							}
						}
						if (moves == 0) 
						{
							if (!path.Contains(neighbour)) 
							{
								path.Add(neighbour);
							}
							continue;
						}
						else
						{
							waitList.Enqueue(neighbour);  //join the neighbour to the waitList to wait for next search
						}

					}  
				}

				Debug.Log("we can't find the goal!!!!!!");




			}
			return path;

		}

		//this method is to decide which path should the AI to go. It compares the distance of goal position and the end position after moving.
		public int BFS_Assese_Value(Vector2 startPosition)
		{
			// four direction: up,right,down,left 
			int[,] dir = new int[4,2]
			{  
				{0, 1}, {1, 0},  
				{0, -1}, {-1, 0}  
			}; 
				
			Vector2 goalposition = new Vector2 (10, 18);

			var visited = new HashSet<Vector2> (); //store node that has been visited

			var waitList = new Queue<Vector2> ();  //store node that need to be visited
			waitList.Enqueue (startPosition);

			while (waitList.Count > 0) 
			{
				Vector2 current = waitList.Dequeue ();
				Vector2 neighbour = new Vector2 ();

				Debug.Log("x position = " + current.x + "  y position = " + current.y);  //use to debug

				visited.Add(current);  // add the current to the visited list

				for(int i = 0; i < 4; ++i){  
					neighbour.x = current.x+dir[i,0]; //search the neighbour node 
					neighbour.y = current.y+dir[i,1];

					if (neighbour == goalposition) 
					{
						Debug.Log ("We found the goal!");
						return (int)Vector2.Distance (goalposition, startPosition); // when we found the goal
					}

					if ((isvalid(neighbour)) && !visited.Contains(neighbour))
					{  
						waitList.Enqueue(neighbour);  //join the neighbour to the waitList to wait for next search
					}  
				}

				Debug.Log("we can't find the goal!!!!!!");




			}
			return -1;

		}

		public bool isvalid(Vector2 position) // to check if the node is a tile.
		{
			int x = (int)position.x;
			int y = (int)position.y;

			if (y == 0)
			{
				return true;
			}
			else if(y == 1)
			{
				if (x == 0 || x == 4 || x == 8|| x == 12 || x == 16 || x == 19)
					return true;
				else
					return false;
			}
			else if(y == 2)
			{
				return true;
			}
			else if (y == 3 || y == 4)
			{
				if (x == 2 || x == 6 || x == 10 || x == 14 || x == 18)
					return true;
				else
					return false;
			}
			else if (y == 5)
			{
				if (x >= 2 && x <= 18)
					return true;
				else
					return false;
			}
			else if (y == 6)
			{
				if (x == 4 || x == 8 || x == 12 || x == 16)
					return true;
				else
					return false;
			}
			else if (y == 7)
			{
				if (x >= 4 && x <= 16)
					return true;
				else
					return false;
			}
			else if (y == 8 || y == 9)
			{
				if (x == 6 || x == 14)
					return true;
				else
					return false;
			}
			else if (y == 10)
			{
				if (x >= 6 && x <= 14)
					return true;
				else
					return false;
			}
			else if (y == 11)
			{
				if (x == 8 || x == 12)
					return true;
				else
					return false;
			}
			else if (y == 12)
			{
				if (x >= 8 && x <= 12)
					return true;
				else
					return false;
			}
			else if(y == 13)
			{
				if (x == 10)
					return true;
				else
					return false;
			}
			else if (y == 14)
			{
				if (x >= 4 && x <= 16)
					return true;
				else
					return false;
			}
			else if (y == 15 || y == 16 || y == 17)
			{
				if (x == 4 || x == 16)
					return true;
				else
					return false;
			}
			else if (y == 18)
			{
				if (x >= 4 && x <= 16)
					return true;
				else
					return false;
			}
			else
				return false;
		}
    }
}
