using System;
using UnityEngine;

public class Clicking : MonoBehaviour
{
    private MatchMngr myMngr;
	void Awake ()
    {
        myMngr = GetComponent<MatchMngr>();
	}
	
	void Update ()
    {
        if (Input.GetButtonUp("Click") && myMngr.wePlaying)
        {
            ClickPic();
        }
    }

    private void ClickPic()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, 100))
        {
            var hitName = hit.transform.gameObject.name;
            var hitNumber = 0;
            Int32.TryParse(hitName[hitName.Length - 1].ToString(), out hitNumber);
            myMngr.playerList[hitNumber] += 1;
            if (myMngr.playerList[hitNumber] >= myMngr.range)
            {
                myMngr.playerList[hitNumber] = 0;
            }
            hit.transform.gameObject.GetComponent<Renderer>().material = myMngr.textures[myMngr.playerList[hitNumber]];
        }
    }
}
