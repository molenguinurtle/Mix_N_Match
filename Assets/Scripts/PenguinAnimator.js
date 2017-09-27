#pragma strict

public class PenguinAnimator extends MonoBehaviour
{
	enum direction {
	Stationary, Forward, Backward, Left, Right,
	LeftForward, RightForward, LeftBackward, RightBackward
	}
	
	static var instance: PenguinAnimator;
	var moveDirection: direction;
	function Awake()
	{
		instance = this;
	}
	
	function Update()
	{
	
	}
	
	function DetermineCurrentMoveDirection()
	{
		var forward = false;
		var backward = false;
		var left = false;
		var right = false;
		
		if (PenguinMotor.instance.moveVector.z > 0)
		{
			forward = true;
		}
		if (PenguinMotor.instance.moveVector.z < 0)
		{
			backward = true;
		}		
		if (PenguinMotor.instance.moveVector.x > 0)
		{
			right = true;
		}		
		if (PenguinMotor.instance.moveVector.x < 0)
		{
			left = true;
		}
		
		if (forward)
		{
			if (left)
			{
				moveDirection = direction.LeftForward;
			}
			else if (right)
			{
				moveDirection = direction.RightForward;
			}
			else
			{
				moveDirection = direction.Forward;
			}
		}
		else if (backward)
		{
			if (left)
			{
				moveDirection = direction.LeftBackward;
			}
			else if (right)
			{
				moveDirection = direction.RightBackward;
			}
			else
			{
				moveDirection = direction.Backward;
			}
		}
		else if (left)
		{
			moveDirection = direction.Left;
		}
		else if (right)
		{
			moveDirection = direction.Right;
		}
		else
		{
			moveDirection = direction.Stationary;
		}
	}
}