#pragma strict
import System.Collections.Generic;
import UnityEngine.SceneManagement;
public class MatchMngr extends MonoBehaviour
{
	var score: int = 0;
	private var startTimer: float;
	var timer: float;
	var count: float = 5.00;
	var range: int;
	var playerCubes: GameObject[];
	var targetCubes: GameObject[];
	var catTextures: Material[];
	var flwrTextures: Material[];
	var hatTextures: Material[];
	var giftTextures: Material[];
	var textures: Material[];
	var targetList: int[];
	var playerList: int[];
	var scoreSnds: AudioClip[];
	var sfMusic: AudioClip[];
	var hedMusic: AudioClip[];
	var marMusic: AudioClip[];
	var pokMusic: AudioClip[];
	var countClips: AudioClip[];
	var wePlaying = false;
	var needTimer = true;
	var gameOver = false;
	var needReset = false;
	var matchCam: Camera;
	var highTxt: GUIStyle;

	private var playCam: Camera;
	private var scoreTune: AudioClip;
	private var highScores: int[];
	private var highNames: String[];
	private var daHighs = new List.<int>();
	private var daNames = new List.<String>();
	private var daHighNames = new Dictionary.<int, String>();
	private var daLevel: String;
	private var daSuite: String;
	private var needStart = true;
	private var playedOne = false;
	private var playedTwo = false;
	private var playedThree = false;
	private var playedGo = false;
	private var needNewHigh = false;
	private var needName = false;
	private var lesOptions: MainMenu;
	private var newName: String= "AAA";



	function Start()
	{
		lesOptions = GameObject.FindGameObjectWithTag("Options").GetComponent(MainMenu);
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
				timer = 99.00;
				highScores = PlayerPrefsX.GetIntArray("HighScores99", 1200, 5);
				highNames = PlayerPrefsX.GetStringArray("HighNames99", "SLY PK 2", 5);
			}
			else if (PlayerPrefs.GetString("QuickTmr", "30") == "60")
			{				
				needTimer = true;
				timer = 60.00;
				highScores = PlayerPrefsX.GetIntArray("HighScores60", 1200, 5);
				highNames = PlayerPrefsX.GetStringArray("HighNames60", "SLY SN 4", 5);
			}
			else if (PlayerPrefs.GetString("QuickTmr", "30") == "45")
			{
				needTimer = true;
				timer = 45.00;
				highScores = PlayerPrefsX.GetIntArray("HighScores45", 1200, 5);
				highNames = PlayerPrefsX.GetStringArray("HighNames45", "SLY SF 3", 5);
			}
			else if (PlayerPrefs.GetString("QuickTmr", "30") == "30")
			{
				needTimer = true;
				timer = 30.00;
				highScores = PlayerPrefsX.GetIntArray("HighScores30", 1200, 5);
				highNames = PlayerPrefsX.GetStringArray("HighNames30", "SLY MR 2", 5);

			}
			else if (PlayerPrefs.GetString("QuickTmr", "30") == "Unlimited")
			{
				needTimer = false;
				timer = 99.00;
			}
			
			//moving the 'matchCam' based on saved/default options
			if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Right")
			{
				matchCam.rect = Rect(.75, 0, .25, .5);
			}
			else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Left")
			{
				matchCam.rect = Rect(0, .5, .25, .5);
			}
			else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Right")
			{
				matchCam.rect = Rect(.75, .5, .25, .5);
			}
			else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Left")
			{
				matchCam.rect = Rect(0, 0, .25, .5);
			}
			
			//Setting the level/background based off saved/default options
			daLevel = PlayerPrefs.GetString("QuickLvl", "Random");
			if (daLevel == "Random")
			{
				daLevel = lesOptions.levels[Random.Range(1, lesOptions.levels.length)];
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
				playCam.backgroundColor = Color(1.0, .55, 0);
				matchCam.backgroundColor = Color(1.0, .55, 0);
			}
			else if (daLevel == "Purple")
			{
				playCam.backgroundColor = Color(.3, 0, .65);
				matchCam.backgroundColor = Color(.3, 0, .65);
			}
			
			//Setting the Suite based off saved/default options
			daSuite = PlayerPrefs.GetString("QuickSuite", "Random");
			if (daSuite == "Random")
			{
				daSuite = lesOptions.suites[Random.Range(1, lesOptions.suites.length)];
			}
			if (daSuite == "Street Fighter")
			{
				textures = catTextures;
				for (var targ in targetCubes)
				{
					targ.transform.localScale = Vector3(.275, .275, .275);
					targ.GetComponent.<Renderer>().material = textures[0];
				}
				for (var play in playerCubes)
				{
					var pColsf = play.GetComponent(BoxCollider) as BoxCollider;
					play.transform.localScale = Vector3(.275, .275, .275);
					pColsf.size = Vector3(6, 4, 6);
					pColsf.center = Vector3(0, 0, -.5);
					play.GetComponent.<Renderer>().material = textures[0];
				}
			}
			else if (daSuite == "Mario")
			{
				textures = giftTextures;
				for (var targ in targetCubes)
				{
					targ.transform.localScale = Vector3(.4, .4, .4);
					targ.GetComponent.<Renderer>().material = textures[0];
				}
				for (var play in playerCubes)
				{
					var pColm = play.GetComponent(BoxCollider) as BoxCollider;
					play.transform.localScale = Vector3(.4, .4, .4);
					pColm.size = Vector3(4, 4, 4);
					pColm.center = Vector3(0, 0, 0);
					play.GetComponent.<Renderer>().material = textures[0];
				}
			}
			else if (daSuite == "Sonic")
			{
				textures = hatTextures;
				for (var targ in targetCubes)
				{
					targ.transform.localScale = Vector3(.23, .23, .23);
					targ.GetComponent.<Renderer>().material = textures[0];
				}
				for (var play in playerCubes)
				{
					var pCols = play.GetComponent(BoxCollider) as BoxCollider;
					play.transform.localScale = Vector3(.23, .23, .23);
					pCols.size = Vector3(7.5, 4, 7.5);
					pCols.center = Vector3(0, 0, -1);
					play.GetComponent.<Renderer>().material = textures[0];
				}
			}
			else if (daSuite == "Pokemon")
			{
				textures = flwrTextures;
				for (var targ in targetCubes)
				{
					targ.transform.localScale = Vector3(.275, .275, .275);
					targ.GetComponent.<Renderer>().material = textures[0];
				}
				for (var play in playerCubes)
				{
					var pColp = play.GetComponent(BoxCollider) as BoxCollider;
					play.transform.localScale = Vector3(.275, .275, .275);
					pColp.size = Vector3(6, 4, 6);
					pColp.center = Vector3(0, 0, 0);
					play.GetComponent.<Renderer>().material = textures[0];
				}
			}
		}
		else if (lesOptions.quickGame == false)
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
				timer = 99.00;
				highScores = PlayerPrefsX.GetIntArray("HighScores99", 1200, 5);
				highNames = PlayerPrefsX.GetStringArray("HighNames99", "SLY PK 2", 5);
			}
			else if (lesOptions.currentTimer == "60")
			{				
				needTimer = true;
				timer = 60.00;
				highScores = PlayerPrefsX.GetIntArray("HighScores60", 1200, 5);
				highNames = PlayerPrefsX.GetStringArray("HighNames60", "SLY SN 4", 5);
			}
			else if (lesOptions.currentTimer == "45")
			{
				needTimer = true;
				timer = 45.00;
				highScores = PlayerPrefsX.GetIntArray("HighScores45", 1200, 5);
				highNames = PlayerPrefsX.GetStringArray("HighNames45", "SLY SF 3", 5);
			}
			else if (lesOptions.currentTimer == "30")
			{
				needTimer = true;
				timer = 30.00;
				highScores = PlayerPrefsX.GetIntArray("HighScores30", 1200, 5);
				highNames = PlayerPrefsX.GetStringArray("HighNames30", "SLY MR 2", 5);

			}
			else if (lesOptions.currentTimer == "Unlimited")
			{
				needTimer = false;
				timer = 99.00;
			}
			
			//moving the 'matchCam' based on customize game options
			if (lesOptions.currentPosition == "Bottom Right")
			{
				matchCam.rect = Rect(.75, 0, .25, .5);
			}
			else if (lesOptions.currentPosition == "Top Left")
			{
				matchCam.rect = Rect(0, .5, .25, .5);
			}
			else if (lesOptions.currentPosition == "Top Right")
			{
				matchCam.rect = Rect(.75, .5, .25, .5);
			}
			else if (lesOptions.currentPosition == "Bottom Left")
			{
				matchCam.rect = Rect(0, 0, .25, .5);
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
				playCam.backgroundColor = Color(1.0, .55, 0);
				matchCam.backgroundColor = Color(1.0, .55, 0);
			}
			else if (daLevel == "Purple")
			{
				playCam.backgroundColor = Color(.3, 0, .65);
				matchCam.backgroundColor = Color(.3, 0, .65);
			}
			else if (daLevel == "Random")
			{
				daLevel = lesOptions.levels[Random.Range(1, lesOptions.levels.length)];
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
					playCam.backgroundColor = Color(1.0, .55, 0);
					matchCam.backgroundColor = Color(1.0, .55, 0);
				}
				else if (daLevel == "Purple")
				{
					playCam.backgroundColor = Color(.3, 0, .65);
					matchCam.backgroundColor = Color(.3, 0, .65);
				}
			}
			
			// Setting up the suite based on customize game options
			if (lesOptions.currentSuite == "Sonic")
			{
				daSuite = "Sonic";
				textures = hatTextures;
				for (var targ in targetCubes)
				{
					targ.transform.localScale = Vector3(.23, .23, .23);
					targ.GetComponent.<Renderer>().material = textures[0];
				}
				for (var play in playerCubes)
				{
					var pColsc = play.GetComponent(BoxCollider) as BoxCollider;
					play.transform.localScale = Vector3(.23, .23, .23);
					pColsc.size = Vector3(7.5, 4, 7.5);
					pColsc.center = Vector3(0, 0, -1);
					play.GetComponent.<Renderer>().material = textures[0];
				}
			}
			else if (lesOptions.currentSuite == "Street Fighter")
			{
				daSuite = "Street Fighter";
				textures = catTextures;
				for (var targ in targetCubes)
				{
					targ.transform.localScale = Vector3(.275, .275, .275);
					targ.GetComponent.<Renderer>().material = textures[0];
				}
				for (var play in playerCubes)
				{
					var pColsfc = play.GetComponent(BoxCollider) as BoxCollider;
					play.transform.localScale = Vector3(.275, .275, .275);
					pColsfc.size = Vector3(6, 4, 6);
					pColsfc.center = Vector3(0, 0, -.5);
					play.GetComponent.<Renderer>().material = textures[0];
				}
			}
			else if (lesOptions.currentSuite == "Pokemon")
			{
				daSuite = "Pokemon";
				textures = flwrTextures;
				for (var targ in targetCubes)
				{
					targ.transform.localScale = Vector3(.275, .275, .275);
					targ.GetComponent.<Renderer>().material = textures[0];
				}
				for (var play in playerCubes)
				{
					var pColpc = play.GetComponent(BoxCollider) as BoxCollider;
					play.transform.localScale = Vector3(.275, .275, .275);
					pColpc.size = Vector3(6, 4, 6);
					pColpc.center = Vector3(0, 0, 0);
					play.GetComponent.<Renderer>().material = textures[0];
				}
			}
			else if (lesOptions.currentSuite == "Mario")
			{
				daSuite = "Mario";
				textures = giftTextures;
				for (var targ in targetCubes)
				{
					targ.transform.localScale = Vector3(.4, .4, .4);
					targ.GetComponent.<Renderer>().material = textures[0];
				}
				for (var play in playerCubes)
				{
					var pColmc = play.GetComponent(BoxCollider) as BoxCollider;
					play.transform.localScale = Vector3(.4, .4, .4);
					pColmc.size = Vector3(4, 4, 4);
					pColmc.center = Vector3(0, 0, 0);
					play.GetComponent.<Renderer>().material = textures[0];
				}
			}
			else if (lesOptions.currentSuite == "Random")
			{
				daSuite = lesOptions.suites[Random.Range(1, lesOptions.suites.length)];
				if (daSuite == "Street Fighter")
				{
					textures = catTextures;
					for (var targ in targetCubes)
					{
						targ.transform.localScale = Vector3(.275, .275, .275);
						targ.GetComponent.<Renderer>().material = textures[0];
					}
					for (var play in playerCubes)
					{
						var pColsfr = play.GetComponent(BoxCollider) as BoxCollider;
						play.transform.localScale = Vector3(.275, .275, .275);
						pColsfr.size = Vector3(6, 4, 6);
						pColsfr.center = Vector3(0, 0, -.5);
						play.GetComponent.<Renderer>().material = textures[0];
					}
				}
				else if (daSuite == "Mario")
				{
					textures = giftTextures;
					for (var targ in targetCubes)
					{
						targ.transform.localScale = Vector3(.4, .4, .4);
						targ.GetComponent.<Renderer>().material = textures[0];
					}
					for (var play in playerCubes)
					{
						var pColmr = play.GetComponent(BoxCollider) as BoxCollider;
						play.transform.localScale = Vector3(.4, .4, .4);
						pColmr.size = Vector3(4, 4, 4);
						pColmr.center = Vector3(0, 0, 0);
						play.GetComponent.<Renderer>().material = textures[0];
					}
				}
				else if (daSuite == "Sonic")
				{
					textures = hatTextures;
					for (var targ in targetCubes)
					{
						targ.transform.localScale = Vector3(.23, .23, .23);
						targ.GetComponent.<Renderer>().material = textures[0];
					}
					for (var play in playerCubes)
					{
						var pColsr = play.GetComponent(BoxCollider) as BoxCollider;
						play.transform.localScale = Vector3(.23, .23, .23);
						pColsr.size = Vector3(7.5, 4, 7.5);
						pColsr.center = Vector3(0, 0, -1);
						play.GetComponent.<Renderer>().material = textures[0];
					}
				}
				else if (daSuite == "Pokemon")
				{
					textures = flwrTextures;
					for (var targ in targetCubes)
					{
						targ.transform.localScale = Vector3(.275, .275, .275);
						targ.GetComponent.<Renderer>().material = textures[0];
					}
					for (var play in playerCubes)
					{
						var pColpr = play.GetComponent(BoxCollider) as BoxCollider;
						play.transform.localScale = Vector3(.275, .275, .275);
						pColpr.size = Vector3(6, 4, 6);
						pColpr.center = Vector3(0, 0, 0);
						play.GetComponent.<Renderer>().material = textures[0];
					}
				}
				
			}
		}
		
		//Setting up audio based on suite
		if (daSuite == "Mario")
		{
			GetComponent.<AudioSource>().clip = marMusic[Random.Range(0, marMusic.length)];
			scoreTune = scoreSnds[0];
		}
		else if (daSuite == "Sonic")
		{
			GetComponent.<AudioSource>().clip = hedMusic[Random.Range(0, hedMusic.length)];
			scoreTune = scoreSnds[1];
		}
		else if (daSuite == "Street Fighter")
		{
			GetComponent.<AudioSource>().clip = sfMusic[Random.Range(0, sfMusic.length)];
			scoreTune = scoreSnds[2];
		}
		else if (daSuite == "Pokemon")
		{
			GetComponent.<AudioSource>().clip = pokMusic[Random.Range(0, pokMusic.length)];
			scoreTune = scoreSnds[3];
		}
		
		//Setting up mix camera border based on suite
		matchCam.GetComponent(MixCamGUI).daSuite = daSuite;
		if (lesOptions.quickGame)
		{
			matchCam.GetComponent(MixCamGUI).camPos = PlayerPrefs.GetString("QuickPos", "Bottom Right");
		}
		else if (lesOptions.quickGame == false)
		{
			matchCam.GetComponent(MixCamGUI).camPos = lesOptions.currentPosition;
		}
		targetList = new int[9];
		playerList = new int[9];
		
		startTimer = timer;
		SetTargetPic();
	}
	
	function Update()
	{
		if (needStart && !wePlaying) //We will count down from 3 to start the game
		{
			count -= Time.deltaTime;
			if (Mathf.RoundToInt(count) == 3 && !playedThree)
			{
				GetComponent.<AudioSource>().PlayOneShot(countClips[0], 1.0);
				playedThree = true;
			}
			else if (Mathf.RoundToInt(count) == 2 && !playedTwo)
			{
				GetComponent.<AudioSource>().PlayOneShot(countClips[1], 1.0);
				playedTwo = true;
			}
			else if (Mathf.RoundToInt(count) == 1 && !playedOne)
			{
				GetComponent.<AudioSource>().PlayOneShot(countClips[2], 1.0);
				playedOne = true;
			}
			else if (Mathf.RoundToInt(count) == 0 && !playedGo)
			{
				GetComponent.<AudioSource>().PlayOneShot(countClips[3], 1.0);
				playedGo = true;
			}
			else if (count <= 00)
			{
				GetComponent.<AudioSource>().Play();
				wePlaying = true;
				needStart = false;
			}
		}
		if (wePlaying && needTimer) //Now that the game has started, start the timer
		{
			timer -= Time.deltaTime;
		}
		if (targetList == playerList && wePlaying) //If the player pic matches the target pic...
		{
			score += 100*range;
			GetComponent.<AudioSource>().PlayOneShot(scoreTune, .7);
			if (needTimer)
			{
				if (range == 2)
				{
					timer += 1.0;
				}
				else if (range == 3)
				{
					timer += 2.0;
				}
				else if (range == 4)
				{
					timer += 3.0;
				}
			}
			SetTargetPic();
		}
		if (timer <= 00 && wePlaying) //If we run out of time
		{
			GetComponent.<AudioSource>().Stop();
			wePlaying = false;
			gameOver = true;
			for (var targ in targetCubes)
			{
				targ.GetComponent.<Renderer>().enabled = false;
			}
			for (var play in playerCubes)
			{
				play.GetComponent.<Renderer>().enabled = false;
			}
			for (var high in highScores)
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
			var i: int;

			for (i=0; i<5; i++)
			{
				daHighNames.Add(highScores[i]+i+1, highNames[i]);
			}
			if (daSuite == "Street Fighter")
			{
				daHighNames.Add(score, newName+" SF "+range.ToString());
			}
			else if (daSuite == "Mario")
			{
				daHighNames.Add(score, newName+" MR "+range.ToString());
			}
			else if (daSuite == "Sonic")
			{
				daHighNames.Add(score, newName+" SN "+range.ToString());
			}
			else if (daSuite == "Pokemon")
			{
				daHighNames.Add(score, newName+" PK "+range.ToString());
			}
			for (var num in daHighNames.Keys)
			{
				daHighs.Add(num);
			}
			daHighs.Sort();
			daHighs.Reverse();
			for (i=0; i<5; i++)
			{
				daNames.Add(daHighNames[daHighs[i]]);
			}
			daHighs.RemoveAt(highScores.Length);
			for (i=0; i<5; i++)
			{
				daHighs[i]= daHighs[i] - daHighs[i]%10;
				if (daHighs[i] == 0)
				{
					daHighs[i] +=100;
				}
			}
			highScores = daHighs.ToArray();
			highNames = daNames.ToArray();
			if (startTimer == 99.00)
			{
				PlayerPrefsX.SetIntArray("HighScores99", highScores);
				PlayerPrefsX.SetStringArray("HighNames99", highNames);
			}
			else if (startTimer == 60.00)
			{
				PlayerPrefsX.SetIntArray("HighScores60", highScores);
				PlayerPrefsX.SetStringArray("HighNames60", highNames);
			} 
			else if (startTimer == 45.00)
			{
				PlayerPrefsX.SetIntArray("HighScores45", highScores);
				PlayerPrefsX.SetStringArray("HighNames45", highNames);
			}
			else if (startTimer == 30.00)
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
			count = 4.0;
			
			SetTargetPic();
			
			for (var play in playerCubes)
			{
				play.GetComponent.<Renderer>().material = textures[0];
			}
			for (var num in playerList)
			{
				num = 0;
			}
			for (var targ in targetCubes)
			{
				targ.GetComponent.<Renderer>().enabled = true;
			}
			for (var play in playerCubes)
			{
				play.GetComponent.<Renderer>().enabled = true;
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
	
	function SetTargetPic()
	{
		targetList[0] = Random.Range(0, range);
		targetCubes[0].GetComponent.<Renderer>().material = textures[targetList[0]];
		
		targetList[1] = Random.Range(0, range);
		targetCubes[1].GetComponent.<Renderer>().material = textures[targetList[1]];
		
		targetList[2] = Random.Range(0, range);
		targetCubes[2].GetComponent.<Renderer>().material = textures[targetList[2]];
		
		targetList[3] = Random.Range(0, range);
		targetCubes[3].GetComponent.<Renderer>().material = textures[targetList[3]];

		targetList[4] = Random.Range(0, range);
		targetCubes[4].GetComponent.<Renderer>().material = textures[targetList[4]];
		
		targetList[5] = Random.Range(0, range);
		targetCubes[5].GetComponent.<Renderer>().material = textures[targetList[5]];

		targetList[6] = Random.Range(0, range);
		targetCubes[6].GetComponent.<Renderer>().material = textures[targetList[6]];

		targetList[7] = Random.Range(0, range);
		targetCubes[7].GetComponent.<Renderer>().material = textures[targetList[7]];

		targetList[8] = Random.Range(0, range);
		targetCubes[8].GetComponent.<Renderer>().material = textures[targetList[8]];
		
		return targetList;
	}
	
	function OnGUI()
	{
		if (needStart && !wePlaying)
		{
			if (count < 3.5)
			{
				if (lesOptions.quickGame)
				{
					if (Mathf.RoundToInt(count).ToString() != "0")
					{
						if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Right")
						{
							GUI.Label (Rect (Screen.width - Screen.width/10, Screen.height/20*3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
						}
						else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Right")
						{
							GUI.Label (Rect (Screen.width - Screen.width/10, Screen.height-Screen.height/20*3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
						}
						else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Left")
						{
							GUI.Label (Rect (Screen.width/10, Screen.height/20*3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
						}
						else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Left")
						{
							GUI.Label (Rect (Screen.width/10, Screen.height-Screen.height/20*3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
						}
					}
					if (Mathf.RoundToInt(count).ToString() == "0")
					{
						if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Right")
						{
							GUI.Label (Rect (Screen.width - Screen.width/10, Screen.height/20*3, 100, 100), "GO!", highTxt);
						}
						else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Right")
						{
							GUI.Label (Rect (Screen.width - Screen.width/10, Screen.height-Screen.height/20*3, 100, 100), "GO!", highTxt);
						}
						else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Left")
						{
							GUI.Label (Rect (Screen.width/10, Screen.height/20*3, 100, 100), "GO!", highTxt);
						}
						else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Left")
						{
							GUI.Label (Rect (Screen.width/10, Screen.height-Screen.height/20*3, 100, 100), "GO!", highTxt);
						}
					}
				}
				else if (lesOptions.quickGame == false)
				{
					if (Mathf.RoundToInt(count).ToString() != "0")
					{
						if (lesOptions.currentPosition == "Bottom Right")
						{
							GUI.Label (Rect (Screen.width - Screen.width/10, Screen.height/20*3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
						}
						else if (lesOptions.currentPosition == "Top Right")
						{
							GUI.Label (Rect (Screen.width - Screen.width/10, Screen.height-Screen.height/20*3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
						}
						else if (lesOptions.currentPosition == "Bottom Left")
						{
							GUI.Label (Rect (Screen.width/10, Screen.height/20*3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
						}
						else if (lesOptions.currentPosition == "Top Left")
						{
							GUI.Label (Rect (Screen.width/10, Screen.height-Screen.height/20*3, 100, 100), Mathf.RoundToInt(count).ToString(), highTxt);
						}
					}
					if (Mathf.RoundToInt(count).ToString() == "0")
					{
						if (lesOptions.currentPosition == "Bottom Right")
						{
							GUI.Label (Rect (Screen.width - Screen.width/10, Screen.height/20*3, 100, 100), "GO!", highTxt);
						}
						else if (lesOptions.currentPosition == "Top Right")
						{
							GUI.Label (Rect (Screen.width - Screen.width/10, Screen.height-Screen.height/20*3, 100, 100), "GO!", highTxt);
						}
						else if (lesOptions.currentPosition == "Bottom Left")
						{
							GUI.Label (Rect (Screen.width/10, Screen.height/20*3, 100, 100), "GO!", highTxt);
						}
						else if (lesOptions.currentPosition == "Top Left")
						{
							GUI.Label (Rect (Screen.width/10, Screen.height-Screen.height/20*3, 100, 100), "GO!", highTxt);
						}
					}
				}
			}
		}
		if (wePlaying)
		{
			if (needTimer)
			{
				GUI.Label (Rect (Screen.width/2 - 50, 0, 100, 100), Mathf.RoundToInt(timer).ToString(), lesOptions.myText);
			}
			if (lesOptions.quickGame)
			{
				if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Right")
				{
					GUI.Label (Rect (Screen.width - Screen.width/10, Screen.height/20, 100, 100), score.ToString(), lesOptions.myText);
					
					if (GUI.Button (Rect (Screen.width-Screen.width/10, Screen.height/20*2, 80, 40), "Quit", lesOptions.myButton))
                    {
                        Destroy(lesOptions.transform.gameObject);
                        SceneManager.LoadScene("MixOptions");
					}
				}
				else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Right")
				{
					GUI.Label (Rect (Screen.width - Screen.width/10, Screen.height-Screen.height/20*3, 100, 100), score.ToString(), lesOptions.myText);
					
					if (GUI.Button (Rect (Screen.width-Screen.width/10,Screen.height-Screen.height/20*2, 80, 40), "Quit", lesOptions.myButton))
					{
                        Destroy(lesOptions.transform.gameObject);
						SceneManager.LoadScene("MixOptions");
					}
				}
				else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Bottom Left")
				{
					GUI.Label (Rect (Screen.width/10, Screen.height/20, 100, 100), score.ToString(), lesOptions.myText);
					
					if (GUI.Button (Rect (Screen.width/10,Screen.height/20*2, 80, 40), "Quit", lesOptions.myButton))
					{
                        Destroy(lesOptions.transform.gameObject);
						SceneManager.LoadScene("MixOptions");
					}
				}
				else if (PlayerPrefs.GetString("QuickPos", "Bottom Right") == "Top Left")
				{
					GUI.Label (Rect (Screen.width/10, Screen.height-Screen.height/20*3, 100, 100), score.ToString(), lesOptions.myText);
					
					if (GUI.Button (Rect (Screen.width/10,Screen.height-Screen.height/20*2, 80, 40), "Quit", lesOptions.myButton))
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
					GUI.Label (Rect (Screen.width - Screen.width/10, Screen.height/20, 100, 100), score.ToString(), lesOptions.myText);
					
					if (GUI.Button (Rect (Screen.width-Screen.width/10, Screen.height/20*2, 80, 40), "Quit", lesOptions.myButton))
					{
                        Destroy(lesOptions.transform.gameObject);
						SceneManager.LoadScene("MixOptions");
					}
				}
				else if (lesOptions.currentPosition == "Top Right")
				{
					GUI.Label (Rect (Screen.width - Screen.width/10, Screen.height-Screen.height/20*3, 100, 100), score.ToString(), lesOptions.myText);
					
					if (GUI.Button (Rect (Screen.width-Screen.width/10,Screen.height-Screen.height/20*2, 80, 40), "Quit", lesOptions.myButton))
					{
                        Destroy(lesOptions.transform.gameObject);
						SceneManager.LoadScene("MixOptions");
					}
				}
				else if (lesOptions.currentPosition == "Bottom Left")
				{
					GUI.Label (Rect (Screen.width/10, Screen.height/20, 100, 100), score.ToString(), lesOptions.myText);
					
					if (GUI.Button (Rect (Screen.width/10,Screen.height/20*2, 80, 40), "Quit", lesOptions.myButton))
					{
                        Destroy(lesOptions.transform.gameObject);
						SceneManager.LoadScene("MixOptions");
					}
				}
				else if (lesOptions.currentPosition == "Top Left")
				{
					GUI.Label (Rect (Screen.width/10, Screen.height-Screen.height/20*3, 100, 100), score.ToString(), lesOptions.myText);
					
					if (GUI.Button (Rect (Screen.width/10,Screen.height-Screen.height/20*2, 80, 40), "Quit", lesOptions.myButton))
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
				GUI.Label (Rect (Screen.width/2-100, Screen.height/2-50, 200, 100), "YOUR SCORE IS: " + score.ToString(), lesOptions.myText);
				
				if (GUI.Button (Rect (Screen.width/2-80, Screen.height/2+35, 160, 30), "Main Menu", lesOptions.myButton))
                {
                    Destroy(lesOptions.transform.gameObject);
					SceneManager.LoadScene("MixOptions");
				}
				
				if (GUI.Button (Rect (Screen.width/2-55, Screen.height/2+75, 110, 30), "Replay", lesOptions.myButton))
				{
					needReset = true;
				}
			}
			if (needNewHigh && needName)
			{
				GUI.Label (Rect (Screen.width/2-50, Screen.height/2-90, 100, 30), "INPUT NAME", lesOptions.myText);
				newName = GUI.TextField(Rect (Screen.width/2-30, Screen.height/2-50, 60, 30),newName,3, highTxt);
				
				if (GUI.Button (Rect (Screen.width/2-40, Screen.height/2, 80, 30), "Done", lesOptions.myButton))
				{
					needName = false;
				}
			}
		}
	}
}
