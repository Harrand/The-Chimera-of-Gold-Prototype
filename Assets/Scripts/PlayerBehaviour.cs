using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{ //Aswin's Amazing Emporium of Random Bits of Code That Makes the whole thing work 05/12/17
    internal class PlayerBehaviour : MonoBehaviour
    {
        public int cell = 2;
        public Vector3 target;
        public void Movement()
        {

            if (Input.GetKeyDown("up")) {
                target.y = Mathf.Round(transform.position.y) + 1;
                target.x = Mathf.Round(transform.position.x);
                if (tileCheck((int)target.x, (int)target.y))
                    transform.position = target;
                else
                    target = transform.position;
            }
            if (Input.GetKeyDown("down")) {
                target.y = Mathf.Round(transform.position.y) - 1;
                target.x = Mathf.Round(transform.position.x);
                if (tileCheck((int)target.x, (int)target.y))
                    transform.position = target;
                else
                    target = transform.position;
            }
            if (Input.GetKeyDown("right")) {
                target.x = Mathf.Round(transform.position.x) + 1;
                target.y = Mathf.Round(transform.position.y);
                if (tileCheck((int)target.x, (int)target.y))
                    transform.position = target;
                else
                    target = transform.position;
            }
            if (Input.GetKeyDown("left")) {
                target.x = Mathf.Round(transform.position.x) - 1;
                target.y = Mathf.Round(transform.position.y);
                if (tileCheck((int)target.x, (int)target.y))
                    transform.position = target;
                else
                    target = transform.position;
            }

            
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