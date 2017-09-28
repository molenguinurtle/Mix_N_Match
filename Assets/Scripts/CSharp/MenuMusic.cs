using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MenuMusic : MonoBehaviour
{
    private float t = 0;
    private AudioSource myAudio;
    public AudioClip[] lesTunes;

	void Start ()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.clip = lesTunes[Random.Range(0, lesTunes.Length)];
        myAudio.Play();
    }
	
	void Update ()
    {
        t += Time.deltaTime;
        if (t >= myAudio.clip.length)
        {
            myAudio.Stop();
            myAudio.clip = lesTunes[Random.Range(0, lesTunes.Length)];
            myAudio.Play();
            t = 0;
        }
    }
}
