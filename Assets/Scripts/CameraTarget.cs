using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public static CameraTarget instance;
    public Transform target;
	public bool resetting;
	public float lerpSpeed;
	public bool waiting;
	private void Start()
	{
		if (instance != null && instance != this)
		{ Destroy(this.gameObject); return; }
		else
			instance = this;
	}
	void LateUpdate()
    {
		if (!resetting && !waiting)
			transform.position = new Vector3(0.5f, target.transform.position.y, 0);
		else if (!waiting)
		{
			transform.position += new Vector3(0, lerpSpeed, 0);
			if (transform.position.y > 4.5f)
				waiting = true;
		}
		transform.GetChild(0).transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
    }
}
