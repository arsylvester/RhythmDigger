using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraTarget : MonoBehaviour
{
    public static CameraTarget instance;
    public Transform target;
	public bool resetting;
	public float speed = .01f;
	public float acceleration = .01f;
    public float maxSpeed = 10;
	public bool waiting;
    public GameObject vignette;
    public int expandVignetteFrameCount = 60;
    public float vignetteExpandedRadius = 1.75f;
	
    private void Awake()
    {
        Conductor.OnBeat += FlashVignette;
    }
    void OnDestroy()
    {
        Conductor.OnBeat -= FlashVignette;
    }
    private void Start()
	{
		if (instance != null && instance != this)
		{ Destroy(this.gameObject); return; }
		else
			instance = this;
	}

    void FlashVignette()
	{
        if(!Conductor._instance.gameIsOver)
            vignette.GetComponent<Animator>().Play("VignetteBeat", 0, 0);
	}

    public void ResetToTop()
    {
        GameManager._instance.SetGameOver();
        StartCoroutine(ScaleVignette());

        IEnumerator ScaleVignette()
        {
            //GetComponentInChildren<Animator>().StopPlayback();
            GetComponentInChildren<Animator>().Play("VignetteSpin", 0, 0);

            Vector3 goal = new Vector3(vignetteExpandedRadius, vignetteExpandedRadius, 1);
            Vector3 start = transform.lossyScale;
            int elapsedFrames = 0;
            while (elapsedFrames < expandVignetteFrameCount)
            {
                float i = (float)++elapsedFrames / expandVignetteFrameCount;
                transform.localScale = Vector3.Lerp(start, goal, i);
                yield return new WaitForEndOfFrame();
            }

            resetting = true;
        }
    }

	void LateUpdate()
    {
        if (!resetting && !waiting)
        {
            transform.position = new Vector3(0.5f, target.transform.position.y, 0);
            vignette.transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
        }
        else if (!waiting)
        {
            if(speed < maxSpeed)
                speed += acceleration;
            transform.position += new Vector3(0, speed, 0);
            vignette.transform.position = Vector3.Lerp(vignette.transform.position, new Vector3(0.5f, transform.position.y, transform.position.z), 0.01f);
            if (transform.position.y > 4.5f)
                waiting = true;

            if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame || Mouse.current.backButton.wasPressedThisFrame)
                transform.position = new Vector3(0.5f, 4.5f, 0);
        }
    }
}