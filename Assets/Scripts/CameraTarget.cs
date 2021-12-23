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
	private void Start()
	{
		if (instance != null && instance != this)
		{ Destroy(this.gameObject); return; }
		else
			instance = this;
	}

    public void ResetToTop()
    {
        StartCoroutine(ScaleVignette());

        IEnumerator ScaleVignette()
        {
            GetComponentInChildren<Animator>().Play("VignetteSpin", 0, 0);
            Vector3 goal = new Vector3(1.75f, 1.75f, 1);
            Vector3 start = vignette.transform.lossyScale;
            int elapsedFrames = 0;
            while (elapsedFrames < expandVignetteFrameCount)
            {
                float i = (float)++elapsedFrames / expandVignetteFrameCount;
                vignette.transform.localScale = Vector3.Lerp(start, goal, i);
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
            //if(speed < maxSpeed)
            //    speed += acceleration;
            transform.position += new Vector3(0, speed, 0);
            vignette.transform.position = Vector3.Lerp(vignette.transform.position, new Vector3(0.5f, transform.position.y, transform.position.z), 0.01f);
            if (transform.position.y > 4.5f)
                waiting = true;

            if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame || Mouse.current.backButton.wasPressedThisFrame)
                transform.position = new Vector3(0.5f, 4.5f, 0);
        }
    }
}