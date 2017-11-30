using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
	public class Dice : MonoBehaviour
	{		
		public GameObject diceFace;
		private Transform diceTransform;

		void Start()
		{
            // right now const parameters, edit as needed
            const int width = 100, height = 100;
            this.Render(Dice.Roll(1, 6), 0, 0, width, height);
        }
		
		static int Roll (int min, int max) 
		{
			int rolled = Random.Range(min,max);
			return rolled;
		}

		void Render (int numberRolled, int x, int y, int width, int height)
		{
            diceFace = Instantiate(Resources.Load("Prefabs/DiceFace") as GameObject);
            diceFace.GetComponent<SpriteRenderer>().sprite = Resources.Load("dice_faces/" + numberRolled, typeof(Sprite)) as Sprite;
            diceFace.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            diceFace.transform.localScale = new Vector3(width, height, 1);
            diceFace.transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
            //Debug.Log("Rolled a " + numberRolled);
            // TODO: Set Scale
		}

	}
}


