using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    internal class PlayerBehaviour : MonoBehaviour
    {
        public void Movement()
        {
            var x = Input.GetAxis("Horizontal")*10f;
            var y = Input.GetAxis("Vertical")*10f;
            
            transform.Translate(x, y, 0);
        }
    }
}