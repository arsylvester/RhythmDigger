using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleRandomizer : MonoBehaviour
{
    [SerializeField] public Animator animator;
    // [SerializeField] public List<AnimationClip> animClips = new List<AnimationClip>();
    [SerializeField] public int numAnims = 5;
    [SerializeField] public float pauseTime = 1f, randomRange = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        if (!animator)
        {
            TryGetComponent<Animator>(out animator);
        }
        StartNewClip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewClip()
    {
        StartCoroutine(StartClip());
        

    }
    IEnumerator StartClip()
    {
        yield return new WaitForSeconds(Random.Range(pauseTime*(1-randomRange),pauseTime*(1+randomRange)));
        int randIndex = Random.Range(0,numAnims);
        animator.SetInteger("AnimIndex",randIndex);
        animator.SetTrigger("Sparkle");
    }
}
