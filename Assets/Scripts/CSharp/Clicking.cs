using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicking : MonoBehaviour {

    private MatchMngr myMngr;
	void Awake ()
    {
        myMngr = GetComponent<MatchMngr>();
	}
	
	void Update ()
    {
        if (Input.GetButtonUp("Click") && myMngr.wePlaying)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray,out  hit, 100))
            {
                if (hit.transform.gameObject.name == "Cube0")
                {
                    myMngr.playerList[0] += 1;
                    if (myMngr.playerList[0] >= myMngr.range)
                    {
                        myMngr.playerList[0] = 0;
                    }
                    hit.transform.gameObject.GetComponent<Renderer>().material = myMngr.textures[myMngr.playerList[0]];
                }
                if (hit.transform.gameObject.name == "Cube1")
                {
                    myMngr.playerList[1] += 1;
                    if (myMngr.playerList[1] >= myMngr.range)
                    {
                        myMngr.playerList[1] = 0;
                    }
                    hit.transform.gameObject.GetComponent< Renderer > ().material = myMngr.textures[myMngr.playerList[1]];
                }
                if (hit.transform.gameObject.name == "Cube2")
                {
                    myMngr.playerList[2] += 1;
                    if (myMngr.playerList[2] >= myMngr.range)
                    {
                        myMngr.playerList[2] = 0;
                    }
                    hit.transform.gameObject.GetComponent< Renderer > ().material = myMngr.textures[myMngr.playerList[2]];
                }
                if (hit.transform.gameObject.name == "Cube3")
                {
                    myMngr.playerList[3] += 1;
                    if (myMngr.playerList[3] >= myMngr.range)
                    {
                        myMngr.playerList[3] = 0;
                    }
                    hit.transform.gameObject.GetComponent< Renderer > ().material = myMngr.textures[myMngr.playerList[3]];
                }
                if (hit.transform.gameObject.name == "Cube4")
                {
                    myMngr.playerList[4] += 1;
                    if (myMngr.playerList[4] >= myMngr.range)
                    {
                        myMngr.playerList[4] = 0;
                    }
                    hit.transform.gameObject.GetComponent< Renderer > ().material = myMngr.textures[myMngr.playerList[4]];
                }
                if (hit.transform.gameObject.name == "Cube5")
                {
                    myMngr.playerList[5] += 1;
                    if (myMngr.playerList[5] >= myMngr.range)
                    {
                        myMngr.playerList[5] = 0;
                    }
                    hit.transform.gameObject.GetComponent< Renderer > ().material = myMngr.textures[myMngr.playerList[5]];
                }
                if (hit.transform.gameObject.name == "Cube6")
                {
                    myMngr.playerList[6] += 1;
                    if (myMngr.playerList[6] >= myMngr.range)
                    {
                        myMngr.playerList[6] = 0;
                    }
                    hit.transform.gameObject.GetComponent< Renderer > ().material = myMngr.textures[myMngr.playerList[6]];
                }
                if (hit.transform.gameObject.name == "Cube7")
                {
                    myMngr.playerList[7] += 1;
                    if (myMngr.playerList[7] >= myMngr.range)
                    {
                        myMngr.playerList[7] = 0;
                    }
                    hit.transform.gameObject.GetComponent< Renderer > ().material = myMngr.textures[myMngr.playerList[7]];
                }
                if (hit.transform.gameObject.name == "Cube8")
                {
                    myMngr.playerList[8] += 1;
                    if (myMngr.playerList[8] >= myMngr.range)
                    {
                        myMngr.playerList[8] = 0;
                    }
                    hit.transform.gameObject.GetComponent< Renderer > ().material = myMngr.textures[myMngr.playerList[8]];
                }
            }
        }
    }
}
