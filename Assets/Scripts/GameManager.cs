using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class GameManager : MonoBehaviour
    {
        public BoardManager boardScript;
        // Use this for initialization
        void Start()
        {
            boardScript = GetComponent<BoardManager>();
            initBoard();
        }

        void initBoard()
        {
            boardScript.SetupScene();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}