using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	public float lookAheadDistance;
	private Vector3 destination;
	private bool lookingRight;
	private float lerpValue = 0;
    private bool moving = false;
    private Vector3 oldposition;
    private Vector3 lerpPosition;

	// Use this for initialization
	void Start () {
        oldposition = target.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (target) {
            if(target.position == oldposition)
            {
                //you're not moving
                moving = false;
            }
            else if(target.position != oldposition)
            {
                //you're moving
                moving = true;
            }
            //set old position
            oldposition = target.position;

			Vector3 point = GetComponent<Camera>().WorldToViewportPoint (target.position);
			Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, point.z));
			destination = transform.position + delta;

			if(Input.GetKeyDown(KeyCode.A)) {
				lookingRight = false;
			}
			else if(Input.GetKeyDown(KeyCode.D)) {
				lookingRight = true;
			}
			if(!lookingRight) {
				destination.x = destination.x - lookAheadDistance;
			}
			else{
				destination.x = destination.x + lookAheadDistance;
			}
            //transform.position = Vector3.SmoothDamp (transform.position, destination, ref velocity, dampTime);
            //17.7 became 15.4 instantly
            //		transform.position = Vector3.Lerp (transform.position, destination, lerpValue);

            //set lerp position
            lerpPosition = Vector3.Lerp(transform.position, destination, 0.05f);
                lerpValue += 0.001f;

			if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyDown(KeyCode.D)) {
				lerpValue = 0f;
			}
			//needs bool moving with
			//if moving, lerp from current position to transform.position
			//if at transform.position, lerp to destination.
			//if stopped moving, lerp from current position to zero (destination without the lookahead)
		}
	}
    private void LateUpdate()
    {
        transform.position = lerpPosition;
        //print("lerping position = " + lerpPosition + "\ncurrent position = " + transform.position);
    }
}
