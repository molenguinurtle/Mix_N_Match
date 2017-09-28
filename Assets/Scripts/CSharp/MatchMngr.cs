using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MatchMngr : MonoBehaviour {
    public int score = 0;
    private float startTimer;
    public float timer;
    public float count = 5.00f;
    public int range;
    public GameObject[] playerCubes;
    public GameObject[] targetCubes;
    public Material[] catTextures;
    public Material[] flwrTextures;
    public Material[] hatTextures;
    public Material[] giftTextures;
    public Material[] textures;
    public int[] targetList;
    public int[] playerList;
    public AudioClip[] scoreSnds;
    public AudioClip[] sfMusic;
    public AudioClip[] hedMusic;
    public AudioClip[] marMusic;
    public  AudioClip[] pokMusic;
    public AudioClip[] countClips;
    public bool wePlaying = false;
    public bool needTimer = true;
    public bool gameOver = false;
    bool needReset = false;
    public Camera matchCam;
    public GUIStyle highTxt;

    private Camera playCam;
    private AudioClip scoreTune;
    private int[] highScores;
    private string[] highNames;
    private List<int> daHighs = new List<int>();
    private List<string> daNames = new List<string>();
    private Dictionary<int, string> daHighNames = new Dictionary< int, string>();
	private string daLevel;
    private string daSuite;
    private bool needStart = true;
    private bool playedOne = false;
    private bool playedTwo = false;
    private bool playedThree = false;
    private bool playedGo = false;
    private bool needNewHigh = false;
    private bool needName = false;
    private MainMenu lesOptions;
    private string newName = "AAA";
    // Use this for initialization
    void Start ()
    {
        lesOptions = GameObject.FindGameObjectWithTag("Options").GetComponent<MainMenu>();
        highScores = new int[5];
        playCam = Camera.main;
        if (lesOptions.quickGame)
        {
            //setting the range based off saved/default options
            if (PlayerPrefs.GetString("QuickRange", "2") == "2")
            {
                range = 2;
            }
            else if (PlayerPrefs.GetString("QuickRange", "2") == "3")
            {
                range = 3;
            }
            else if (PlayerPrefs.GetString("QuickRange", "2") == "4")
            {
                range = 4;
            }
            else if (PlayerPrefs.GetString("QuickRange", "2") == "Random")
            {
                range = Random.Range(2, 5);
            }

            //setting the timer based off saved/default options
            if (PlayerPrefs.GetString("QuickTmr", "30") == "99")
            {
                needTimer = true;
                timer = 99.00f;
                highScores = PlayerPrefsX.GetIntArray("HighScores99", 1200, 5);
                highNames = PlayerPrefsX.GetStringArray("HighNames99", "SLY PK 2", 5);
            }
            else if (PlayerPrefs.GetString("QuickTmr", "30") == "60")
            {
                needTimer = true;
                timer = 60.00f;
                highScores = PlayerPrefsX.GetIntArray("HighScores60", 1200, 5);
                highNames = PlayerPrefsX.GetStringArray("HighNames60", "SLY SN 4", 5);
            }
            else if (PlayerPrefs.GetString("QuickTmr", "30") == "45")
            {
                needTimer = true;
                timer = 45.00f;
                highScores = PlayerPrefsX.GetIntArray("HighScores45", 1200, 5);
                highNames = PlayerPrefsX.GetStringArray("HighNames45", "SLY SF 3", 5);
            }
            else if (PlayerPrefs.GetString("QuickTmr", "30") == "30")
            {
                needTimer = true;
                timer = 30.00f;
                highScores = PlayerPrefsX.GetIntArray("HighScores30", 1200, 5);
                highNames = PlayerPrefsX.GetStringArray("HighNames30", "SLY MR 2", 5);

            }
            else if (PlayerPrefs.GetString("QuickTmr", "30") == "Unlimited")
            {
                needTimer = false;
                timer = 99.00f;
            }

            //moving the 'matchCam' based on saved/default options
            if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Right")
            {
                matchCam.rect = new Rect(.75f, 0, .25f, .5f);
            }
            else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Left")
            {
                matchCam.rect = new Rect(0, .5f, .25f, .5f);
            }
            else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Right")
            {
                matchCam.rect = new Rect(.75f, .5f, .25f, .5f);
            }
            else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Left")
            {
                matchCam.rect = new Rect(0, 0, .25f, .5f);
            }

            //Setting the level/background based off saved/default options
            daLevel = PlayerPrefs.GetString("QuickLvl", "Random");
            if (daLevel == "Random")
            {
                daLevel = lesOptions.levels[Random.Range(1, lesOptions.levels.Length)];
            }
            if (daLevel == "Red")
            {
                playCam.backgroundColor = Color.red;
                matchCam.backgroundColor = Color.red;
            }
            else if (daLevel == "Blue")
            {
                playCam.backgroundColor = Color.blue;
                matchCam.backgroundColor = Color.blue;
            }
            else if (daLevel == "Green")
            {
                playCam.backgroundColor = Color.green;
                matchCam.backgroundColor = Color.green;
            }
            else if (daLevel == "White")
            {
                playCam.backgroundColor = Color.white;
                matchCam.backgroundColor = Color.white;
            }
            else if (daLevel == "Grey")
            {
                playCam.backgroundColor = Color.grey;
                matchCam.backgroundColor = Color.grey;
            }
            else if (daLevel == "Yellow")
            {
                playCam.backgroundColor = Color.yellow;
                matchCam.backgroundColor = Color.yellow;
            }
            else if (daLevel == "Orange")
            {
                playCam.backgroundColor = new Color(1.0f, .55f, 0);
                matchCam.backgroundColor = new Color(1.0f, .55f, 0);
            }
            else if (daLevel == "Purple")
            {
                playCam.backgroundColor = new Color(.3f, 0, .65f);
                matchCam.backgroundColor = new Color(.3f, 0, .65f);
            }

            //Setting the Suite based off saved/default options
            daSuite = PlayerPrefs.GetString("QuickSuite", "Random");
            if (daSuite == "Random")
            {
                daSuite = lesOptions.suites[Random.Range(1, lesOptions.suites.Length)];
            }
            if (daSuite == "Street Fighter")
            {
                SF_Suite_SetUp();
            }
            else if (daSuite == "Mario")
            {
                Mario_Suite_SetUp();
            }
            else if (daSuite == "Sonic")
            {
                Sonic_Suite_SetUp();
            }
            else if (daSuite == "Pokemon")
            {
                Pokemon_Suite_SetUp();
            }
        }
        else if (!lesOptions.quickGame)
        {
            //setting the 'range' var based on customize game options
            if (lesOptions.currentRange == "2")
            {
                range = 2;
            }
            else if (lesOptions.currentRange == "3")
            {
                range = 3;
            }
            else if (lesOptions.currentRange == "4")
            {
                range = 4;
            }
            else if (lesOptions.currentRange == "Random")
            {
                range = Random.Range(2, 5);
            }

            //setting the 'timer' variable based on customize game options
            if (lesOptions.currentTimer == "99")
            {
                needTimer = true;
                timer = 99.00f;
                highScores = PlayerPrefsX.GetIntArray("HighScores99", 1200, 5);
                highNames = PlayerPrefsX.GetStringArray("HighNames99", "SLY PK 2", 5);
            }
            else if (lesOptions.currentTimer == "60")
            {
                needTimer = true;
                timer = 60.00f;
                highScores = PlayerPrefsX.GetIntArray("HighScores60", 1200, 5);
                highNames = PlayerPrefsX.GetStringArray("HighNames60", "SLY SN 4", 5);
            }
            else if (lesOptions.currentTimer == "45")
            {
                needTimer = true;
                timer = 45.00f;
                highScores = PlayerPrefsX.GetIntArray("HighScores45", 1200, 5);
                highNames = PlayerPrefsX.GetStringArray("HighNames45", "SLY SF 3", 5);
            }
            else if (lesOptions.currentTimer == "30")
            {
                needTimer = true;
                timer = 30.00f;
                highScores = PlayerPrefsX.GetIntArray("HighScores30", 1200, 5);
                highNames = PlayerPrefsX.GetStringArray("HighNames30", "SLY MR 2", 5);

            }
            else if (lesOptions.currentTimer == "Unlimited")
            {
                needTimer = false;
                timer = 99.00f;
            }

            //moving the 'matchCam' based on customize game options
            if (lesOptions.currentPosition == "Bottom Right")
            {
                matchCam.rect = new Rect(.75f, 0, .25f, .5f);
            }
            else if (lesOptions.currentPosition == "Top Left")
            {
                matchCam.rect = new Rect(0, .5f, .25f, .5f);
            }
            else if (lesOptions.currentPosition == "Top Right")
            {
                matchCam.rect = new Rect(.75f, .5f, .25f, .5f);
            }
            else if (lesOptions.currentPosition == "Bottom Left")
            {
                matchCam.rect = new Rect(0, 0, .25f, .5f);
            }

            //Setting up the background/level based on customize game options
            daLevel = lesOptions.currentLevel;
            if (daLevel == "Red")
            {
                playCam.backgroundColor = Color.red;
                matchCam.backgroundColor = Color.red;
            }
            else if (daLevel == "Blue")
            {
                playCam.backgroundColor = Color.blue;
                matchCam.backgroundColor = Color.blue;
            }
            else if (daLevel == "Green")
            {
                playCam.backgroundColor = Color.green;
                matchCam.backgroundColor = Color.green;
            }
            else if (daLevel == "White")
            {
                playCam.backgroundColor = Color.white;
                matchCam.backgroundColor = Color.white;
            }
            else if (daLevel == "Grey")
            {
                playCam.backgroundColor = Color.grey;
                matchCam.backgroundColor = Color.grey;
            }
            else if (daLevel == "Yellow")
            {
                playCam.backgroundColor = Color.yellow;
                matchCam.backgroundColor = Color.yellow;
            }
            else if (daLevel == "Orange")
            {
                playCam.backgroundColor = new Color(1.0f, .55f, 0);
                matchCam.backgroundColor = new Color(1.0f, .55f, 0);
            }
            else if (daLevel == "Purple")
            {
                playCam.backgroundColor = new Color(.3f, 0, .65f);
                matchCam.backgroundColor = new Color(.3f, 0, .65f);
            }
            else if (daLevel == "Random")
            {
                daLevel = lesOptions.levels[Random.Range(1, lesOptions.levels.Length)];
                if (daLevel == "Red")
                {
                    playCam.backgroundColor = Color.red;
                    matchCam.backgroundColor = Color.red;
                }
                else if (daLevel == "Blue")
                {
                    playCam.backgroundColor = Color.blue;
                    matchCam.backgroundColor = Color.blue;
                }
                else if (daLevel == "Green")
                {
                    playCam.backgroundColor = Color.green;
                    matchCam.backgroundColor = Color.green;
                }
                else if (daLevel == "White")
                {
                    playCam.backgroundColor = Color.white;
                    matchCam.backgroundColor = Color.white;
                }
                else if (daLevel == "Grey")
                {
                    playCam.backgroundColor = Color.grey;
                    matchCam.backgroundColor = Color.grey;
                }
                else if (daLevel == "Yellow")
                {
                    playCam.backgroundColor = Color.yellow;
                    matchCam.backgroundColor = Color.yellow;
                }
                else if (daLevel == "Orange")
                {
                    playCam.backgroundColor = new Color(1.0f, .55f, 0);
                    matchCam.backgroundColor = new Color(1.0f, .55f, 0);
                }
                else if (daLevel == "Purple")
                {
                    playCam.backgroundColor = new Color(.3f, 0, .65f);
                    matchCam.backgroundColor = new Color(.3f, 0, .65f);
                }
            }

            // Setting up the suite based on customize game options
            if (lesOptions.currentSuite == "Random")
            {
                daSuite = lesOptions.suites[Random.Range(1, lesOptions.suites.Length)];
            }
            if (lesOptions.currentSuite == "Street Fighter" || daSuite == "Street Fighter")
            {
                daSuite = "Street Fighter";
                SF_Suite_SetUp();
            }
            else if (lesOptions.currentSuite == "Mario" || daSuite == "Mario")
            {
                daSuite = "Mario";
                Mario_Suite_SetUp();
            }
            else if (lesOptions.currentSuite == "Sonic" || daSuite == "Sonic")
            {
                daSuite = "Sonic";
                Sonic_Suite_SetUp();
            }
            else if (lesOptions.currentSuite == "Pokemon" || daSuite == "Pokemon")
            {
                daSuite = "Pokemon";
                Pokemon_Suite_SetUp();
            }
            
        }
        
        //Setting up audio based on suite
        //C'est la vie. Need to come back and move all this stuff into helper methods for set up. And USE SWITCH statements
        if (daSuite == "Mario")
        {
            GetComponent<AudioSource>().clip = marMusic[Random.Range(0, marMusic.Length)];
            scoreTune = scoreSnds[0];
        }
        else if (daSuite == "Sonic")
        {
            GetComponent<AudioSource>().clip = hedMusic[Random.Range(0, hedMusic.Length)];
            scoreTune = scoreSnds[1];
        }
        else if (daSuite == "Street Fighter")
        {
            GetComponent<AudioSource>().clip = sfMusic[Random.Range(0, sfMusic.Length)];
            scoreTune = scoreSnds[2];
        }
        else if (daSuite == "Pokemon")
        {
            GetComponent<AudioSource>().clip = pokMusic[Random.Range(0, pokMusic.Length)];
            scoreTune = scoreSnds[3];
        }

        //Setting up mix camera border based on suite
        matchCam.GetComponent<MixCamGUI>().daSuite = daSuite;
        if (lesOptions.quickGame)
        {
            matchCam.GetComponent<MixCamGUI>().camPos = PlayerPrefs.GetString("QuickPos", "Bottom Right");
        }
        else if (lesOptions.quickGame == false)
        {
            matchCam.GetComponent<MixCamGUI>().camPos = lesOptions.currentPosition;
        }
        targetList = new int[9];
        playerList = new int[9];

        startTimer = timer;
        SetTargetPic();
    }

    // Update is called once per frame
    void Update()
    {
        if (needStart && !wePlaying) //We will count down from 3 to start the game
        {
            count -= Time.deltaTime;
            if (Mathf.RoundToInt(count) == 3 && !playedThree)
            {
                GetComponent<AudioSource>().PlayOneShot(countClips[0], 1.0f);
                playedThree = true;
            }
            else if (Mathf.RoundToInt(count) == 2 && !playedTwo)
            {
                GetComponent<AudioSource>().PlayOneShot(countClips[1], 1.0f);
                playedTwo = true;
            }
            else if (Mathf.RoundToInt(count) == 1 && !playedOne)
            {
                GetComponent<AudioSource>().PlayOneShot(countClips[2], 1.0f);
                playedOne = true;
            }
            else if (Mathf.RoundToInt(count) == 0 && !playedGo)
            {
                GetComponent<AudioSource>().PlayOneShot(countClips[3], 1.0f);
                playedGo = true;
            }
            else if (count <= 00)
            {
                GetComponent<AudioSource>().Play();
                wePlaying = true;
                needStart = false;
            }
        }
        if (wePlaying && needTimer) //Now that the game has started, start the timer
        {
            timer -= Time.deltaTime;
        }
        if (playerList.SequenceEqual(targetList) && wePlaying) //If the player pic matches the target pic...
        {
            score += 100 * range;
            GetComponent<AudioSource>().PlayOneShot(scoreTune, .7f);
            if (needTimer)
            {
                if (range == 2)
                {
                    timer += 1.0f;
                }
                else if (range == 3)
                {
                    timer += 2.0f;
                }
                else if (range == 4)
                {
                    timer += 3.0f;
                }
            }
            SetTargetPic();
        }
        if (timer <= 00 && wePlaying) //If we run out of time
        {
            GetComponent<AudioSource>().Stop();
            wePlaying = false;
            gameOver = true;
            foreach (var targ in targetCubes)
            {
                targ.GetComponent<Renderer>().enabled = false;
            }
            foreach (var play in playerCubes)
            {
                play.GetComponent<Renderer>().enabled = false;
            }
            foreach (var high in highScores)
            {
                if (score > high)
                {
                    needNewHigh = true;
                    needName = true;
                }
            }
        }
        if (needNewHigh && !needName)
        {
            int i;

            for (i = 0; i < 5; i++)
            {
                daHighNames.Add(highScores[i] + i + 1, highNames[i]);
            }
            if (daSuite == "Street Fighter")
            {
                daHighNames.Add(score, newName + " SF " + range.ToString());
            }
            else if (daSuite == "Mario")
            {
                daHighNames.Add(score, newName + " MR " + range.ToString());
            }
            else if (daSuite == "Sonic")
            {
                daHighNames.Add(score, newName + " SN " + range.ToString());
            }
            else if (daSuite == "Pokemon")
            {
                daHighNames.Add(score, newName + " PK " + range.ToString());
            }
            foreach (var num in daHighNames.Keys)
            {
                daHighs.Add(num);
            }
            daHighs.Sort();
            daHighs.Reverse();
            for (i = 0; i < 5; i++)
            {
                daNames.Add(daHighNames[daHighs[i]]);
            }
            daHighs.RemoveAt(highScores.Length);
            for (i = 0; i < 5; i++)
            {
                daHighs[i] = daHighs[i] - daHighs[i] % 10;
                if (daHighs[i] == 0)
                {
                    daHighs[i] += 100;
                }
            }
            highScores = daHighs.ToArray();
            highNames = daNames.ToArray();
            if (startTimer == 99.00f)
            {
                PlayerPrefsX.SetIntArray("HighScores99", highScores);
                PlayerPrefsX.SetStringArray("HighNames99", highNames);
            }
            else if (startTimer == 60.00f)
            {
                PlayerPrefsX.SetIntArray("HighScores60", highScores);
                PlayerPrefsX.SetStringArray("HighNames60", highNames);
            }
            else if (startTimer == 45.00f)
            {
                PlayerPrefsX.SetIntArray("HighScores45", highScores);
                PlayerPrefsX.SetStringArray("HighNames45", highNames);
            }
            else if (startTimer == 30.00f)
            {
                PlayerPrefsX.SetIntArray("HighScores30", highScores);
                PlayerPrefsX.SetStringArray("HighNames30", highNames);
            }
            needNewHigh = false;
        }
        if (needReset)
        {
            timer = startTimer;
            score = 0;
            count = 4.0f;

            SetTargetPic();

            foreach (var play in playerCubes)
            {
                play.GetComponent<Renderer>().material = textures[0];
            }
            for (int i =0;i<playerList.Length;i++)
            {
                playerList[i] = 0;
            }
            foreach (var targ in targetCubes)
            {
                targ.GetComponent<Renderer>().enabled = true;
            }
            foreach (var play in playerCubes)
            {
                play.GetComponent<Renderer>().enabled = true;
            }
            daHighs.Clear();
            daNames.Clear();
            daHighNames.Clear();
            playedOne = false;
            playedTwo = false;
            playedThree = false;
            playedGo = false;
            gameOver = false;
            needStart = true;
            needReset = false;
        }

    
    }
    private void SetTargetPic()
    {
        targetList[0] = Random.Range(0, range);
        targetCubes[0].GetComponent<Renderer>().material = textures[targetList[0]];

        targetList[1] = Random.Range(0, range);
        targetCubes[1].GetComponent<Renderer>().material = textures[targetList[1]];

        targetList[2] = Random.Range(0, range);
        targetCubes[2].GetComponent<Renderer>().material = textures[targetList[2]];

        targetList[3] = Random.Range(0, range);
        targetCubes[3].GetComponent<Renderer>().material = textures[targetList[3]];

        targetList[4] = Random.Range(0, range);
        targetCubes[4].GetComponent<Renderer>().material = textures[targetList[4]];

        targetList[5] = Random.Range(0, range);
        targetCubes[5].GetComponent<Renderer>().material = textures[targetList[5]];

        targetList[6] = Random.Range(0, range);
        targetCubes[6].GetComponent<Renderer>().material = textures[targetList[6]];

        targetList[7] = Random.Range(0, range);
        targetCubes[7].GetComponent<Renderer>().material = textures[targetList[7]];

        targetList[8] = Random.Range(0, range);
        targetCubes[8].GetComponent<Renderer>().material = textures[targetList[8]];

    }

    #region Suite Set Up Methods
    private void Pokemon_Suite_SetUp()
    {
        textures = flwrTextures;
        foreach (var targ in targetCubes)
        {
            targ.transform.localScale = new Vector3(.275f, .275f, .275f);
            targ.GetComponent<Renderer>().material = textures[0];
        }
        foreach (var play in playerCubes)
        {
            var pColp = play.GetComponent<BoxCollider>() as BoxCollider;
            play.transform.localScale = new Vector3(.275f, .275f, .275f);
            pColp.size = new Vector3(6, 4, 6);
            pColp.center = new Vector3(0, 0, 0);
            play.GetComponent<Renderer>().material = textures[0];
        }
    }

    private void Sonic_Suite_SetUp()
    {
        textures = hatTextures;
        foreach (var targ in targetCubes)
        {
            targ.transform.localScale = new Vector3(.23f, .23f, .23f);
            targ.GetComponent<Renderer>().material = textures[0];
        }
        foreach (var play in playerCubes)
        {
            var pCols = play.GetComponent<BoxCollider>() as BoxCollider;
            play.transform.localScale = new Vector3(.23f, .23f, .23f);
            pCols.size = new Vector3(7.5f, 4, 7.5f);
            pCols.center = new Vector3(0, 0, -1);
            play.GetComponent<Renderer>().material = textures[0];
        }
    }

    private void Mario_Suite_SetUp()
    {
        textures = giftTextures;
        foreach (var targ in targetCubes)
        {
            targ.transform.localScale = new Vector3(.4f, .4f, .4f);
            targ.GetComponent<Renderer>().material = textures[0];
        }
        foreach (var play in playerCubes)
        {
            var pColm = play.GetComponent<BoxCollider>() as BoxCollider;
            play.transform.localScale = new Vector3(.4f, .4f, .4f);
            pColm.size = new Vector3(4, 4, 4);
            pColm.center = new Vector3(0, 0, 0);
            play.GetComponent<Renderer>().material = textures[0];
        }
    }

    private void SF_Suite_SetUp()
    {
        textures = catTextures;
        foreach (var targ in targetCubes)
        {
            targ.transform.localScale = new Vector3(.275f, .275f, .275f);
            targ.GetComponent<Renderer>().material = textures[0];
        }
        foreach (var play in playerCubes)
        {
            var pColsf = play.GetComponent<BoxCollider>() as BoxCollider;
            play.transform.localScale = new Vector3(.275f, .275f, .275f);
            pColsf.size = new Vector3(6, 4, 6);
            pColsf.center = new Vector3(0, 0, -.5f);
            play.GetComponent<Renderer>().material = textures[0];
        }
    }
    #endregion

    void OnGUI()
    {
        if (needStart && !wePlaying)
        {
            if (count < 3.5f)
            {
                if (lesOptions.quickGame)
                {
                    if (Mathf.RoundToInt(count).ToString() != "0")
                    {
                        if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Right")
                        {
                            GUI.Label(new Rect(Screen.width - Screen.width / 10, Screen.height / 20 * 3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
                        }
                        else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Right")
                        {
                            GUI.Label(new Rect(Screen.width - Screen.width / 10, Screen.height - Screen.height / 20 * 3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
                        }
                        else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Left")
                        {
                            GUI.Label(new Rect(Screen.width / 10, Screen.height / 20 * 3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
                        }
                        else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Left")
                        {
                            GUI.Label(new Rect(Screen.width / 10, Screen.height - Screen.height / 20 * 3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
                        }
                    }
                    if (Mathf.RoundToInt(count).ToString() == "0")
                    {
                        if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Right")
                        {
                            GUI.Label(new Rect(Screen.width - Screen.width / 10, Screen.height / 20 * 3, 100, 100), "GO!", highTxt);
                        }
                        else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Right")
                        {
                            GUI.Label(new Rect(Screen.width - Screen.width / 10, Screen.height - Screen.height / 20 * 3, 100, 100), "GO!", highTxt);
                        }
                        else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Left")
                        {
                            GUI.Label(new Rect(Screen.width / 10, Screen.height / 20 * 3, 100, 100), "GO!", highTxt);
                        }
                        else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Left")
                        {
                            GUI.Label(new Rect(Screen.width / 10, Screen.height - Screen.height / 20 * 3, 100, 100), "GO!", highTxt);
                        }
                    }
                }
                else if (lesOptions.quickGame == false)
                {
                    if (Mathf.RoundToInt(count).ToString() != "0")
                    {
                        if (lesOptions.currentPosition == "Bottom Right")
                        {
                            GUI.Label(new Rect(Screen.width - Screen.width / 10, Screen.height / 20 * 3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
                        }
                        else if (lesOptions.currentPosition == "Top Right")
                        {
                            GUI.Label(new Rect(Screen.width - Screen.width / 10, Screen.height - Screen.height / 20 * 3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
                        }
                        else if (lesOptions.currentPosition == "Bottom Left")
                        {
                            GUI.Label(new Rect(Screen.width / 10, Screen.height / 20 * 3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
                        }
                        else if (lesOptions.currentPosition == "Top Left")
                        {
                            GUI.Label(new Rect(Screen.width / 10, Screen.height - Screen.height / 20 * 3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
                        }
                    }
                    if (Mathf.RoundToInt(count).ToString() == "0")
                    {
                        if (lesOptions.currentPosition == "Bottom Right")
                        {
                            GUI.Label(new Rect(Screen.width - Screen.width / 10, Screen.height / 20 * 3, 100, 100), "GO!", highTxt);
                        }
                        else if (lesOptions.currentPosition == "Top Right")
                        {
                            GUI.Label(new Rect(Screen.width - Screen.width / 10, Screen.height - Screen.height / 20 * 3, 100, 100), "GO!", highTxt);
                        }
                        else if (lesOptions.currentPosition == "Bottom Left")
                        {
                            GUI.Label(new Rect(Screen.width / 10, Screen.height / 20 * 3, 100, 100), "GO!", highTxt);
                        }
                        else if (lesOptions.currentPosition == "Top Left")
                        {
                            GUI.Label(new Rect(Screen.width / 10, Screen.height - Screen.height / 20 * 3, 100, 100), "GO!", highTxt);
                        }
                    }
                }
            }
        }
        if (wePlaying)
        {
            if (needTimer)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, 0, 100, 100), Mathf.RoundToInt(timer).ToString(), lesOptions.myText);
            }
            if (lesOptions.quickGame)
            {
                if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Right")
                {
                    GUI.Label(new Rect(Screen.width - Screen.width / 10, Screen.height / 20, 100, 100), score.ToString(), lesOptions.myText);

                    if (GUI.Button(new Rect(Screen.width - Screen.width / 10, Screen.height / 20 * 2, 80, 40), "Quit", lesOptions.myButton))
                    {
                        Destroy(lesOptions.transform.gameObject);
                        SceneManager.LoadScene("MixOptions");
                    }
                }
                else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Right")
                {
                    GUI.Label(new Rect(Screen.width - Screen.width / 10, Screen.height - Screen.height / 20 * 3, 100, 100), score.ToString(), lesOptions.myText);

                    if (GUI.Button(new Rect(Screen.width - Screen.width / 10, Screen.height - Screen.height / 20 * 2, 80, 40), "Quit", lesOptions.myButton))
                    {
                        Destroy(lesOptions.transform.gameObject);
                        SceneManager.LoadScene("MixOptions");
                    }
                }
                else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Left")
                {
                    GUI.Label(new Rect(Screen.width / 10, Screen.height / 20, 100, 100), score.ToString(), lesOptions.myText);

                    if (GUI.Button(new Rect(Screen.width / 10, Screen.height / 20 * 2, 80, 40), "Quit", lesOptions.myButton))
                    {
                        Destroy(lesOptions.transform.gameObject);
                        SceneManager.LoadScene("MixOptions");
                    }
                }
                else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Left")
                {
                    GUI.Label(new Rect(Screen.width / 10, Screen.height - Screen.height / 20 * 3, 100, 100), score.ToString(), lesOptions.myText);

                    if (GUI.Button(new Rect(Screen.width / 10, Screen.height - Screen.height / 20 * 2, 80, 40), "Quit", lesOptions.myButton))
                    {
                        Destroy(lesOptions.transform.gameObject);
                        SceneManager.LoadScene("MixOptions");
                    }
                }
            }
            else if (lesOptions.quickGame == false)
            {
                if (lesOptions.currentPosition == "Bottom Right")
                {
                    GUI.Label(new Rect(Screen.width - Screen.width / 10, Screen.height / 20, 100, 100), score.ToString(), lesOptions.myText);

                    if (GUI.Button(new Rect(Screen.width - Screen.width / 10, Screen.height / 20 * 2, 80, 40), "Quit", lesOptions.myButton))
                    {
                        Destroy(lesOptions.transform.gameObject);
                        SceneManager.LoadScene("MixOptions");
                    }
                }
                else if (lesOptions.currentPosition == "Top Right")
                {
                    GUI.Label(new Rect(Screen.width - Screen.width / 10, Screen.height - Screen.height / 20 * 3, 100, 100), score.ToString(), lesOptions.myText);

                    if (GUI.Button(new Rect(Screen.width - Screen.width / 10, Screen.height - Screen.height / 20 * 2, 80, 40), "Quit", lesOptions.myButton))
                    {
                        Destroy(lesOptions.transform.gameObject);
                        SceneManager.LoadScene("MixOptions");
                    }
                }
                else if (lesOptions.currentPosition == "Bottom Left")
                {
                    GUI.Label(new Rect(Screen.width / 10, Screen.height / 20, 100, 100), score.ToString(), lesOptions.myText);

                    if (GUI.Button(new Rect(Screen.width / 10, Screen.height / 20 * 2, 80, 40), "Quit", lesOptions.myButton))
                    {
                        Destroy(lesOptions.transform.gameObject);
                        SceneManager.LoadScene("MixOptions");
                    }
                }
                else if (lesOptions.currentPosition == "Top Left")
                {
                    GUI.Label(new Rect(Screen.width / 10, Screen.height - Screen.height / 20 * 3, 100, 100), score.ToString(), lesOptions.myText);

                    if (GUI.Button(new Rect(Screen.width / 10, Screen.height - Screen.height / 20 * 2, 80, 40), "Quit", lesOptions.myButton))
                    {
                        Destroy(lesOptions.transform.gameObject);
                        SceneManager.LoadScene("MixOptions");
                    }
                }
            }
        }
        // If the game is over
        if (!wePlaying && gameOver && !needReset)
        {
            if (!needName)
            {
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOUR SCORE IS: " + score.ToString(), lesOptions.myText);

                if (GUI.Button(new Rect(Screen.width / 2 - 80, Screen.height / 2 + 35, 160, 30), "Main Menu", lesOptions.myButton))
                {
                    Destroy(lesOptions.transform.gameObject);
                    SceneManager.LoadScene("MixOptions");
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 55, Screen.height / 2 + 75, 110, 30), "Replay", lesOptions.myButton))
                {
                    needReset = true;
                }
            }
            if (needNewHigh && needName)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 90, 100, 30), "INPUT NAME", lesOptions.myText);
                newName = GUI.TextField(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 50, 60, 30), newName, 3, highTxt);

                if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2, 80, 30), "Done", lesOptions.myButton))
                {
                    needName = false;
                }
            }
        }
    }
}
	


