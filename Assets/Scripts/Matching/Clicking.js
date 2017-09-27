#pragma strict

private var daMngr: MatchMngr;

function Awake()
{
	daMngr = GetComponent(MatchMngr);
}

function Update () 
{
	if (Input.GetButtonUp ("Click") && daMngr.wePlaying)
	{
		var ray : Ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		var hit: RaycastHit;
		if (Physics.Raycast (ray, hit, 100))
		{
			if (hit.transform.gameObject.name == "Cube0")
			{
				daMngr.playerList[0] +=1;
				if (daMngr.playerList[0] >= daMngr.range)
				{
					daMngr.playerList[0] =0;
				}
				hit.transform.gameObject.GetComponent.<Renderer>().material= daMngr.textures[daMngr.playerList[0]];
			}
			if (hit.transform.gameObject.name == "Cube1")
			{
				daMngr.playerList[1] +=1;
				if (daMngr.playerList[1] >= daMngr.range)
				{
					daMngr.playerList[1] =0;
				}
				hit.transform.gameObject.GetComponent.<Renderer>().material= daMngr.textures[daMngr.playerList[1]];
			}
			if (hit.transform.gameObject.name == "Cube2")
			{
				daMngr.playerList[2] +=1;
				if (daMngr.playerList[2] >= daMngr.range)
				{
					daMngr.playerList[2] =0;
				}
				hit.transform.gameObject.GetComponent.<Renderer>().material= daMngr.textures[daMngr.playerList[2]];
			}
			if (hit.transform.gameObject.name == "Cube3")
			{
				daMngr.playerList[3] +=1;
				if (daMngr.playerList[3] >= daMngr.range)
				{
					daMngr.playerList[3] =0;
				}
				hit.transform.gameObject.GetComponent.<Renderer>().material= daMngr.textures[daMngr.playerList[3]];
			}
			if (hit.transform.gameObject.name == "Cube4")
			{
				daMngr.playerList[4] +=1;
				if (daMngr.playerList[4] >= daMngr.range)
				{
					daMngr.playerList[4] =0;
				}
				hit.transform.gameObject.GetComponent.<Renderer>().material= daMngr.textures[daMngr.playerList[4]];
			}
			if (hit.transform.gameObject.name == "Cube5")
			{
				daMngr.playerList[5] +=1;
				if (daMngr.playerList[5] >= daMngr.range)
				{
					daMngr.playerList[5] =0;
				}
				hit.transform.gameObject.GetComponent.<Renderer>().material= daMngr.textures[daMngr.playerList[5]];
			}
			if (hit.transform.gameObject.name == "Cube6")
			{
				daMngr.playerList[6] +=1;
				if (daMngr.playerList[6] >= daMngr.range)
				{
					daMngr.playerList[6] =0;
				}
				hit.transform.gameObject.GetComponent.<Renderer>().material= daMngr.textures[daMngr.playerList[6]];
			}
			if (hit.transform.gameObject.name == "Cube7")
			{
				daMngr.playerList[7] +=1;
				if (daMngr.playerList[7] >= daMngr.range)
				{
					daMngr.playerList[7] =0;
				}
				hit.transform.gameObject.GetComponent.<Renderer>().material= daMngr.textures[daMngr.playerList[7]];
			}
			if (hit.transform.gameObject.name == "Cube8")
			{
				daMngr.playerList[8] +=1;
				if (daMngr.playerList[8] >= daMngr.range)
				{
					daMngr.playerList[8] =0;
				}
				hit.transform.gameObject.GetComponent.<Renderer>().material= daMngr.textures[daMngr.playerList[8]];
			}
		}
	}
}