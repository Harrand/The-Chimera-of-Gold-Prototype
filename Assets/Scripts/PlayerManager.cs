using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{

    public class PlayerManager : MonoBehaviour
    {

        public GameObject[] Players;
        public GameObject[] Pawns;
        int index = 0;
        public void SetupPlayers()
        {
            
            Pawns = new GameObject[5];
            for (int x = -1; x < 21; x++)
            {
                
                if(CheckPlayerTile(x))
                {
                    GameObject Pawn = Instantiate(Players[index], new Vector3(x, -1, 0f), Quaternion.identity) as GameObject;
                    Pawn.AddComponent<PlayerBehaviour>();
                    Pawns[index] = Pawn;
                    int a = Pawns.Length;
                    Debug.Log("Length " + a);
                    index++;
                }
            }
        }
        bool CheckPlayerTile(int x)
        {
            if (x == 2 || x == 6 || x == 10 || x == 14 || x == 18)
                return true;
            else
                return false;
        }

        // Update is called once per frame
        public void UpdatePlayers()
        {
            if (index >= 5)
                index = 0;
                   
            if(Input.GetKeyDown("space"))
            {
                index++;
                Debug.Log("Spaace");

            }
            

            GameObject p = Pawns[index];
            Debug.Log(index);
            p.GetComponent<PlayerBehaviour>().Movement();

        }
    }
}