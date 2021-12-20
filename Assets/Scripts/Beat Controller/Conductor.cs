using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Conductor : Singleton<Conductor>
{
    [SerializeField] public MusicData currentMusicData;  
    [SerializeField] public AudioSource audioSource; //an AudioSource attached to this GameObject that will play the music.
    public float musicBPM = 60f, firstBeatOffset = 0f, //First beat offset is in seconds
        beatBufferTime = 0.1f;
    public int beatsOnScreen = 1;
    
    [SerializeField] public GameObject beatIndicatorPrefab;
    [SerializeField] public GameObject UI_beatSpawnLeft, UI_beatSpawnRight, UI_beatGoal, UI_beatIndicatorController;
    [SerializeField] public List<GameObject> currentBeats = new List<GameObject>();
    [SerializeField]public Animator heartbeatAnimator;

    public float secPerBeat = 1f, beatsPerLoop = 1f, musicLength = 0f;

    public float songPosition,songPositionInBeats,dspSongTime,
        loopPositionInBeats,//The current position of the song within the loop in beats.
        loopPositionInAnalog,//The current relative position of the song within the loop measured between 0 and 1.
        beatElapsed;

    //the total number of loops completed since the looping clip first started
    public int completedLoops = 0;
    public int chainSize = 0, highestChain = 0, goldMultiplier = 1;
    public Vector2 beatRange;
    public bool validBeat, musicPlaying;

    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        audioSource = GetComponent<AudioSource>();  

        LoadMusicFromData();     

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        // audioSource.Play();
        StartCoroutine(StartBeatWithDelay(firstBeatOffset));
        heartbeatAnimator.GetComponent<Image>().SetNativeSize();
        // GetComponent<Image>().SetNativeSize();
        UI_beatGoal.GetComponent<RectTransform>().sizeDelta = heartbeatAnimator.GetComponent<RectTransform>().sizeDelta;
    }

    public void LoadMusicFromData(){LoadMusicFromData(currentMusicData);}
    public void LoadMusicFromData(MusicData newData)
    {
        audioSource.clip = newData.audioClip;
        musicBPM = newData.musicBPM;
        firstBeatOffset = newData.firstBeatOffset;
        beatsOnScreen = newData.beatsOnScreen;

        secPerBeat = 60f / musicBPM;
        musicLength = newData.audioClip.length;
        beatsPerLoop = (musicBPM)*(musicLength/60f);
    }

    // Update is called once per frame
    void Update()
    {
        if(musicPlaying)
        {
            UIManager._instance.UpdateChainCount(chainSize);
            // determine how many seconds since the song started
            songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

            //determine how many beats since the song started
            songPositionInBeats = songPosition / secPerBeat;
    
            loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
            if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
            {
                completedLoops++;
                musicPlaying = false;
                CancelInvoke();
                StartCoroutine(StartBeatWithDelay(firstBeatOffset));
            }
                
            loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;


            beatElapsed += Time.deltaTime;
            // pressTime += Time.deltaTime;
            validBeat = (beatElapsed < (secPerBeat * beatRange.x) || (beatElapsed > (secPerBeat * beatRange.y)));
            // validBeat = (beatElapsed > (secPerBeat * beatRange.x) || (beatElapsed < (secPerBeat * beatRange.y)));
            // validBeat = (beatElapsed < (secPerBeat * beatRange.x + secPerBeat * beatRange.y));
        }
        
    }

    IEnumerator StartBeatWithDelay(float delayTime)
    {
        yield return new WaitForSeconds(0.1f);
        audioSource.Play();
        musicPlaying = true;
        yield return new WaitForSeconds(delayTime); //0.206
        InvokeRepeating("CreateBeats",0,secPerBeat);
    }

    public void AnimateHeart()
    {

    }

    public void ResetChain()
    {
        chainSize = 0;
        goldMultiplier = 1;
    }
    // float pressTime = 0f;
    public bool CheckValidBeat()
    {
        // Debug.Log("Time between presses: "+(pressTime));
        // pressTime = 0f;
        validBeat = (beatElapsed < (secPerBeat * beatRange.x) || (beatElapsed > (secPerBeat * beatRange.y)));
        // Debug.Log("beatElapsed: "+beatElapsed+" (secPerBeat * beatRange.y): "+(secPerBeat * beatRange.y)+ " (secPerBeat * beatRange.x): "+(secPerBeat * beatRange.x));
        // validBeat = (beatElapsed > (secPerBeat * beatRange.x) || (beatElapsed < (secPerBeat * beatRange.y)));
        float goalWidth = UI_beatGoal.GetComponent<RectTransform>().sizeDelta.x;
        GameObject topBeat1 = currentBeats[0];
        GameObject topBeat2 = currentBeats[1];
        float curPos = topBeat1.GetComponent<RectTransform>().anchoredPosition.x;
        if(validBeat)
        {
            // heartbeatAnimator.Play("heartBeat_Good",0,0);
            // heartbeatAnimator.SetTrigger("good");
            StartCoroutine(killBeat(topBeat1));
            StartCoroutine(killBeat(topBeat2));
            chainSize++;
            goldMultiplier = (int)Mathf.Ceil((float)chainSize/10f);
            if (chainSize > highestChain){
                highestChain = chainSize;
            }
               
            return true;
        }
        else
        {
            
            // heartbeatAnimator.Play("heartBeat_Bad",0,0);
            // heartbeatAnimator.SetTrigger("bad");
            return false;
        }
    }

    private IEnumerator killBeat(GameObject beat)
    {
        //beat.GetComponent<beatMover>().StopMove(); //removed for gamefeel reasons - CB feel free to override if its causing you trouble!
        beat.GetComponent<Animator>().Play("beatIndicator_hit",0,0);
        currentBeats.Remove(beat);
        yield return new WaitForSeconds(0.1f);

        Destroy(beat);
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
        beatMover mover = goLeft.GetComponent<beatMover>();  
        mover.StartMove(targetPos, musicBPM, beatsOnScreen, beatBufferTime);
        currentBeats.Add(goLeft);

        // Create Right beat indicator
        spawnPos = new Vector3(UI_beatSpawnRight.GetComponent<RectTransform>().localPosition.x,0,0);
        targetPos = new Vector3(UI_beatGoal.GetComponent<RectTransform>().anchoredPosition.x,0,0);
        parent = UI_beatIndicatorController.GetComponent<RectTransform>();
        GameObject goRight = Instantiate(beatIndicatorPrefab, spawnPos, Quaternion.identity, parent) as GameObject;
        goRight.GetComponent<RectTransform>().anchoredPosition = spawnPos;
        currentBeats.Add(goRight);
        mover = goRight.GetComponent<beatMover>();  
        mover.StartMove(targetPos, musicBPM, beatsOnScreen, beatBufferTime);  

        // heartbeatAnimator.Play("heartBeat_heartBeat", 0, 0);    
        // heartbeatAnimator.Play("heartBeat_Good", 0, 0);  
    }
}
