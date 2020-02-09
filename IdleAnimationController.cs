using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class IdleAnimationController : MonoBehaviour
{
    public Animator seriAnimator;
    private int indexValue;
    // Start is called before the first frame update
    void Start()
    {
        seriAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        indexValue = Random.Range(0, 3);
        //seriAnimator.SetInteger("idleIndex", indexValue);
        if (seriAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !seriAnimator.IsInTransition(0))
        {
            seriAnimator.SetInteger("idleIndex", indexValue);
        }
    }
}
