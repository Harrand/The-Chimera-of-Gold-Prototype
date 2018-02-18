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
        //public DecisionTree treescript;
        //public AIBehaviour aiscript;
        // public PlayerBehaviour playerMovement;

        // Use this for initialization
        void Start()
        {
            boardScript = GetComponent<BoardManager>();
            playerScript = GetComponent<PlayerManager>();
            obstacleScript = GetComponent<ObstacleManager>();
            //treescript = GetComponent<DecisionTree>();
            //aiscript = GetComponent<AIBehaviour>();

            playerScript.obstacleScript = obstacleScript;
            obstacleScript.setupManager (playerScript);
            //aiscript.setupDecisionTree(treescript);

            initBoard();
        
        }

        void initBoard()
		{
			boardScript.checkPlayerNumbers(5);
            boardScript.SetupScene();
            playerScript.SetupPlayers();
            obstacleScript.SetupObstacles();
        }

        // Update is called once per frame
        void Update()
        {
            playerScript.UpdatePlayers();
			obstacleScript.UpdateObstacle();
            //aiScript.UpdateAIs();
        }
    }
}