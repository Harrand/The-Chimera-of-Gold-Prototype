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

        // Use this for initialization
        void Start()
        {
            boardScript = GetComponent<BoardManager>();
            playerScript = GetComponent<PlayerManager>();
            obstacleScript = GetComponent<ObstacleManager>();
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
        }
    }
}