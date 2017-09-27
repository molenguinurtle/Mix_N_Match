#pragma strict
var icons: Texture2D[];
var daSuite: String;
var camPos: String;
var border: GUIStyle;
function Start ()
{

}

function Update ()
{

}

function OnGUI()
{
	var z: int;
	var daCon: Texture2D;
	if (daSuite == "Mario")
	{
		daCon = icons[0];
	}
	else if (daSuite == "Sonic")
	{
		daCon = icons[1];
	}
	else if (daSuite == "Pokemon")
	{
		daCon = icons[2];
	}
	else if (daSuite == "Street Fighter")
	{
		daCon = icons[3];
	}
	if (camPos == "Bottom Right")
	{
		//***Bottom Right***
		//Horizontal edges
		for (z=0; z<(Screen.width/4)/(daCon.width/5)-1; z++)
		{
			GUI.Label (Rect (Screen.width/4*3+(daCon.width/5*z)+daCon.width/5, Screen.height-daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
			GUI.Label (Rect (Screen.width/4*3+(daCon.width/5*z)+daCon.width/5, Screen.height/2, daCon.width/5, daCon.height/5), daCon, border);
		}
		//Vertical edges
		for (z=0; z<(Screen.height/2)/(daCon.height/5)-1; z++)
		{
			GUI.Label (Rect (Screen.width/4*3, Screen.height/2+(daCon.height/5*z)+daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
			GUI.Label (Rect (Screen.width-daCon.width/5, Screen.height/2+(daCon.height/5*z)+daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
		}
		//Corners
		GUI.Label (Rect (Screen.width/4*3, Screen.height-daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
		GUI.Label (Rect (Screen.width/4*3, Screen.height/2, daCon.width/5, daCon.height/5), daCon, border);
		GUI.Label (Rect (Screen.width-daCon.width/5, Screen.height-daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
		GUI.Label (Rect (Screen.width-daCon.width/5, Screen.height/2, daCon.width/5, daCon.height/5), daCon, border);
	}
	else if (camPos == "Top Right")
	{
		//Horizontal edges
		for (z=0; z<(Screen.width/4)/(daCon.width/5)-1; z++)
		{
			GUI.Label (Rect (Screen.width/4*3+(daCon.width/5*z)+daCon.width/5, 0, daCon.width/5, daCon.height/5), daCon, border);
			GUI.Label (Rect (Screen.width/4*3+(daCon.width/5*z)+daCon.width/5, Screen.height/2-daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
		}
		//Vertical edges
		for (z=0; z<(Screen.height/2)/(daCon.height/5)-1; z++)
		{
			GUI.Label (Rect (Screen.width/4*3, (daCon.height/5*z)+daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
			GUI.Label (Rect (Screen.width-daCon.width/5, (daCon.height/5*z)+daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
		}
		//Corners
		GUI.Label (Rect (Screen.width/4*3, 0, daCon.width/5, daCon.height/5), daCon, border);
		GUI.Label (Rect (Screen.width/4*3, Screen.height/2-daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
		GUI.Label (Rect (Screen.width-daCon.width/5, 0, daCon.width/5, daCon.height/5), daCon, border);
		GUI.Label (Rect (Screen.width-daCon.width/5, Screen.height/2-daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
	}
	else if (camPos == "Top Left")
	{
		//Horizontal edges
		for (z=0; z<(Screen.width/4)/(daCon.width/5)-1; z++)
		{
			GUI.Label (Rect ((daCon.width/5*z)+daCon.width/5, 0, daCon.width/5, daCon.height/5), daCon, border);
			GUI.Label (Rect ((daCon.width/5*z)+daCon.width/5, Screen.height/2-daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
		}
		//Vertical edges
		for (z=0; z<(Screen.height/2)/(daCon.height/5)-1; z++)
		{
			GUI.Label (Rect (0, (daCon.height/5*z)+daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
			GUI.Label (Rect (Screen.width/4-daCon.width/5, (daCon.height/5*z)+daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
		}
		//Corners
		GUI.Label (Rect (0, 0, daCon.width/5, daCon.height/5), daCon, border);
		GUI.Label (Rect (Screen.width/4-daCon.width/5, Screen.height/2-daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
		GUI.Label (Rect (Screen.width/4-daCon.width/5, 0, daCon.width/5, daCon.height/5), daCon, border);
		GUI.Label (Rect (0, Screen.height/2-daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
	}
	else if (camPos == "Bottom Left")
	{
		//Horizontal edges
		for (z=0; z<(Screen.width/4)/(daCon.width/5)-1; z++)
		{
			GUI.Label (Rect ((daCon.width/5*z)+daCon.width/5, Screen.height-daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
			GUI.Label (Rect ((daCon.width/5*z)+daCon.width/5, Screen.height/2, daCon.width/5, daCon.height/5), daCon, border);
		}
		//Vertical edges
		for (z=0; z<(Screen.height/2)/(daCon.height/5)-1; z++)
		{
			GUI.Label (Rect (0, Screen.height/2+(daCon.height/5*z)+daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
			GUI.Label (Rect (Screen.width/4-daCon.width/5, Screen.height/2+(daCon.height/5*z)+daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
		}
		//Corners
		GUI.Label (Rect (0, Screen.height/2, daCon.width/5, daCon.height/5), daCon, border);
		GUI.Label (Rect (Screen.width/4-daCon.width/5, Screen.height/2, daCon.width/5, daCon.height/5), daCon, border);
		GUI.Label (Rect (Screen.width/4-daCon.width/5, Screen.height-daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
		GUI.Label (Rect (0, Screen.height-daCon.height/5, daCon.width/5, daCon.height/5), daCon, border);
	}


}