using UnityEngine;

namespace Chimera
{
	public class Dice : MonoBehaviour
	{		
        // Harry and Ciara 30/11/2017
		public GameObject diceFace;
		private Transform diceTransform;

		public void Start()
		{
            // right now const parameters, edit as needed
            const int width = 10, height = 10;
            // upon script initialisation roll a default D6.
            this.Render(Dice.Roll(1, 6), -10, -1, width, height);
        }
		
		public static int Roll (int min, int max) 
		{
			int rolled = Random.Range(min,max);
			return rolled;
		}

		public void Render (int numberRolled, int x, int y, int width, int height)
		{
            // Make copy of existing 'DiceFace' prefab
            diceFace = Instantiate(Resources.Load("Prefabs/DiceFace") as GameObject);
            // Edit sprite to be loaded texture dice_faces/x.png where x is number rolled.
            diceFace.GetComponent<SpriteRenderer>().sprite = Resources.Load("dice_faces/" + numberRolled, typeof(Sprite)) as Sprite;
            // White background or the whole thing is black.
            diceFace.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            // Set scale, position and rotation.
            diceFace.transform.localScale = new Vector3(width, height, 1);
            diceFace.transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
		}

	}
}


