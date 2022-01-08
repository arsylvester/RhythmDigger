using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldReward : MonoBehaviour
{
	public int Value;
	[SerializeField] protected SoundSystem.SoundEvent SFX;
	private void Start()
	{
		Value = Mathf.FloorToInt(Random.Range(0.2f, 1.5f)) * Random.Range(1, 6);
		GetComponent<Rigidbody2D>().AddForce(Vector2.up + new Vector2(Random.Range(-1.1f, 1.1f), Random.Range(-1.1f, 1.1f)), ForceMode2D.Impulse);
		GetComponent<Rigidbody2D>().AddTorque(Random.Range(-2, 2), ForceMode2D.Impulse);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<GoldCollector>())
		{

		}
	}

	public void Collect()
	{
		SFX.PlayOneShot(0);
		GameManager._instance.AddGold(Value);
		Destroy(this.gameObject);
	}
}
