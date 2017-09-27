#pragma strict

var characterController: CharacterController;

private var daMotor: PenguinMotor;

function Awake() 
{
	characterController = GetComponent("CharacterController") as CharacterController;
	daMotor = GetComponent(PenguinMotor);
	PlayerCamera.UseExistingOrCreateNewCam();
}

function Update()
{
	if (Camera.main == null)
	{
		return;
	}
	GetLocoInput();
	HandleActionInput();
	
	daMotor.UpdateMotor();
}

function GetLocoInput() //This is for getting the player's input and then passing that to 'PenguinMotor'
{
	var deadZone = .1;
	
	daMotor.verticalVel = daMotor.moveVector.y;
	daMotor.moveVector = Vector3.zero;
	
	if (Input.GetAxis("Vertical") > deadZone || Input.GetAxis("Vertical") < - deadZone)
	{
		daMotor.moveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));
	}
	
	if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < - deadZone)
	{
		daMotor.moveVector += new Vector3(Input.GetAxis("Horizontal"), 0, 0);
	}
	PenguinAnimator.instance.DetermineCurrentMoveDirection();
}

function HandleActionInput()
{
	if (Input.GetButton("Jump"))
	{
		Jump();
	}
}

function Jump()
{
	daMotor.Jump();
}	