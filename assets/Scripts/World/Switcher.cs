using UnityEngine;
using System.Collections;

public class Switcher : MonoBehaviour {

	private Renderer Switchrenderer = new Renderer();
	private Color MainColor = Color.red;
	private Color[] Colors = new Color[] {Color.red, Color.green, Color.blue, Color.white};
	public int changeColors = 0;
	private int ColorID = 0;


	IEnumerator ChangeColor() {
		while(true) {
			Switchrenderer.material.SetColor ("_Color", MainColor);
		if(ColorID < 2) {
			ColorID++;
		}
		else {
			ColorID = 0;
		}
			MainColor = Colors [ColorID];
			yield return new WaitForSeconds(1);
	}
}
	void Start() {
		Switchrenderer = GetComponent<Renderer> ();
		if(changeColors == 0) {
		Switchrenderer.material.SetColor ("_Color", Colors [3]);
		} else if(changeColors == 1)
        {
            Switchrenderer.material.SetColor("_Color", Colors[0]);
        } else if(changeColors == 2)
        {
            Switchrenderer.material.SetColor("_Color", Colors[1]);
        } else if(changeColors == 3)
        {
            Switchrenderer.material.SetColor("_Color", Colors[2]);
        }
		else {
			StartCoroutine (ChangeColor());
		}
	}
}
