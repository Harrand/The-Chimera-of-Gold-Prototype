using UnityEngine;

namespace Chimera
{
	public class Dice : MonoBehaviour
	{		
        // Harry and Ciara 30/11/2017
        //diceix??
		public GameObject diceFace;
		private Transform diceTransform;

		public void Start()
		{
            // right now const parameters, edit as needed
            const int width = 10, height = 10;
            // upon script initialisation roll a default D6.
            //diceFace = Instantiate(Resources.Load("Prefabs/DiceFace") as GameObject);
            //this.Resources.Load("Prefabs/DiceFace");
            this.Render(6, -10, -1, width, height);
        }
		
		public static int Roll () 
		{
			int rolled = Random.Range(1,7);
			return rolled;
		}

		public void Render (int numberRolled, int x, int y, int width, int height)
		{
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


