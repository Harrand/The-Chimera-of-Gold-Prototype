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
        public bool[] OnObstacles;
        public GameObject dice;
        public GameObject ring;
        public ObstacleManager obstacleScript;
        public int restMove = 0;
        int[] points = new int[] { 0,0,0,0,0};
        public int index = 0;
        int turn = 0;
        int pawnsIndex = 0;
        int pawnCount = 0;
        public static int playerTurn = 0;
        int moves = 0;
        int moved = 0;
        bool move = false;
        bool aimove = false;
        Vector3 end = new Vector3(10, 17, 0f);

        public int human = 4;

        public GameObject[] GetPlayers()
        {
            return Players;
        }
        public GameObject[] GetPawns()
        {
            return Pawns;
        }

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

        public void SetupPlayers()
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
                        Debug.Log(index);
                        GameObject Pawn = Instantiate(Players[index], new Vector3(x, -1, 0f), Quaternion.identity) as GameObject;

                        /*if (pawnsIndex >= human * 5)
                        {
                            Pawn.AddComponent<AIBehaviour>();
                            Pawn.GetComponent<AIBehaviour>().origin.x = x;
                            Pawn.GetComponent<AIBehaviour>().origin.y = -1;
                        }
                        Pawn.AddComponent<PlayerBehaviour>();
                        Pawn.GetComponent<PlayerBehaviour>().origin.x = x;
                        Pawn.GetComponent<PlayerBehaviour>().origin.y = -1;*/
                        if (pawnsIndex < human * 5)
                        {
                            //GameObject Pawn = Instantiate(Players[index], new Vector3(x, -1, 0f), Quaternion.identity) as GameObject;
                            Pawn.AddComponent<PlayerBehaviour>();
                            Pawn.GetComponent<PlayerBehaviour>().origin.x = x;
                            Pawn.GetComponent<PlayerBehaviour>().origin.y = -1;
                            //Pawns[pawnsIndex] = Pawn;
                        }
                        else
                        {
                            //GameObject Pawn = Instantiate(Players[index], new Vector3(x, -1, 0f), Quaternion.identity) as GameObject;
                            Pawn.AddComponent<AIBehaviour>();
                            Pawn.GetComponent<AIBehaviour>().origin.x = x;
                            Pawn.GetComponent<AIBehaviour>().origin.y = -1;
                            //Pawns[pawnsIndex] = Pawn;
                        }                        

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
            Debug.Log(a);
            while(Pawns[a] == null)
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
        
        // Update is called once per frame
        public void UpdatePlayers()
        {
            
            /*Aswin + Ciara: Limited movement based on dice roll 06/12/2017*/
            if (move && !aimove)
            {
                index = choosePawn(turn);
                ring.transform.position = Pawns[index].GetComponent<PlayerBehaviour>().transform.position;
                moved = Pawns[index].GetComponent<PlayerBehaviour>().Movement(index);
                Debug.Log("Human Moving pawn");
                Pawns[index].GetComponent<PlayerBehaviour>().restMove = moves - moved;
                //If on the final tile and can finish (Roll a one or have one left to move)
                if ((Pawns[index].GetComponent<PlayerBehaviour>().transform.position.x == 10 && Pawns[index].GetComponent<PlayerBehaviour>().transform.position.y == 18) && (moves - moved) == 1)
                {
                    Pawns[index].GetComponent<PlayerBehaviour>().transform.position = end;
                    removePawn(Pawns[index]);
                    Pawns[index] = null;
                    addPoint(playerTurn);
                    if(checkWinner())
                    {
                        Debug.Log("WINNER WINNER CHICKEN DINNER!!!");
                    }
                    
                    move = false;
                    moved = 0;

                    nextPlayer();

                    ring.transform.position = Pawns[choosePawn(0)].GetComponent<PlayerBehaviour>().transform.position;
                }

                if (moved >= moves || Pawns[index].GetComponent<PlayerBehaviour>().meetObsracle)
                {
                    Debug.Log("into if move");
                    //Checks if colliding 
                    for(int i = 0; i < Pawns.Length; i++)
                    {
                        if(Pawns[i] != null && Pawns[i].GetComponent<AIBehaviour>() == null)
                        {
                            if (Pawns[index].GetComponent<PlayerBehaviour>() != null && i != index && (Pawns[index].GetComponent<PlayerBehaviour>().transform.position == Pawns[i].GetComponent<PlayerBehaviour>().transform.position))
                            {
                                Pawns[i].GetComponent<PlayerBehaviour>().transform.position = Pawns[i].GetComponent<PlayerBehaviour>().origin;
                            }
                        }
                        if (Pawns[i] != null && Pawns[i].GetComponent<AIBehaviour>() != null)
                        {
                            if (Pawns[index].GetComponent<AIBehaviour>() != null && i != index && (Pawns[index].GetComponent<AIBehaviour>().transform.position == Pawns[i].GetComponent<AIBehaviour>().transform.position))
                            {
                                Pawns[i].GetComponent<AIBehaviour>().transform.position = Pawns[i].GetComponent<AIBehaviour>().origin;
                            }
                        }
                    }
                    move = false;
                    Pawns[index].GetComponent<PlayerBehaviour>().resetMoves();
                    moved = 0;
                    OnObstacles[index] = obstacleScript.CheckObstacle((int)Pawns[index].transform.position.x, (int)Pawns[index].transform.position.y);
                    nextPlayer();
                }

            }
            else if(move && aimove)
            {
                index = choosePawn(turn);
                ring.transform.position = Pawns[index].GetComponent<AIBehaviour>().transform.position;
                moved = Pawns[index].GetComponent<AIBehaviour>().Movement(index, moves);
                Debug.Log("AI Moving pawn");
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
                    aimove = false;
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
                        if (Pawns[i] != null && Pawns[i].GetComponent<AIBehaviour>() == null)
                        {
                            if (Pawns[index].GetComponent<PlayerBehaviour>() != null && i != index && (Pawns[index].GetComponent<PlayerBehaviour>().transform.position == Pawns[i].GetComponent<PlayerBehaviour>().transform.position))
                            {
                                Pawns[i].GetComponent<PlayerBehaviour>().transform.position = Pawns[i].GetComponent<PlayerBehaviour>().origin;
                            }
                        }
                        if (Pawns[i] != null && Pawns[i].GetComponent<AIBehaviour>() != null)
                        {
                            if (Pawns[index].GetComponent<AIBehaviour>() != null && i != index && (Pawns[index].GetComponent<AIBehaviour>().transform.position == Pawns[i].GetComponent<AIBehaviour>().transform.position))
                            {
                                Pawns[i].GetComponent<AIBehaviour>().transform.position = Pawns[i].GetComponent<AIBehaviour>().origin;
                            }
                        }
                    }
                    move = false;
                    aimove = false;
                    Pawns[index].GetComponent<AIBehaviour>().resetMoves();
                    moved = 0;
                    OnObstacles[index] = obstacleScript.CheckObstacle((int)Pawns[index].transform.position.x, (int)Pawns[index].transform.position.y);
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
                    if (Pawns[choosePawn(turn)].GetComponent<AIBehaviour>() == null)
                    {
                        aimove = false;
                    }
                    else
                    {
                        aimove = true;
                    }
                    moves = Dice.Roll();
                    dice.GetComponent<Dice>().Render(moves, -10, -1, 10, 10);
                }
                if (Pawns[choosePawn(turn)].GetComponent<AIBehaviour>() == null)
                {
                    ring.transform.position = Pawns[choosePawn(turn)].GetComponent<PlayerBehaviour>().transform.position;
                }
                else
                {
                    ring.transform.position = Pawns[choosePawn(turn)].GetComponent<AIBehaviour>().transform.position;
                }
                
                //Debug.Log(turn);
            }
        }
    }
}