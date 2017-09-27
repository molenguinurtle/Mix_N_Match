#pragma strict

public class PlayerCamera extends MonoBehaviour
{
	var instance: PlayerCamera;
	var targetLookAt: Transform;
	var distance = 5.0;
	var distanceMin = 2.0;
	var distanceMax = 10.0;
	var distanceSmooth = 0.05;
	var x_MouseSensitivity = 5.0;
	var y_MouseSensitivity = 5.0;
	var x_Smooth = 0.05;
	var y_Smooth = 0.1;
	var y_MinLimit = -30.0;
	var y_MaxLimit = 80.0;
	
	private var mouseX = 0.0;
	private var mouseY = 0.0;
	private var velX = 0.0;
	private var velY = 0.0;
	private var velZ = 0.0;
	private var pos = Vector3.zero; //position
	private var velDistance = 0.0;
	private var startDistance = 0.0;
	private var desiredDistance = 0.0;
	private var desiredPos = Vector3.zero; //desired position
	
	function Awake()
	{
		instance = this;
	}
	function Start() 
	{
		distance = Mathf.Clamp(distance, distanceMin, distanceMax);
		startDistance = distance;
		Reset();
	}
	
	function LateUpdate()
	{
		//If we don't have a target to look at, we're done here
		if (targetLookAt == null)
		{
			return;
		}
		
		HandlePlayerInput();
		CalculateDesiredPos();
		UpdatePos();
	}
	
	function HandlePlayerInput()
	{
		mouseX += Input.GetAxis("Mouse X") * x_MouseSensitivity;
		mouseY += Input.GetAxis("Mouse Y") * y_MouseSensitivity;
		
		//This is where we limit mouseY using the Helper class
		mouseY = Helper.ClampAngle(mouseY, y_MinLimit, y_MaxLimit);
		
		
	}
	
	function CalculateDesiredPos()
	{
		//Evaluate distance
		distance = Mathf.SmoothDamp(distance, desiredDistance, velDistance, distanceSmooth);
		
		//Calculate desired position
		desiredPos = CalculatePosition(mouseY, mouseX, distance);
	}
	
	function CalculatePosition(rotationX: float, rotationY: float, distance: float)
	{
		var direction = Vector3(0, 0, -distance);
		var rotation = Quaternion.Euler(-rotationX, rotationY, 0);
		return targetLookAt.position + rotation * direction; 
	}
	
	function UpdatePos()
	{
		var posX = Mathf.SmoothDamp(pos.x, desiredPos.x, velX, x_Smooth);
		var posY = Mathf.SmoothDamp(pos.y, desiredPos.y, velY, y_Smooth);
		var posZ = Mathf.SmoothDamp(pos.z, desiredPos.z, velZ, x_Smooth);
		pos = Vector3(posX, posY, posZ);
		
		transform.position = pos; //Update the camera's position with the smoothed position we just calculated
		
		transform.LookAt(targetLookAt); //tell the camera to look at the 'targetLookAt'/the player
	}
	
	public function Reset()
	{
		mouseX = 0.0;
		mouseY = 10.0;
		distance = startDistance;
		desiredDistance = distance;
	}
	
	public static function UseExistingOrCreateNewCam()
	{
		var tempCam: GameObject;
		var targLookAt: GameObject;
		var myCam: PlayerCamera;
		
		if (Camera.main != null)
		{
			tempCam = Camera.main.gameObject;
		}
		else
		{
			tempCam = new GameObject("Main Camera");
			tempCam.AddComponent.<Camera>();
			tempCam.tag = "MainCamera";
		}
		
		tempCam.AddComponent.<PlayerCamera>();
		myCam = tempCam.GetComponent(PlayerCamera) as PlayerCamera;
		
		targLookAt = GameObject.Find("camLookAt") as GameObject;
		
		if (targLookAt == null)
		{
			targLookAt = new GameObject("camLookAt");
			targLookAt.transform.position = Vector3.zero;
		}
		
		myCam.targetLookAt = targLookAt.transform;
	}
	
}