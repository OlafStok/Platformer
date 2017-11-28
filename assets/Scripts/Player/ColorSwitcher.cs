using UnityEngine;

public class ColorSwitcher : MonoBehaviour {

	public static Renderer playerrenderer = new Renderer();
	public BoxCollider2D boxCollider;
	public LayerMask switchblock;
	public float range;
	public int horizontalRayCount = 4;
	float horizontalRaySpacing;
	public RaycastOrigins raycastOrigins;
	private GameObject hitObject;
    private Color MainColor;
	public float speed;
	public float jump;
	public float timetojumpapex;

    //raycast
    public struct RaycastOrigins {
		public Vector2 topLeft, topRight, topMiddle;
	}

	//raycast
	public GameObject RayCastObject;
	public void CheckForHit() {
		Bounds bounds = boxCollider.bounds;
		bounds.Expand (0.015f * -2);

		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
		raycastOrigins.topMiddle = new Vector2 (bounds.center.x, bounds.max.y);


		Vector3 fwd = RayCastObject.transform.TransformDirection (Vector3.up);
		Debug.DrawRay (raycastOrigins.topMiddle, fwd * range, Color.green);
		Debug.DrawRay (raycastOrigins.topLeft, fwd * range, Color.green);
		Debug.DrawRay (raycastOrigins.topRight, fwd * range, Color.green);
		RaycastHit2D hit = (Physics2D.Raycast (raycastOrigins.topMiddle, fwd, range, switchblock));
		RaycastHit2D hit2 = (Physics2D.Raycast (raycastOrigins.topLeft, fwd, range, switchblock));
		RaycastHit2D hit3 = (Physics2D.Raycast (raycastOrigins.topRight, fwd, range, switchblock));

		//als je raakt verandert kleur
		if (hit || hit2 || hit3) {
            print("hit colorswitcher");
            if (hit) {
						hitObject = hit.transform.gameObject;
					} else if (hit2) {
						hitObject = hit2.transform.gameObject;
					} else {
						hitObject = hit3.transform.gameObject;
					}
			MainColor = hitObject.GetComponent<Renderer> ().material.GetColor ("_Color");
			print (MainColor);
			playerrenderer.material.SetColor ("_Color", MainColor);
		}
		
	}

	private void determineAttributes() {
		if (ColorSwitcher.playerrenderer.material.GetColor("_Color") == Color.blue) {
			Movement.runSpeed = speed*2;
			Movement.walkSpeed = speed;
			Movement.maxJumpHeight = jump*4;
			Movement.timeToJumpApex = 0.6f;
			Movement.CalculateJumpHeight ();
		}
		else if (ColorSwitcher.playerrenderer.material.GetColor("_Color") == Color.red) {
			Movement.maxJumpHeight = jump/1.5f;
			Movement.timeToJumpApex = 0.4f;
			Movement.runSpeed = speed*3;
			Movement.walkSpeed = speed*1.5f;
			Movement.CalculateJumpHeight ();
		}
		else if (ColorSwitcher.playerrenderer.material.GetColor("_Color") == Color.green) {
			Movement.maxJumpHeight = jump*1.5f;
			Movement.timeToJumpApex = 1.0f;
			Movement.runSpeed = speed*1.5f;
			Movement.walkSpeed = speed*0.75f;
			Movement.CalculateJumpHeight ();
		}
		else if (ColorSwitcher.playerrenderer.material.GetColor("_Color") == Color.white) {
            Movement.maxJumpHeight = jump;
			Movement.timeToJumpApex = timetojumpapex;
			Movement.runSpeed = speed*1.5f;
			Movement.walkSpeed = speed;
            Movement.CalculateJumpHeight();
        }
		else {
            //back to neutral

		}
	}

	void Start () {
		playerrenderer = GetComponent<Renderer> ();
		boxCollider = GetComponent<BoxCollider2D> ();
	}
	

	void Update () {
		CheckForHit ();
		determineAttributes ();
	}



}
