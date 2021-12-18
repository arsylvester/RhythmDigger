using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Conductor : MonoBehaviour
{
    public GameObject beatIndicatorPrefab;
    public GameObject UI_beatSpawnLeft, UI_beatSpawnRight, UI_beatGoal, UI_beatIndicatorController;
    [SerializeField]
    public List<GameObject> currentBeats = new List<GameObject>();
    [SerializeField]
    // public List<Sprite> heartbeatAnimation = new List<Sprite>();
    public Animator heartbeatAnimator;

    //This is determined by the song you're trying to sync up to
    [SerializeField]
    public float musicBPM = 60f;
    
    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    //Conductor instance
    public static Conductor instance;

    //the number of beats in each loop
    public float beatsPerLoop;

    //the total number of loops completed since the looping clip first started
    public int completedLoops = 0;

    //The current position of the song within the loop in beats.
    public float loopPositionInBeats;
    //The current relative position of the song within the loop measured between 0 and 1.
    public float loopPositionInAnalog;
    public float songLength = 0f;

    public float beatElapsed;
    public Vector2 beatRange;
    public bool validBeat;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / musicBPM;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();
        InvokeRepeating("CreateBeats",0.1f,secPerBeat);
        heartbeatAnimator.GetComponent<Image>().SetNativeSize();
        GetComponent<Image>().SetNativeSize();
        UI_beatGoal.GetComponent<RectTransform>().sizeDelta = heartbeatAnimator.GetComponent<RectTransform>().sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        // determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        songLength = musicSource.clip.length;
        beatsPerLoop = (musicBPM)*(songLength/60f);
        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
        if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
            completedLoops++;
        loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;


        beatElapsed += Time.deltaTime;
        validBeat = (beatElapsed < (secPerBeat * beatRange.x) || (beatElapsed > (secPerBeat * beatRange.y)));
    }

    public void AnimateHeart()
    {

    }

    public bool CheckValidBeat()
    {
        float goalWidth = UI_beatGoal.GetComponent<RectTransform>().sizeDelta.x;
        GameObject topBeat1 = currentBeats[0];
        GameObject topBeat2 = currentBeats[1];
        float curPos = topBeat1.GetComponent<RectTransform>().anchoredPosition.x;
        if(validBeat)
        {
            heartbeatAnimator.Play("heartBeat_Good",0,0);
            StartCoroutine(killBeat(topBeat1));
            StartCoroutine(killBeat(topBeat2));
            return true;
        }
        else
        {
            heartbeatAnimator.Play("heartBeat_Bad",0,0);
            return false;
        }
    }

    private IEnumerator killBeat(GameObject beat)
    {
        //beat.GetComponent<beatMover>().StopMove(); //removed for gamefeel reasons - CB feel free to override if its causing you trouble!
        beat.GetComponent<Animator>().Play("beatIndicator_hit",0,0);
        //currentBeats.Remove(beat);
        yield return new WaitForSeconds(1f);

        //Destroy(beat);
    }

    void CreateBeats()
    {
        beatElapsed = 0;
        heartbeatAnimator.Play("heartBeat_heartBeat", 0, 0);

        // Create left beat indicator
        Vector3 spawnPos = new Vector3(UI_beatSpawnLeft.GetComponent<RectTransform>().localPosition.x,0,0);
        Vector3 targetPos = new Vector3(UI_beatGoal.GetComponent<RectTransform>().anchoredPosition.x,0,0);
        RectTransform parent = UI_beatIndicatorController.GetComponent<RectTransform>();
        GameObject goLeft = Instantiate(beatIndicatorPrefab, spawnPos, Quaternion.identity, parent) as GameObject;
        goLeft.GetComponent<RectTransform>().anchoredPosition = spawnPos;
        BeatMover mover = goLeft.GetComponent<BeatMover>();  
        mover.StartMove(targetPos, musicBPM);
        currentBeats.Add(goLeft);

        // Create Right beat indicator
        spawnPos = new Vector3(UI_beatSpawnRight.GetComponent<RectTransform>().localPosition.x,0,0);
        targetPos = new Vector3(UI_beatGoal.GetComponent<RectTransform>().anchoredPosition.x,0,0);
        parent = UI_beatIndicatorController.GetComponent<RectTransform>();
        GameObject goRight = Instantiate(beatIndicatorPrefab, spawnPos, Quaternion.identity, parent) as GameObject;
        goRight.GetComponent<RectTransform>().anchoredPosition = spawnPos;
        currentBeats.Add(goRight);
        mover = goRight.GetComponent<BeatMover>();  
        mover.StartMove(targetPos, musicBPM);      
    }
}
