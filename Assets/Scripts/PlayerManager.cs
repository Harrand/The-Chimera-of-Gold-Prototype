using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    /*Aswin's Other Amazing work that holds the whole thing together
     * I will now write a haiku to express how i feel
     *This controls players
     *Setup, spawn, turn control too
     *Will do for now, bye
     */
    public class PlayerManager : MonoBehaviour
    {

        public GameObject[] Players;
        public GameObject[] Pawns;
        int index = 0;
        int pawnsIndex = 0;
        int pawnCount = 0;
        int playerTurn = 0;
        public void SetupPlayers()
        {
            
            Pawns = new GameObject[25];
            for (int x = -1; x < 21; x++)
            {

                if (CheckPlayerTile(x))
                {
                    while (pawnCount < 5)
                    {
                        GameObject Pawn = Instantiate(Players[index], new Vector3(x, -1, 0f), Quaternion.identity) as GameObject;
                        Pawn.AddComponent<PlayerBehaviour>();
                        Pawns[pawnsIndex] = Pawn;

                        pawnsIndex++;
                        pawnCount++;
                    }
                    pawnCount = 0;
                    index++;
                    if (index >= 5)
                        index = 0;
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
            
            
            if(Input.GetKeyDown("space"))
            {
                playerTurn++;
                if(playerTurn >= 5)
                {
                    playerTurn = 0;
                }
                index = playerTurn * 5;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
                index = playerTurn * 5 + 0;
            else if(Input.GetKeyDown(KeyCode.Alpha2))
                index = playerTurn * 5 + 1;
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                index = playerTurn * 5 + 2;
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                index = playerTurn * 5 + 3;
            else if (Input.GetKeyDown(KeyCode.Alpha5))
                index = playerTurn * 5 + 4;
            
            Pawns[index].GetComponent<PlayerBehaviour>().Movement();
        }
    }
}