using UnityEngine;

[RequireComponent (typeof (PlayerRaycasting))]
public class Movement : MonoBehaviour {

	public static float maxJumpHeight = 4;
	public static float minJumpHeight = 1;
	public static float timeToJumpApex = .4f;
	public float accelerationTimeAirborne = .2f;
	public float accelerationTimeGrounded = .1f;
	public float accelerationTimeAirborneR = .3f;
	public float accelerationTimeGroundedR = .15f;
	public static float moveSpeed = 6;
	public static float runSpeed;
	public static float walkSpeed;
	public float stopsmoothtimeground, stopsmoothtimeair;
	private bool running;
	public float wallslideaccel;
	private float wallslidevelocity;
	float KEYA;
	float KEYD;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	static float gravity;
	static float maxJumpVelocity;
	static float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothingA, velocityXSmoothingAR, velocityXSmoothingD, velocityXSmoothingDR, velocityXSmoothingstoppingAIR, velocityXSmoothingstoppingGROUND;

	PlayerRaycasting controller;

	void Start() {
		controller = GetComponent<PlayerRaycasting> ();
		CalculateJumpHeight ();
	}

	public static void CalculateJumpHeight() {
		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
//		print ("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);
	}


	public void movePlayer() {
		if(Input.GetKey(KeyCode.A)){
			KEYA = -1;
		}
		if(Input.GetKey(KeyCode.D)){
			KEYA = 1;
		}
		if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
			KEYA = 0;
		}
		if(Input.GetKey(KeyCode.LeftShift)){
			moveSpeed = runSpeed;
			running = true;
		}
		else if(!Input.GetKey(KeyCode.LeftShift)){
			moveSpeed = walkSpeed;
			running = false;
		}

		Vector2 input = new Vector2 (KEYA, Input.GetAxisRaw ("Vertical"));
		int wallDirX = (controller.collisions.left) ? -1 : 1;

		float targetVelocityX = input.x * moveSpeed;
		//if holding a button, acceleration is defined by this
		if(Input.GetKey(KeyCode.A)) {
			if(!running) {
				velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothingA, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
			}
			else {
				velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothingAR, (controller.collisions.below)?accelerationTimeGroundedR:accelerationTimeAirborneR);
			}
		}
		else if(Input.GetKey(KeyCode.D)) {
			if(!running) {
				velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothingD, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
			}
			else {
				velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothingDR, (controller.collisions.below)?accelerationTimeGroundedR:accelerationTimeAirborneR);
			}
		}
		else {
			if(controller.collisions.below) { // if not in the air (collisions below us)
				velocity.x = Mathf.SmoothDamp(velocity.x, 0, ref velocityXSmoothingstoppingGROUND, stopsmoothtimeground);
			}
			else { //if in the air
				velocity.x = Mathf.SmoothDamp(velocity.x, 0, ref velocityXSmoothingstoppingAIR, stopsmoothtimeair);
			}
		}
		//velocity.x = targetVelocityX;
		bool wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below/* && velocity.y < 0*/) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				//this is instant max wallslide speed
				velocity.y = -wallSlideSpeedMax;
				//velocity.y = Mathf.SmoothDamp(velocity.y, -wallSlideSpeedMax, ref wallslidevelocity, wallslideaccel);
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothingA = 0;
				velocityXSmoothingD = 0;
				velocityXSmoothingstoppingAIR = 0;
				velocityXSmoothingstoppingGROUND = 0;
				velocity.x = 0;

				if (input.x != wallDirX && input.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}

		if (Input.GetKeyDown (KeyCode.Space) && !Input.GetKey(KeyCode.S)) {
			if (wallSliding == true) {
				if (wallDirX == input.x) {
					velocity.x = -wallDirX * wallJumpClimb.x;
					velocity.y = wallJumpClimb.y;
				}
				else if (input.x == 0) {
					velocity.x = -wallDirX * wallJumpOff.x;
					velocity.y = wallJumpOff.y;
				}
				else {
					velocity.x = -wallDirX * wallLeap.x;
					velocity.y = wallLeap.y;
				}
			}
			if (controller.collisions.below) {
				velocity.y = maxJumpVelocity;
			}
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			if (velocity.y > minJumpVelocity) {
				velocity.y = minJumpVelocity;
			}
		}


		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime, input);

		if (controller.collisions.above || controller.collisions.below) {
			if(!controller.collisions.slidingDownMaxSlope) {
			velocity.y = 0;
			}
		}
	}

	void Update() {
		movePlayer ();
	}
}