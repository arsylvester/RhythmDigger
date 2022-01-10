using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Conductor : Singleton<Conductor>
{
    public delegate void Beat();
    public static event Beat OnBeat = delegate {};
    // public static event Action<> OnBeat = delegate {};
    [SerializeField] public MusicData currentMusicData;  
    [SerializeField] public AudioSource audioSource; //an AudioSource attached to this GameObject that will play the music.
    public float musicBPM = 60f, firstBeatOffset = 0f, //First beat offset is in seconds
        validBeatOffset = 0.25f,
        beatBufferTime = 0.1f,
        pauseBetweenLoops = 0.1f;
    public int beatsOnScreen = 1;
    
    [SerializeField] public GameObject beatIndicatorPrefab;
    [SerializeField] public GameObject UI_beatSpawnLeft, UI_beatSpawnRight, UI_beatGoal, UI_beatIndicatorController;
    [SerializeField] public List<GameObject> currentBeats = new List<GameObject>();
    [SerializeField]public Animator heartbeatAnimator, comboAnimator;

    public float secPerBeat = 1f, beatsPerLoop = 1f, musicLength = 0f;

    public float songPosition,songPositionInBeats,dspSongTime,
        loopPositionInBeats,//The current position of the song within the loop in beats.
        loopPositionInAnalog,//The current relative position of the song within the loop measured between 0 and 1.
        beatElapsed;

    //the total number of loops completed since the looping clip first started
    public int completedLoops = 0;
    public int chainSize = 0, highestChain = 0, missedBeats = 0;
    [SerializeField] public int MAXMISSEDBEATS = 3;
    public Vector2 beatRange;
    public bool validBeat, early = false, musicPlaying, killChainOnMissedBeats;
    public bool gameIsOver = false;

    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        audioSource = GetComponent<AudioSource>();  

        LoadMusicFromData();     

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;
        Application.targetFrameRate = 60;
        Screen.SetResolution(360, 540, false);
        //Start the music
        // audioSource.Play();
        StartCoroutine(StartBeatWithDelay(firstBeatOffset));
        heartbeatAnimator.GetComponent<Image>().SetNativeSize();
        // GetComponent<Image>().SetNativeSize();
        UI_beatGoal.GetComponent<RectTransform>().sizeDelta = heartbeatAnimator.GetComponent<RectTransform>().sizeDelta;
    }

    private void OnEnable()
    {
        GameManager.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= GameOver;
    }

    private void GameOver()
    {
        gameIsOver = true;
    }

    public void LoadMusicFromData(){LoadMusicFromData(currentMusicData);}
    public void LoadMusicFromData(MusicData newData)
    {
        audioSource.clip = newData.audioClip;
        musicBPM = newData.musicBPM;
        firstBeatOffset = newData.firstBeatOffset;
        validBeatOffset = newData.validBeatOffset;
        pauseBetweenLoops = newData.pauseBetweenLoops;
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
            // UIManager._instance.UpdateChainCount(chainSize);
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
            validBeat = (beatElapsed + validBeatOffset < ((secPerBeat + validBeatOffset) * beatRange.x) || 
                        (beatElapsed + validBeatOffset > ((secPerBeat + validBeatOffset) * beatRange.y)));
            // validBeat = (beatElapsed + validBeatOffset < ((secPerBeat) * beatRange.x) || 
            //             (beatElapsed + validBeatOffset > ((secPerBeat) * beatRange.y)));
            if( beatElapsed < secPerBeat && beatElapsed > secPerBeat*0.5){
                early = true;
            } else {
                early = false;
            }
            if(killChainOnMissedBeats & missedBeats>=MAXMISSEDBEATS)
            {
                ResetChain();
            }
            // validBeat = (beatElapsed > (secPerBeat * beatRange.x) || (beatElapsed < (secPerBeat * beatRange.y)));
            // validBeat = (beatElapsed < (secPerBeat * beatRange.x + secPerBeat * beatRange.y));
        }
        
    }

    IEnumerator StartBeatWithDelay(float delayTime)
    {
        yield return new WaitForSeconds(pauseBetweenLoops);
        audioSource.Play();
        musicPlaying = true;
        yield return new WaitForSeconds(delayTime); //0.206
        InvokeRepeating("CreateBeats",0,secPerBeat);
        // InvokeRepeating("FlashAllBeats", 0, secPerBeat);
        // InvokeRepeating("FlashVignette", 0, secPerBeat);
    }
    void FlashAllBeats()
	{
        foreach (GameObject beatMover in currentBeats)
            beatMover.GetComponent<Animator>().Play("hit", 0, 0);
	}

    void FlashVignette()
	{
        if(!gameIsOver)
            CameraTarget.instance.vignette.GetComponent<Animator>().Play("VignetteBeat", 0, 0);
	}

    public void AnimateHeart()
    {

    }

    public void ResetChain()
    {
        chainSize = 0;
        GameManager._instance.goldMultiplier = 1;
        if(early)
            UIManager._instance.NotificationText("Early!");
        else
            UIManager._instance.NotificationText("Late!");
        // UIManager._instance.UpdateChainCount(chainSize);
        UpdateComboAnim();
    }
    // float pressTime = 0f;
    public bool CheckValidBeat()
    {
        // Debug.Log("Time between presses: "+(pressTime));
        // pressTime = 0f;
        // validBeat = (beatElapsed < (secPerBeat * beatRange.x) || (beatElapsed > (secPerBeat * beatRange.y)));
        // Debug.Log("beatElapsed+offset: "+(beatElapsed+validBeatOffset)+
        //         " <(secPerBeat * beatRange.x): "+((secPerBeat) * beatRange.x) + 
        //         " >(secPerBeat * beatRange.y): "+((secPerBeat) * beatRange.y) +
        //         " early? "+ early);
        Debug.Log("beatElapsed+offset: "+(beatElapsed+validBeatOffset)+
                " <(secPerBeat * beatRange.x): "+((secPerBeat + validBeatOffset) * beatRange.x) + 
                " >(secPerBeat * beatRange.y): "+((secPerBeat + validBeatOffset) * beatRange.y) +
                " early? "+ early);
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
            missedBeats = 0;
            GameManager._instance.goldMultiplier = (int)Mathf.Ceil((float)chainSize/10f);
            // UIManager._instance.UpdateChainCount(chainSize);
            // goldMultiplier = (int)Mathf.Ceil((float)chainSize/3f);
            UpdateComboAnim();
            if (chainSize > highestChain){
                highestChain = chainSize;
            }
               
            return true;
        }
        else
        {
            
            // heartbeatAnimator.Play("heartBeat_Bad",0,0);
            // heartbeatAnimator.SetTrigger("bad");
            ResetChain();
            return false;
        }
    }
    
    void UpdateComboAnim()
    {
        comboAnimator.SetInteger("Multiplier",GameManager._instance.goldMultiplier);
        UIManager._instance.UpdateChainCount(chainSize);
    }

    public void HideBeatUI()
    {

    }

    private IEnumerator killBeat(GameObject beat)
    {
        //beat.GetComponent<beatMover>().StopMove(); //removed for gamefeel reasons - CB feel free to override if its causing you trouble!
        //beat.GetComponent<Animator>().Play("beatIndicator_hit",0,0);
        // currentBeats.Remove(beat);
        yield return new WaitForSeconds(0.1f);

        // Destroy(beat);
    }

    void CreateBeats()
    {
        OnBeat();
        beatElapsed = 0;
        // beatElapsed = validBeatOffset;
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
