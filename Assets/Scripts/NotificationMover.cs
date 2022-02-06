using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationMover : MonoBehaviour
{

	public float rate;
	private float currentTime;
	// Update is called once per frame

	private void Start()
	{
		Destroy(this.gameObject, UIManager._instance.notifDuration);
	}
	void Update()
    {
		currentTime += Time.deltaTime;
		if (currentTime > rate)
		{
			currentTime = 0;
			transform.localPosition += Vector3.up;
		} 
    }
}
