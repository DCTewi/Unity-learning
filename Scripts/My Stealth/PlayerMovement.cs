using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip shoutingClip;
    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;
    private Animator animator;
    private HashID hash;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<HashID>();
        animator.SetLayerWeight(1, 1f);
    }

    private void Rotating (float h, float v)
    {
        Vector3 targetDir = new Vector3(h, 0, v);
        Quaternion targetRot = Quaternion.LookRotation(targetDir, Vector3.up);
        Rigidbody rb = this.GetComponent<Rigidbody>();
        Quaternion newRot = Quaternion.Lerp(rb.rotation, targetRot, turnSmoothing * Time.deltaTime);
        rb.MoveRotation(newRot);
    }

    private void MovementManagement(float h, float v, bool sneaking)
    {
        animator.SetBool(hash.sneakingBool, sneaking);
        if (h != 0 || v != 0)
        {
            Rotating(h, v);
            animator.SetFloat(hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
        }
        else
        {
            animator.SetFloat(hash.speedFloat, 0f);
        }
    }

    private void AudioManagement(bool shout)
    {
        AudioSource audioSource = this.GetComponent<AudioSource>();
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.locomotionState)
        {
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");
        MovementManagement(h, v, sneak);
    }
}
