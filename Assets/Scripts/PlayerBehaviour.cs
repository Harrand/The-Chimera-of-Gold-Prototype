using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{ //Aswin's Amazing Emporium of Random Bits of Code That Makes the whole thing work 05/12/17
	/*Aswin + Ciara: movement funcitonality 06/12/2017 */
	internal class PlayerBehaviour : MonoBehaviour
	{
		public Vector3 target;
        public Vector3 origin;
        int horizontalMoves = 0;
		int verticalMoves = 0;
		bool upLock = false;
		bool downLock = false;

        public int Movement(int index)
        {
            if (Input.GetKeyDown("up") && !upLock)
            {
                target.y = Mathf.Round(transform.position.y) + 1;
                target.x = Mathf.Round(transform.position.x);
                if (tileCheck((int)target.x, (int)target.y))
                {
                    transform.position = target;
                    verticalMoves++;
                    //Once the player goes up a floor, vertical moves now keep track of total moves made. 
                    //Allows changing direction on the new floor without messing up how many squares you moved on the previous floor
                    verticalMoves += Mathf.Abs(horizontalMoves);
                    horizontalMoves = 0;
                    downLock = true;
                }
                else
                    target = transform.position;

            }
            if (Input.GetKeyDown("down") && !downLock)
            {
                target.y = Mathf.Round(transform.position.y) - 1;
                target.x = Mathf.Round(transform.position.x);
                if (tileCheck((int)target.x, (int)target.y))
                {
                    transform.position = target;
                    verticalMoves++;
                    //Once the player goes down a floor, vertical moves now keep track of total moves made. 
                    //Allows changing direction on the new floor without messing up how many squares you moved on the previous floor
                    verticalMoves += Mathf.Abs(horizontalMoves);
                    horizontalMoves = 0;
                    upLock = true;
                }
                else
                    target = transform.position;


            }
            if (Input.GetKeyDown("right"))
            {
                target.x = Mathf.Round(transform.position.x) + 1;
                target.y = Mathf.Round(transform.position.y);
                if (tileCheck((int)target.x, (int)target.y))
                {
                    transform.position = target;
                    horizontalMoves++;
                }
                else
                    target = transform.position;
            }
            if (Input.GetKeyDown("left"))
            {
                target.x = Mathf.Round(transform.position.x) - 1;
                target.y = Mathf.Round(transform.position.y);
                if (tileCheck((int)target.x, (int)target.y))
                {
                    transform.position = target;
                    horizontalMoves--;
                }
                else
                    target = transform.position;
            }
			return ((Mathf.Abs(horizontalMoves))+(Mathf.Abs(verticalMoves)));
		}

		public void resetMoves()
		{
			horizontalMoves = 0;
			verticalMoves = 0;
			upLock = false;
			downLock = false;
		}

		bool tileCheck(int x, int y)
		{
			if(y == -1 || x == -1 || y == 19 || x == 21)
			{
				return false;
			}
			if (y == 0)
			{
				return true;
			}
			else if (y == 1)
			{
				if (x == 0 || x == 4 || x == 8 || x == 12 || x == 16 || x == 20)
					return true;
				else
					return false;
			}
			else if (y == 2)
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
			else if (y == 13)
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