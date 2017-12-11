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
        public GameObject dice;
        int index = 0;
        int turn = 0;
        int pawnsIndex = 0;
        int pawnCount = 0;
        int playerTurn = 0;
        int moves = 0;
        int moved = 0;
        bool move = false;

        private void setupDice()
        {
            dice = Instantiate(dice, new Vector3(-10, -1, -1), Quaternion.identity) as GameObject;
            dice.AddComponent<Dice>();
            dice.GetComponent<Dice>().Start();
            
        }
        public void SetupPlayers()
        {
            setupDice();
            
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

        int choosePawn(int i)
        {
            return playerTurn * 5 + i;
        }
        
        public void nextPlayer()
        {
            playerTurn++;
            turn = 0;

            if (playerTurn >= 5)
                playerTurn = 0;
        }

		public GameObject getCurrentObject()
		{
			return Pawns [index];
		}
      
        // Update is called once per frame
        public void UpdatePlayers()
        {
           
            /*
            if(Input.GetKeyDown("space"))
            {
                playerTurn++;
                turn = 0;

                if(playerTurn >= 5)
                    playerTurn = 0;
            }
            */

            /*Aswin + Ciara: Limited movement based on dice roll 06/12/2017*/
            if (move)
            { 
                index = choosePawn(turn);
                moved = Pawns[index].GetComponent<PlayerBehaviour>().Movement();
                Debug.Log("Moving pawn");
                if (moved >= moves)
                {
                    move = false;
					Pawns[index].GetComponent<PlayerBehaviour>().resetMoves();
                    moved = 0;
                    nextPlayer();
                }
                
            }
            else
            {
                Debug.Log("Choose pawn, press space when ready to move");
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    turn = 0;
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                    turn = 1;
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                    turn = 2;
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                    turn = 3;
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                    turn = 4;
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    move = true;
                    moves = Dice.Roll();
                    dice.GetComponent<Dice>().Render(moves, -10, -1, 10, 10);
                    Debug.Log("moves = " + moves);
                }

                
                Debug.Log(turn);
            }
        }

        
    }
}