using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public bool isMerging;

    [SerializeField] private float mergeSpeed;

    private void Update()
    {
        if(isMerging) //when the merge sequence is activated for the current object
        {
            if(gameObject.CompareTag("Merger")) PlayBlobAnimation(); //play blob animation on the merger
            else if(!gameObject.CompareTag("Merger")) //if the object is not the merger
            {
                //move the object towards the merger over time by the chosen speed
                transform.position = Vector3.MoveTowards(transform.position, MergeSystem.instance.merger.position, mergeSpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision) //when two cubes collide
    {
        if (gameObject.CompareTag("Merger")) Destroy(collision.gameObject); //if the colliding object is not the merger, destroy it
    }

    private void PlayBlobAnimation()
    {
        animator.Play("CubeBlob"); //play the blob animation 
    }
}
