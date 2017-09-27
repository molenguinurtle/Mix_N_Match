#pragma strict
var lesTunes: AudioClip[];
private var t: float = 0.0;
function Start ()
{
	GetComponent.<AudioSource>().clip = lesTunes[Random.Range(0, lesTunes.length)];
	GetComponent.<AudioSource>().Play();
}

function Update ()
{
	t += Time.deltaTime;
	if (t>= GetComponent.<AudioSource>().clip.length)
	{
		GetComponent.<AudioSource>().Stop();
		GetComponent.<AudioSource>().clip = lesTunes[Random.Range(0, lesTunes.length)];
		GetComponent.<AudioSource>().Play();
		t = 0.0;
	}
}