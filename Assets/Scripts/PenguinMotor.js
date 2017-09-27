#pragma strict

public class PenguinMotor extends MonoBehaviour
{
	var forwardSpeed: float = 5.0;
	var backwardSpeed: float = 2.5;
	var strafeSpeed: float = 4.0;
	
	var jumpSpeed = 9.0;
	var gravity = 15.0;
	var terminalVel = 30.0;
	
	var moveVector: Vector3;
	static var instance: PenguinMotor;
	var verticalVel: float;
	
	private var daMover: PenguinMover;
	
	function Awake()
	{
		instance = this;
		daMover = GetComponent(PenguinMover);
	}
	
	function UpdateMotor()
	{
		SnapAlignCharWithCam();
		ProcessMotion();
	}
	
	function ProcessMotion()
	{
		// Transform moveVector to world space
		moveVector = transform.TransformDirection(moveVector);
		// Normalize moveVector if magnitude is >1
		if (moveVector.magnitude > 1)
		{
			moveVector = Vector3.Normalize(moveVector);
		}
		// Multiply normalized moveVector by moveSpeed
		moveVector *= MoveSpeed();
		
		//Reapply verticalVel to moveVector.y
		moveVector = Vector3(moveVector.x, verticalVel, moveVector.z);
		
		//Apply gravity
		ApplyGravity();
	
		// Move the character in world space
		daMover.characterController.Move(moveVector * Time.deltaTime);
	
	}
	function ApplyGravity()
	{
		if (moveVector.y > - terminalVel)
		{
			moveVector = Vector3(moveVector.x, moveVector.y - gravity * Time.deltaTime, moveVector.z);
		}
		
		if (daMover.characterController.isGrounded && moveVector.y < -1)
		{
			moveVector = Vector3(moveVector.x, -1, moveVector.z);
		}
	}
	public function Jump()
	{
		if (daMover.characterController.isGrounded)
		{
			verticalVel = jumpSpeed;
		}
	}
	function SnapAlignCharWithCam()
	{
		if (moveVector.x != 0 || moveVector.z != 0)
		{
			transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 
				Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
		}
	}
	
	function MoveSpeed()
	{
		var moveSpeed = 0.0;
		
		switch (PenguinAnimator.instance.moveDirection)
		{
			case PenguinAnimator.direction.Stationary:
				moveSpeed = 0.0;
				break;
			case PenguinAnimator.direction.Forward:
				moveSpeed = forwardSpeed;
				break;
			case PenguinAnimator.direction.Backward:
				moveSpeed = backwardSpeed;
				break;
			case PenguinAnimator.direction.Left:
				moveSpeed = strafeSpeed;
				break;	
			case PenguinAnimator.direction.Right:
				moveSpeed = strafeSpeed;
				break;
			case PenguinAnimator.direction.LeftForward:
				moveSpeed = forwardSpeed;
				break;
			case PenguinAnimator.direction.RightForward:
				moveSpeed = forwardSpeed;
				break;
			case PenguinAnimator.direction.LeftBackward:
				moveSpeed = backwardSpeed;
				break;
			case PenguinAnimator.direction.RightBackward:
				moveSpeed = backwardSpeed;
				break;
		}
		
		return moveSpeed;
	}
}