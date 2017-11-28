using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildToPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
		playerrenderer = GetComponent<Renderer> ();
		boxCollider = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckForHit ();
	}

	public static Renderer playerrenderer = new Renderer();
	public BoxCollider2D boxCollider;
	public LayerMask switchblock;
	//raycast length, at 0, theyŕe just dots
	private float range=0, rangel=0, ranger=0;
	private float horizontalRaySpacing;
	public RaycastOrigins raycastOrigins;
	private GameObject hitObject;
	private bool parented = false;

	//raycast
	public struct RaycastOrigins {
		public Vector2 botLeft, botRight, botMid, leftTop, leftBot, leftMid, rightTop, rightBot, rightMid;
	}

	//raycast
	public GameObject RayCastObject;
	public void CheckForHit() {
		Bounds bounds = boxCollider.bounds;
		bounds.Expand (0.015f * 2);

		raycastOrigins.botLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.botRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.botMid = new Vector2 (bounds.center.x, bounds.min.y);

		raycastOrigins.leftBot = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.leftTop = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.leftMid = new Vector2 (bounds.min.x, bounds.center.y);

		raycastOrigins.rightTop = new Vector2 (bounds.max.x, bounds.max.y);
		raycastOrigins.rightBot = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.rightMid = new Vector2 (bounds.max.x, bounds.center.y);


		Vector3 fwd = RayCastObject.transform.TransformDirection (Vector3.up);
		Vector3 rgt = RayCastObject.transform.TransformDirection (Vector3.right);

		Debug.DrawRay (raycastOrigins.botMid, fwd * range, Color.blue);
		Debug.DrawRay (raycastOrigins.botLeft, fwd * range, Color.blue);
		Debug.DrawRay (raycastOrigins.botRight, fwd * range, Color.blue);

		Debug.DrawRay (raycastOrigins.leftBot, rgt * rangel, Color.blue);
		Debug.DrawRay (raycastOrigins.leftTop, rgt * rangel, Color.blue);
		Debug.DrawRay (raycastOrigins.leftMid, rgt * rangel, Color.blue);

		Debug.DrawRay (raycastOrigins.rightBot, rgt * ranger, Color.blue);
		Debug.DrawRay (raycastOrigins.rightMid, rgt * ranger, Color.blue);
		Debug.DrawRay (raycastOrigins.rightTop, rgt * ranger, Color.blue);

		RaycastHit2D hit = (Physics2D.Raycast (raycastOrigins.botMid, fwd, range, switchblock));
		RaycastHit2D hit2 = (Physics2D.Raycast (raycastOrigins.botLeft, fwd, range, switchblock));
		RaycastHit2D hit3 = (Physics2D.Raycast (raycastOrigins.botRight, fwd, range, switchblock));

		RaycastHit2D hitl = (Physics2D.Raycast (raycastOrigins.leftBot, rgt, rangel, switchblock));
		RaycastHit2D hitl2 = (Physics2D.Raycast (raycastOrigins.leftMid, rgt, rangel, switchblock));
		RaycastHit2D hitl3 = (Physics2D.Raycast (raycastOrigins.leftTop, rgt, rangel, switchblock));

		RaycastHit2D hitr = (Physics2D.Raycast (raycastOrigins.rightBot, rgt, ranger, switchblock));
		RaycastHit2D hitr2 = (Physics2D.Raycast (raycastOrigins.rightMid, rgt, ranger, switchblock));
		RaycastHit2D hitr3 = (Physics2D.Raycast (raycastOrigins.rightTop, rgt, ranger, switchblock));

		//als je raakt parent je
		if (hit || hit2 || hit3 || hitl || hitl2 || hitl3 || hitr || hitr2 || hitr3) {
			print("hit moving platform");
			if (!parented) {

				if (hit) {
					hitObject = hit.transform.gameObject;
				} else if (hit2) {
					hitObject = hit2.transform.gameObject;
				} else if (hit3) {
					hitObject = hit3.transform.gameObject;
				} else if (hitl) {
					hitObject = hitl.transform.gameObject;
				} else if (hitl2) {
					hitObject = hitl2.transform.gameObject;
				} else if (hitl3) {
					hitObject = hitl3.transform.gameObject;
				} else if (hitr) {
					hitObject = hitr.transform.gameObject;
				} else if (hitr2) {
					hitObject = hitr2.transform.gameObject;
				} else if (hitr3) {
					hitObject = hitr3.transform.gameObject;
				}
			}
			transform.SetParent (hitObject.transform);
			parented = true;
		}
		else if(!hit && !hit2 && !hit3 && !hitl && !hitl2 && !hitl3 && !hitr && !hitr2 && !hitr3 && parented) {
			hitObject.transform.DetachChildren ();
			parented = false;
		}
	}
}