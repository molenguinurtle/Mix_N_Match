using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public bool started, gameStarted, show, customizing, viewScores, quickGame;
    float t = 00;
    public Texture2D marioPic;
    public Texture2D sonicPic;
    public Texture2D sfPic;
    public Texture2D pokPic;
    public Texture2D mrIcon;
    public Texture2D sfIcon;
    public Texture2D snIcon;
    public Texture2D pkIcon;
    private int a = 0;
    private int b = 0;
    private int c = 0;
    private int d = 0;
    private int e = 0;
    private int f = 0;
    private int[] highScores30;
    private int[] highScores45;
    private int[] highScores60;
    private int[] highScores99;
    private string[] highNames30;
    private string[] highNames45;
    private string[] highNames60;
    private string[] highNames99;

    public GUIStyle myButton;
    public  GUIStyle myText;
    public GUIStyle myLogo;
    public GUIStyle myLevel;
    public GUIStyle myIcon;

    public string[] range;
    public string[] suites;
    public string[] levels;
    public string[] camPositions;
    public string[] timers;
    public string[] scores;
    public  GameObject[] titleBoxes;
    public Material[] titleTxtrs;
    public string currentSuite;
    public string currentRange;
    public string currentLevel;
    public string currentTimer;
    public string currentPosition;
    public string currentScore;

    void Start ()
    {
        DontDestroyOnLoad(this);
        currentSuite = suites[a];
        currentRange = range[b];
        currentLevel = levels[c];
        currentTimer = timers[d];
        currentPosition = camPositions[e];


        highScores30 = PlayerPrefsX.GetIntArray("HighScores30", 1200, 5);
        highScores45 = PlayerPrefsX.GetIntArray("HighScores45", 1200, 5);
        highScores60 = PlayerPrefsX.GetIntArray("HighScores60", 1200, 5);
        highScores99 = PlayerPrefsX.GetIntArray("HighScores99", 1200, 5);

        highNames30 = PlayerPrefsX.GetStringArray("HighNames30", "SLY MR 2", 5);
        highNames45 = PlayerPrefsX.GetStringArray("HighNames45", "SLY SF 3", 5);
        highNames60 = PlayerPrefsX.GetStringArray("HighNames60", "SLY SN 4", 5);
        highNames99 = PlayerPrefsX.GetStringArray("HighNames99", "SLY PK 2", 5);
    }
	
	void Update ()
    {
        if (!started && !gameStarted)
        {
            t += Time.deltaTime;
            if (t >= .75 && show)
            {
                show = false;
                t = 00;
            }
            if (t >= .75 && !show)
            {
                show = true;
                t = 00;
            }
            if (Input.GetButtonUp("Click"))
            {
			    foreach (var boxScore in titleBoxes)
                {
                    if (boxScore.name == "M")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(0, 2)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "I")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(2, 4)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "X")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(4, 6)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "N")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(6, 8)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "A")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(8, 10)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "T")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(10, 12)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "C")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(12, 14)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "H")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(14, 16)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                }
                started = true;
                show = false;
                t = 00;
            }
        }
    }
    void OnGUI()
    {
        if (show)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "LEFT CLICK TO BEGIN", myText);
        }
        if (started && !customizing && !gameStarted && !viewScores)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height - Screen.height / 11 * 4, 300, 60), "Quick Game", myButton))
            {
                quickGame = true;
                gameStarted = true;
                SceneManager.LoadScene("Match101");
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height - Screen.height / 11 * 3, 250, 60), "Customize", myButton))
            {
                foreach (var boxScore in titleBoxes)
                {
                    boxScore.GetComponent<Renderer>().enabled = false;
                }
                customizing = true;
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height - Screen.height / 11 * 2, 250, 60), "High Scores", myButton))
            {
                foreach (var boxScore in titleBoxes)
                {
                    boxScore.GetComponent<Renderer>().enabled = false;
                }
                viewScores = true;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height - Screen.height / 11, 250, 60), "Exit Game", myButton))
            {
                Application.Quit();
            }

        }
        if (started && viewScores && !gameStarted)
        {
            int z;
            int i;
            currentScore = scores[f];

            GUI.Label(new Rect(Screen.width / 2 - 60, Screen.height / 20, 120, 100), "HIGH SCORES", myText);

            GUI.Label(new Rect(Screen.width / 2 - 60, 100, 120, 100), currentScore, myText);

            GUI.Label(new Rect(Screen.width / 4, 150, 50, 100), "RANK", myText);
            GUI.Label(new Rect(Screen.width / 5 * 2, 150, 50, 100), "NAME", myText);
            GUI.Label(new Rect(Screen.width / 5 * 3, 150, 50, 100), "SCORE", myText);
            GUI.Label(new Rect(Screen.width / 5 * 4, 150, 50, 100), "SUITE", myText);


            if (GUI.Button(new Rect(Screen.width / 10, Screen.height / 20, 150, 45), "Back", myButton))
            {
                foreach (var boxScore in titleBoxes)
                {
                    if (boxScore.name == "M")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(0, 2)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "I")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(2, 4)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "X")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(4, 6)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "N")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(6, 8)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "A")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(8, 10)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "T")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(10, 12)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "C")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(12, 14)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "H")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(14, 16)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                }
                viewScores = false;
            }

            if (GUI.Button(new Rect(Screen.width / 10, Screen.height / 2, 20, 20), "<", myButton))
            {
                f -= 1;
                if (f < 0)
                {
                    f = scores.Length - 1;
                }
            }
            if (GUI.Button(new Rect(Screen.width - Screen.width / 10, Screen.height / 2, 20, 20), ">", myButton))
            {
                f += 1;
                if (f > scores.Length - 1)
                {
                    f = 0;
                }
            }
            if (currentScore == "30 Seconds")
            {
                //Rank
                for (i = 1; i < 6; i++)
                {
                    GUI.Label(new Rect(Screen.width / 4, 150 + 50 * i, 50, 100), i.ToString(), myText);
                }
                //Name
                for (i = 0; i < 5; i++)
                {
                    var namArr30 = highNames30[i].Split(" "[0]);
                    GUI.Label(new Rect(Screen.width / 5 * 2, 200 + 50 * i, 50, 100), namArr30[0], myText);
                }
                //Score
                for (i = 0; i < 5; i++)
                {
                    GUI.Label(new Rect(Screen.width / 5 * 3, 200 + 50 * i, 50, 100), highScores30[i].ToString(), myText);
                }
                //Suite
                for (i = 0; i < 5; i++)
                {
                    var sutArr30 = highNames30[i].Split(" "[0]);
                    var sutRan30 = int.Parse(sutArr30[2]);
                    if (sutArr30[1] == "SF")
                    {
                        for (z = 0; z < sutRan30; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (sfIcon.width / 5 * z), 200 + 50 * i, sfIcon.width / 5, sfIcon.height / 5), sfIcon, myIcon);
                        }
                    }
                    else if (sutArr30[1] == "MR")
                    {
                        for (z = 0; z < sutRan30; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (mrIcon.width / 5 * z), 200 + 50 * i, mrIcon.width / 5, mrIcon.height / 5), mrIcon, myIcon);
                        }
                    }
                    else if (sutArr30[1] == "SN")
                    {
                        for (z = 0; z < sutRan30; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (snIcon.width / 5 * z), 200 + 50 * i, snIcon.width / 5, snIcon.height / 5), snIcon, myIcon);
                        }
                    }
                    else if (sutArr30[1] == "PK")
                    {
                        for (z = 0; z < sutRan30; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (pkIcon.width / 5 * z), 200 + 50 * i, pkIcon.width / 5, pkIcon.height / 5), pkIcon, myIcon);
                        }
                    }
                }
            }
            else if (currentScore == "45 Seconds")
            {
                //Rank
                for (i = 1; i < 6; i++)
                {
                    GUI.Label(new Rect(Screen.width / 4, 150 + 50 * i, 50, 100), i.ToString(), myText);
                }
                //Name
                for (i = 0; i < 5; i++)
                {
                    var namArr45 = highNames45[i].Split(" "[0]);
                    GUI.Label(new Rect(Screen.width / 5 * 2, 200 + 50 * i, 50, 100), namArr45[0], myText);
                }
                //Score
                for (i = 0; i < 5; i++)
                {
                    GUI.Label(new Rect(Screen.width / 5 * 3, 200 + 50 * i, 50, 100), highScores45[i].ToString(), myText);
                }
                //Suite
                for (i = 0; i < 5; i++)
                {
                    var sutArr45 = highNames45[i].Split(" "[0]);
                    var sutRan45 = int.Parse(sutArr45[2]);
                    if (sutArr45[1] == "SF")
                    {
                        for (z = 0; z < sutRan45; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (sfIcon.width / 5 * z), 200 + 50 * i, sfIcon.width / 5, sfIcon.height / 5), sfIcon, myIcon);
                        }
                    }
                    else if (sutArr45[1] == "MR")
                    {
                        for (z = 0; z < sutRan45; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (mrIcon.width / 5 * z), 200 + 50 * i, mrIcon.width / 5, mrIcon.height / 5), mrIcon, myIcon);
                        }
                    }
                    else if (sutArr45[1] == "SN")
                    {
                        for (z = 0; z < sutRan45; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (snIcon.width / 5 * z), 200 + 50 * i, snIcon.width / 5, snIcon.height / 5), snIcon, myIcon);
                        }
                    }
                    else if (sutArr45[1] == "PK")
                    {
                        for (z = 0; z < sutRan45; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (pkIcon.width / 5 * z), 200 + 50 * i, pkIcon.width / 5, pkIcon.height / 5), pkIcon, myIcon);
                        }
                    }
                }
            }
            else if (currentScore == "60 Seconds")
            {
                //Rank
                for (i = 1; i < 6; i++)
                {
                    GUI.Label(new Rect(Screen.width / 4, 150 + 50 * i, 50, 100), i.ToString(), myText);
                }
                //Name
                for (i = 0; i < 5; i++)
                {
                    var namArr60 = highNames60[i].Split(" "[0]);
                    GUI.Label(new Rect(Screen.width / 5 * 2, 200 + 50 * i, 50, 100), namArr60[0], myText);
                }
                //Score
                for (i = 0; i < 5; i++)
                {
                    GUI.Label(new Rect(Screen.width / 5 * 3, 200 + 50 * i, 50, 100), highScores60[i].ToString(), myText);
                }
                //Suite
                for (i = 0; i < 5; i++)
                {
                    var sutArr60 = highNames60[i].Split(" "[0]);
                    var sutRan60 = int.Parse(sutArr60[2]);
                    if (sutArr60[1] == "SF")
                    {
                        for (z = 0; z < sutRan60; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (sfIcon.width / 5 * z), 200 + 50 * i, sfIcon.width / 5, sfIcon.height / 5), sfIcon, myIcon);
                        }
                    }
                    else if (sutArr60[1] == "MR")
                    {
                        for (z = 0; z < sutRan60; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (mrIcon.width / 5 * z), 200 + 50 * i, mrIcon.width / 5, mrIcon.height / 5), mrIcon, myIcon);
                        }
                    }
                    else if (sutArr60[1] == "SN")
                    {
                        for (z = 0; z < sutRan60; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (snIcon.width / 5 * z), 200 + 50 * i, snIcon.width / 5, snIcon.height / 5), snIcon, myIcon);
                        }
                    }
                    else if (sutArr60[1] == "PK")
                    {
                        for (z = 0; z < sutRan60; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (pkIcon.width / 5 * z), 200 + 50 * i, pkIcon.width / 5, pkIcon.height / 5), pkIcon, myIcon);
                        }
                    }
                }
            }
            else if (currentScore == "99 Seconds")
            {
                //Rank
                for (i = 1; i < 6; i++)
                {
                    GUI.Label(new Rect(Screen.width / 4, 150 + 50 * i, 50, 100), i.ToString(), myText);
                }
                //Name
                for (i = 0; i < 5; i++)
                {
                    var namArr99 = highNames99[i].Split(" "[0]);
                    GUI.Label(new Rect(Screen.width / 5 * 2, 200 + 50 * i, 50, 100), namArr99[0], myText);
                }
                //Score
                for (i = 0; i < 5; i++)
                {
                    GUI.Label(new Rect(Screen.width / 5 * 3, 200 + 50 * i, 50, 100), highScores99[i].ToString(), myText);
                }
                //Suite
                for (i = 0; i < 5; i++)
                {
                    var sutArr99 = highNames99[i].Split(" "[0]);
                    var sutRan99 = int.Parse(sutArr99[2]);
                    if (sutArr99[1] == "SF")
                    {
                        for (z = 0; z < sutRan99; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (sfIcon.width / 5 * z), 200 + 50 * i, sfIcon.width / 5, sfIcon.height / 5), sfIcon, myIcon);
                        }
                    }
                    else if (sutArr99[1] == "MR")
                    {
                        for (z = 0; z < sutRan99; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (mrIcon.width / 5 * z), 200 + 50 * i, mrIcon.width / 5, mrIcon.height / 5), mrIcon, myIcon);
                        }
                    }
                    else if (sutArr99[1] == "SN")
                    {
                        for (z = 0; z < sutRan99; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (snIcon.width / 5 * z), 200 + 50 * i, snIcon.width / 5, snIcon.height / 5), snIcon, myIcon);
                        }
                    }
                    else if (sutArr99[1] == "PK")
                    {
                        for (z = 0; z < sutRan99; z++)
                        {
                            GUI.Label(new Rect(Screen.width / 5 * 3.9f + (pkIcon.width / 5 * z), 200 + 50 * i, pkIcon.width / 5, pkIcon.height / 5), pkIcon, myIcon);
                        }
                    }
                }
            }

        }

        //Customizing the Game
        if (started && customizing && !gameStarted)
        {
            GUI.Label(new Rect(Screen.width / 2 - 60, Screen.height / 20, 220, 100), "CUSTOMIZE GAME", myText);

            if (GUI.Button(new Rect(Screen.width / 10, Screen.height / 20, 150, 45), "Back", myButton))
            {
                foreach (var boxScore in titleBoxes)
                {
                    if (boxScore.name == "M")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(0, 2)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "I")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(2, 4)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "X")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(4, 6)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "N")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(6, 8)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "A")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(8, 10)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "T")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(10, 12)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "C")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(12, 14)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                    else if (boxScore.name == "H")
                    {
                        boxScore.GetComponent<Renderer>().material = titleTxtrs[Random.Range(14, 16)];
                        boxScore.GetComponent<Renderer>().enabled = true;
                    }
                }
                customizing = false;
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 240, Screen.height - Screen.height / 20 * 4, 480, Screen.height / 20 * 1.5f), "SAVE AS QUICK GAME SETTINGS", myButton))
            {
                PlayerPrefs.SetString("QuickSuite", currentSuite);
                PlayerPrefs.SetString("QuickRange", currentRange);
                PlayerPrefs.SetString("QuickLvl", currentLevel);
                PlayerPrefs.SetString("QuickTmr", currentTimer);
                PlayerPrefs.SetString("QuickPos", currentPosition);
            }

            // Customizing the suite
            GUI.Label(new Rect(Screen.width / 3 - 60, Screen.height / 15 * 2, 120, 100), "SUITE:", myText);
            if (currentSuite == "Random")
            {
                GUI.Label(new Rect(Screen.width / 3 + 20, Screen.height / 15 * 2, 120, 100), currentSuite, myText);
            }
            else if (currentSuite != "Random")
            {
                GUI.Label(new Rect(Screen.width / 3 + 20, Screen.height / 15, 120, 100), currentSuite, myLogo);
            }
            if (GUI.Button(new Rect(Screen.width / 3 - 10, Screen.height / 15 * 2, 20, 20), "<", myButton))
            {
                a -= 1;
                if (a < 0)
                {
                    a = suites.Length - 1;
                }
                currentSuite = suites[a];
                if (currentSuite == "Mario")
                {
                    myLogo.normal.background = marioPic;
                }
                else if (currentSuite == "Sonic")
                {
                    myLogo.normal.background = sonicPic;
                }
                else if (currentSuite == "Street Fighter")
                {
                    myLogo.normal.background = sfPic;
                }
                else if (currentSuite == "Pokemon")
                {
                    myLogo.normal.background = pokPic;
                }

            }
            if (GUI.Button(new Rect(Screen.width / 3 + 160, Screen.height / 15 * 2, 20, 20), ">", myButton))
            {
                a += 1;
                if (a > suites.Length - 1)
                {
                    a = 0;
                }
                currentSuite = suites[a];
                if (currentSuite == "Mario")
                {
                    myLogo.normal.background = marioPic;
                }
                else if (currentSuite == "Sonic")
                {
                    myLogo.normal.background = sonicPic;
                }
                else if (currentSuite == "Street Fighter")
                {
                    myLogo.normal.background = sfPic;
                }
                else if (currentSuite == "Pokemon")
                {
                    myLogo.normal.background = pokPic;
                }

            }

            // Customizing the range
            GUI.Label(new Rect(Screen.width / 3 - 60, Screen.height / 15 * 4.5f, 120, 100), "RANGE:", myText);
            GUI.Label(new Rect(Screen.width / 3 + 40, Screen.height / 15 * 4.5f, 120, 100), currentRange, myText);
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height / 15 * 4.5f, 20, 20), "<", myButton))
            {
                b -= 1;
                if (b < 0)
                {
                    b = range.Length - 1;
                }
                currentRange = range[b];
            }
            if (GUI.Button(new Rect(Screen.width / 3 + 160, Screen.height / 15 * 4.5f, 20, 20), ">", myButton))
            {
                b += 1;
                if (b > range.Length - 1)
                {
                    b = 0;
                }
                currentRange = range[b];
            }

            // Customizing the level
            GUI.Label(new Rect(Screen.width / 3 - 60, Screen.height / 15 * 6.5f, 120, 100), "BACKGROUND:", myText);
            GUI.Label(new Rect(Screen.width / 3 + 80, Screen.height / 15 * 6.5f, 120, 100), currentLevel, myLevel);
            if (GUI.Button(new Rect(Screen.width / 3 + 55, Screen.height / 15 * 6.5f, 20, 20), "<", myButton))
            {
                c -= 1;
                if (c < 0)
                {
                    c = levels.Length - 1;
                }
                currentLevel = levels[c];
                if (currentLevel == "Red")
                {
                    myLevel.normal.textColor = Color.red;
                }
                else if (currentLevel == "Green")
                {
                    myLevel.normal.textColor = Color.green;
                }
                else if (currentLevel == "Blue")
                {
                    myLevel.normal.textColor = Color.blue;
                }
                else if (currentLevel == "White")
                {
                    myLevel.normal.textColor = Color.white;
                }
                else if (currentLevel == "Grey")
                {
                    myLevel.normal.textColor = Color.grey;
                }
                else if (currentLevel == "Yellow")
                {
                    myLevel.normal.textColor = Color.yellow;
                }
                else if (currentLevel == "Orange")
                {
                    myLevel.normal.textColor = new Color(1.0f, .55f, 0);
                }
                else if (currentLevel == "Purple")
                {
                    myLevel.normal.textColor = new Color(.3f, 0, .65f);
                }
                else if (currentLevel == "Random")
                {
                    myLevel.normal.textColor = Color.black;
                }
            }
            if (GUI.Button(new Rect(Screen.width / 3 + 160, Screen.height / 15 * 6.5f, 20, 20), ">", myButton))
            {
                c += 1;
                if (c > levels.Length - 1)
                {
                    c = 0;
                }
                currentLevel = levels[c];
                if (currentLevel == "Red")
                {
                    myLevel.normal.textColor = Color.red;
                }
                else if (currentLevel == "Green")
                {
                    myLevel.normal.textColor = Color.green;
                }
                else if (currentLevel == "Blue")
                {
                    myLevel.normal.textColor = Color.blue;
                }
                else if (currentLevel == "White")
                {
                    myLevel.normal.textColor = Color.white;
                }
                else if (currentLevel == "Grey")
                {
                    myLevel.normal.textColor = Color.grey;
                }
                else if (currentLevel == "Yellow")
                {
                    myLevel.normal.textColor = Color.yellow;
                }
                else if (currentLevel == "Orange")
                {
                    myLevel.normal.textColor = new Color(1.0f, .55f, 0);
                }
                else if (currentLevel == "Purple")
                {
                    myLevel.normal.textColor = new Color(.3f, 0, .65f);
                }
                else if (currentLevel == "Random")
                {
                    myLevel.normal.textColor = Color.black;
                }
            }

            // Customizing the timer
            GUI.Label(new Rect(Screen.width / 3 - 60, Screen.height / 15 * 8.5f, 120, 100), "TIMER:", myText);
            GUI.Label(new Rect(Screen.width / 3 + 25, Screen.height / 15 * 8.5f, 120, 100), currentTimer, myText);
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height / 15 * 8.5f, 20, 20), "<", myButton))
            {
                d -= 1;
                if (d < 0)
                {
                    d = timers.Length - 1;
                }
                currentTimer = timers[d];
            }
            if (GUI.Button(new Rect(Screen.width / 3 + 120, Screen.height / 15 * 8.5f, 20, 20), ">", myButton))
            {
                d += 1;
                if (d > timers.Length - 1)
                {
                    d = 0;
                }
                currentTimer = timers[d];
            }

            // Customizing the Mix Camera position
            GUI.Label(new Rect(Screen.width / 3 - 60, Screen.height / 15 * 10.5f, 200, 100), "MIX CAM POSITION:", myText);
            GUI.Label(new Rect(Screen.width / 3 + 150, Screen.height / 15 * 10.5f, 120, 100), currentPosition, myText);
            if (GUI.Button(new Rect(Screen.width / 3 + 120, Screen.height / 15 * 10.5f, 20, 20), "<", myButton))
            {
                e -= 1;
                if (e < 0)
                {
                    e = camPositions.Length - 1;
                }
                currentPosition = camPositions[e];
            }
            if (GUI.Button(new Rect(Screen.width / 3 + 280, Screen.height / 15 * 10.5f, 20, 20), ">", myButton))
            {
                e += 1;
                if (e > camPositions.Length - 1)
                {
                    e = 0;
                }
                currentPosition = camPositions[e];
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height - Screen.height / 20 * 2, 180, Screen.height / 20 * 1.5f), "Start Game", myButton))
            {
                quickGame = false;
                customizing = false;
                gameStarted = true;
                SceneManager.LoadScene("Match101");
            }
        }
    }

}
