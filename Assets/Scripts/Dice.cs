using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
	public class Dice : MonoBehaviour
	{		
		public GameObject[] diceFace;

		void Start()
		{
			Debug.Log(Dice.Roll(1,6));
		}
		
		static int Roll (int min, int max) 
		{
			int rolled = Random.Range(min,max);
			return rolled;
		}

		void Render (int numberRolled, int width, int height, int x, int y)
		{		
			for (int i = 0; i<numberRolled; i++)
			{
				Instantiate(diceFace);
			}
		}

	}
}


