using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class GameManager : MonoBehaviour
    {
        public BoardManager boardScript;
        public PlayerManager playerScript;

        // Use this for initialization
        void Start()
        {
            boardScript = GetComponent<BoardManager>();
            playerScript = GetComponent<PlayerManager>();
            initBoard();
        }

        void initBoard()
        {
            boardScript.SetupScene();
            playerScript.SetupPlayers();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}