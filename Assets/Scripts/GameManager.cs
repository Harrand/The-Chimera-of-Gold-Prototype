using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class GameManager : MonoBehaviour
    {
        public BoardManager boardScript;
        public PlayerManager playerScript;
        public ObstacleManager obstacleScript;
        //public AI aiScript;

        // Use this for initialization
        void Start()
        {
            boardScript = GetComponent<BoardManager>();
            playerScript = GetComponent<PlayerManager>();
            obstacleScript = GetComponent<ObstacleManager>();
            //aiScript = GetComponent<AI>();
            playerScript.obstacleScript = obstacleScript;
			obstacleScript.setupManager (playerScript);

            initBoard();
        
        }

        void initBoard()
		{
			boardScript.checkPlayerNumbers(5);
            boardScript.SetupScene();
            playerScript.SetupPlayers();
            obstacleScript.SetupObstacles();
            //aiScript.SetupAI();
            //setupPawns();
        }

        /*void setupPawns()
        {
            aiScript.Players = playerScript.Players;
            aiScript.Pawns = playerScript.Pawns;
        }*/

        // Update is called once per frame
        void Update()
        {
            playerScript.UpdatePlayers();
			obstacleScript.UpdateObstacle();
            //aiScript.UpdateAIs();
        }
    }
}