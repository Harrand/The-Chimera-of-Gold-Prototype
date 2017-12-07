using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

namespace Chimera

{

    public class BoardManager : MonoBehaviour
    {
        // Using Serializable allows us to embed a class with sub properties in the inspector.
        [Serializable]
        public class Count
        {
            public int minimum;             //Minimum value for our Count class.
            public int maximum;             //Maximum value for our Count class.


            //Assignment constructor.
            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }
        public Count wallCount = new Count(1, 20);
        public int columns = 20;                                         //Number of columns in our game board.
        public int rows = 19;                                          //Lower and upper limit for our random number of walls per level.

        public GameObject[] Tiles;                                 //Array of members
		public GameObject[] Camps;								   //Array of camp prefabs
        public GameObject Border;                                 //member
		public bool[] checkCampNumber = new bool[5];			 //to see how many players in this game

        private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
        private List<Vector3> gridPositions = new List<Vector3>();   //A list of possible locations to place tiles.


        //Clears our list gridPositions and prepares it to generate a new board.
        void InitialiseList()
        {
            //Clear our list gridPositions.
            gridPositions.Clear();

            //Loop through x axis (columns).
            for (int x = 1; x < columns - 1; x++)
            {
                //Within each column, loop through y axis (rows).
                for (int y = 1; y < rows - 1; y++)
                {
                    //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }

		public void checkPlayerNumbers(int x)
		{
			if (x > 5) 
			{
				for (int i = 0; i < x; i++) 
				{
					checkCampNumber [i] = true;
				}
			}

			for (int i = 0; i < x; i++) 
			{
				checkCampNumber [i] = true;
			}
		}

        //Checks where the tile goes when buiilding the board
        bool tileCheck(int x, int y)
        {
            if (y == 0)
            {
                return true;
            }
            else if(y == 1)
            {
                if (x == 0 || x == 4 || x == 8|| x == 12 || x == 16 || x == columns-1)
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
        //Sets up the outer walls and floor (background) of the game board.
        void BoardSetup()
        {
            //Instantiate Board and set boardHolder to its transform.
            boardHolder = new GameObject("Board").transform;

            //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
            for (int x = -1; x <= columns; x++)
            {
                //Loop along y axis, starting from -1 to place floor or outerwall tiles.
                for (int y = -1; y <= rows +2; y++)
                {
                    //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                    GameObject toInstantiate = Tiles[1];

                    //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                    if (x == -1 || x == columns || y == -1 || y == 19)
					if (x == 2 && y == -1 && checkCampNumber [0])             //set up the camps into the first place.
							toInstantiate = Camps[0];
					else if (x == 6 && y == -1 && checkCampNumber[1])
							toInstantiate = Camps[1];
					else if (x == 10 && y == -1 && checkCampNumber[2])
							toInstantiate = Camps[2];
					else if (x == 14 && y == -1 && checkCampNumber[3])
							toInstantiate = Camps[3];
					else if (x == 18 && y == -1 && checkCampNumber[4])
							toInstantiate = Camps[4];
						else 	
                        	toInstantiate = Border;
                    else if (tileCheck(x,y))            //Check if a tile should be in the current position
                        toInstantiate = Tiles[0];
                    //Places the goal at the top
                    if (y == 18 && x == 10)
                        toInstantiate = Tiles[2];

                    //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                    instance.transform.SetParent(boardHolder);
                }
            }
        }


        //SetupScene initializes our level and calls the previous functions to lay out the game board
        public void SetupScene()
        {
            //Creates the outer walls and floor.
            BoardSetup();
            //Reset our list of gridpositions.
            InitialiseList();
        }
			
    }
}