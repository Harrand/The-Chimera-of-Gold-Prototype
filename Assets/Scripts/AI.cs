using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{

    public class AI : MonoBehaviour
    {
        public GameObject[] Players;
        public GameObject[] Pawns;
        public bool[] OnObstacles;
        public GameObject dice;
        public GameObject ring;
        public ObstacleManager obstacleScript;
        public int restMove = 0;
        int[] points = new int[] { 0, 0, 0, 0, 0 };
        public int index = 0;
        int turn = 0;
        int pawnsIndex = 0;
        int pawnCount = 0;
        int playerTurn = 0;
        int moves = 0;
        int moved = 0;
        bool move = false;
        Vector3 end = new Vector3(10, 17, 0f);

        private void setupDice()
        {
            dice = Instantiate(Resources.Load("Prefabs/DiceFace") as GameObject);
            dice.AddComponent<Dice>();
            dice.GetComponent<Dice>().diceFace = dice;
            dice.GetComponent<Dice>().Start();
        }

        public GameObject getCurrentObject()
        {
            return Pawns[index];
        }

        public void SetupAI()
        {
            setupDice();

            ring = GameObject.Find("ring");

            Pawns = new GameObject[25];
            for (int x = -1; x < 21; x++)
            {

                if (CheckPlayerTile(x))
                {
                    while (pawnCount < 5)
                    {
                        GameObject Pawn = Instantiate(Players[index], new Vector3(x, -1, 0f), Quaternion.identity) as GameObject;
                        Pawn.AddComponent<AIBehaviour>();
                        Pawn.GetComponent<AIBehaviour>().origin.x = x;
                        Pawn.GetComponent<AIBehaviour>().origin.y = -1;
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
            OnObstacles = new bool[25];
            for (int i = 0; i < 25; i++)
                OnObstacles[i] = false;
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
            int a = playerTurn * 5 + i;
            int x = 0;
            while (Pawns[a] == null)
            {
                a = playerTurn * 5 + x;
                x++;
            }

            return a;
        }

        public void nextPlayer()
        {
            playerTurn++;
            turn = 0;

            if (playerTurn >= 5)
                playerTurn = 0;
        }

        void removePawn(GameObject pawn)
        {
            Destroy(pawn);
        }

        void addPoint(int i)
        {
            points[i] += 1;
            Debug.Log(i + " has " + points[i] + "points");
        }
        bool checkWinner()
        {
            for (int i = 0; i < 5; i++)
            {
                if (points[i] == 5)
                    return true;
            }
            return false;
        }

        /*Vector3 findPaths(int dicepoints)
        {

            return;
        }*/

        // Update is called once per frame
        public void UpdateAIs()
        {

            if (move)
            {
                index = choosePawn(turn);
                ring.transform.position = Pawns[index].GetComponent<AIBehaviour>().transform.position;
                moved = Pawns[index].GetComponent<AIBehaviour>().Movement(index, moves);

                Debug.Log("Moving pawn");
                Pawns[index].GetComponent<AIBehaviour>().restMove = moves - moved;
                //If on the final tile and can finish (Roll a one or have one left to move)
                if ((Pawns[index].GetComponent<AIBehaviour>().transform.position.x == 10 && Pawns[index].GetComponent<AIBehaviour>().transform.position.y == 18) && (moves - moved) == 1)
                {
                    Pawns[index].GetComponent<AIBehaviour>().transform.position = end;
                    removePawn(Pawns[index]);
                    Pawns[index] = null;
                    addPoint(playerTurn);
                    if (checkWinner())
                    {
                        Debug.Log("WINNER WINNER CHICKEN DINNER!!!");
                    }

                    move = false;
                    moved = 0;

                    nextPlayer();

                    ring.transform.position = Pawns[choosePawn(0)].GetComponent<AIBehaviour>().transform.position;
                }

                if (moved >= moves || Pawns[index].GetComponent<AIBehaviour>().meetObsracle)
                {
                    Debug.Log("into if move");
                    //Checks if colliding 
                    for (int i = 0; i < Pawns.Length; i++)
                    {
                        if (Pawns[i] != null)
                        {
                            if (i != index && (Pawns[index].GetComponent<AIBehaviour>().transform.position == Pawns[i].GetComponent<AIBehaviour>().transform.position))
                            {
                                Pawns[i].GetComponent<AIBehaviour>().transform.position = Pawns[i].GetComponent<AIBehaviour>().origin;
                            }
                        }
                    }
                    move = false;
                    Pawns[index].GetComponent<AIBehaviour>().resetMoves();
                    moved = 0;
                    OnObstacles[index] = obstacleScript.CheckObstacle((int)Pawns[index].transform.position.x, (int)Pawns[index].transform.position.y);
                    nextPlayer();
                }

            }
            else
            {
                //Debug.Log("Choose pawn, press space when ready to move");
                turn = 0;

                move = true;
                moves = Dice.Roll();
                dice.GetComponent<Dice>().Render(moves, -10, -1, 10, 10);           
                ring.transform.position = Pawns[choosePawn(turn)].GetComponent<AIBehaviour>().transform.position;
                //Debug.Log(turn);
            }
        }
    }
}