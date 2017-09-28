using UnityEngine;

public class MixCamGUI : MonoBehaviour
{
    public Texture2D[] icons;
    public string daSuite;
    public string camPos;
    public GUIStyle border;

    void OnGUI()
    {
        int z;
        Texture2D daCon = new Texture2D(0,0);
        switch(daSuite)
        {
            case "Mario":
                daCon = icons[0];
                break;
            case "Sonic":
                daCon = icons[1];
                break;
            case "Pokemon":
                daCon = icons[2];
                break;
            case "Street Fighter":
                daCon = icons[3];
                break;
            default:
                break;
        }

        if (camPos == "Bottom Right")
        {
            //***Bottom Right***
            //Horizontal edges
            for (z = 0; z < (Screen.width / 4) / (daCon.width / 5) - 1; z++)
            {
                GUI.Label(new Rect(Screen.width / 4 * 3 + (daCon.width / 5 * z) + daCon.width / 5, Screen.height - daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
                GUI.Label(new Rect(Screen.width / 4 * 3 + (daCon.width / 5 * z) + daCon.width / 5, Screen.height / 2, daCon.width / 5, daCon.height / 5), daCon, border);
            }
            //Vertical edges
            for (z = 0; z < (Screen.height / 2) / (daCon.height / 5) - 1; z++)
            {
                GUI.Label(new Rect(Screen.width / 4 * 3, Screen.height / 2 + (daCon.height / 5 * z) + daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
                GUI.Label(new Rect(Screen.width - daCon.width / 5, Screen.height / 2 + (daCon.height / 5 * z) + daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
            }
            //Corners
            GUI.Label(new Rect(Screen.width / 4 * 3, Screen.height - daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
            GUI.Label(new Rect(Screen.width / 4 * 3, Screen.height / 2, daCon.width / 5, daCon.height / 5), daCon, border);
            GUI.Label(new Rect(Screen.width - daCon.width / 5, Screen.height - daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
            GUI.Label(new Rect(Screen.width - daCon.width / 5, Screen.height / 2, daCon.width / 5, daCon.height / 5), daCon, border);
        }
        else if (camPos == "Top Right")
        {
            //Horizontal edges
            for (z = 0; z < (Screen.width / 4) / (daCon.width / 5) - 1; z++)
            {
                GUI.Label(new Rect(Screen.width / 4 * 3 + (daCon.width / 5 * z) + daCon.width / 5, 0, daCon.width / 5, daCon.height / 5), daCon, border);
                GUI.Label(new Rect(Screen.width / 4 * 3 + (daCon.width / 5 * z) + daCon.width / 5, Screen.height / 2 - daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
            }
            //Vertical edges
            for (z = 0; z < (Screen.height / 2) / (daCon.height / 5) - 1; z++)
            {
                GUI.Label(new Rect(Screen.width / 4 * 3, (daCon.height / 5 * z) + daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
                GUI.Label(new Rect(Screen.width - daCon.width / 5, (daCon.height / 5 * z) + daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
            }
            //Corners
            GUI.Label(new Rect(Screen.width / 4 * 3, 0, daCon.width / 5, daCon.height / 5), daCon, border);
            GUI.Label(new Rect(Screen.width / 4 * 3, Screen.height / 2 - daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
            GUI.Label(new Rect(Screen.width - daCon.width / 5, 0, daCon.width / 5, daCon.height / 5), daCon, border);
            GUI.Label(new Rect(Screen.width - daCon.width / 5, Screen.height / 2 - daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
        }
        else if (camPos == "Top Left")
        {
            //Horizontal edges
            for (z = 0; z < (Screen.width / 4) / (daCon.width / 5) - 1; z++)
            {
                GUI.Label(new Rect((daCon.width / 5 * z) + daCon.width / 5, 0, daCon.width / 5, daCon.height / 5), daCon, border);
                GUI.Label(new Rect((daCon.width / 5 * z) + daCon.width / 5, Screen.height / 2 - daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
            }
            //Vertical edges
            for (z = 0; z < (Screen.height / 2) / (daCon.height / 5) - 1; z++)
            {
                GUI.Label(new Rect(0, (daCon.height / 5 * z) + daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
                GUI.Label(new Rect(Screen.width / 4 - daCon.width / 5, (daCon.height / 5 * z) + daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
            }
            //Corners
            GUI.Label(new Rect(0, 0, daCon.width / 5, daCon.height / 5), daCon, border);
            GUI.Label(new Rect(Screen.width / 4 - daCon.width / 5, Screen.height / 2 - daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
            GUI.Label(new Rect(Screen.width / 4 - daCon.width / 5, 0, daCon.width / 5, daCon.height / 5), daCon, border);
            GUI.Label(new Rect(0, Screen.height / 2 - daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
        }
        else if (camPos == "Bottom Left")
        {
            //Horizontal edges
            for (z = 0; z < (Screen.width / 4) / (daCon.width / 5) - 1; z++)
            {
                GUI.Label(new Rect((daCon.width / 5 * z) + daCon.width / 5, Screen.height - daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
                GUI.Label(new Rect((daCon.width / 5 * z) + daCon.width / 5, Screen.height / 2, daCon.width / 5, daCon.height / 5), daCon, border);
            }
            //Vertical edges
            for (z = 0; z < (Screen.height / 2) / (daCon.height / 5) - 1; z++)
            {
                GUI.Label(new Rect(0, Screen.height / 2 + (daCon.height / 5 * z) + daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
                GUI.Label(new Rect(Screen.width / 4 - daCon.width / 5, Screen.height / 2 + (daCon.height / 5 * z) + daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
            }
            //Corners
            GUI.Label(new Rect(0, Screen.height / 2, daCon.width / 5, daCon.height / 5), daCon, border);
            GUI.Label(new Rect(Screen.width / 4 - daCon.width / 5, Screen.height / 2, daCon.width / 5, daCon.height / 5), daCon, border);
            GUI.Label(new Rect(Screen.width / 4 - daCon.width / 5, Screen.height - daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
            GUI.Label(new Rect(0, Screen.height - daCon.height / 5, daCon.width / 5, daCon.height / 5), daCon, border);
        }


    }
}
