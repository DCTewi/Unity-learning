using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashID : MonoBehaviour
{

    [HideInInspector] public int dyingState;
    [HideInInspector] public int locomotionState;
    [HideInInspector] public int shoutState;
    [HideInInspector] public int deadBool;
    [HideInInspector] public int speedFloat;
    [HideInInspector] public int sneakingBool;
    [HideInInspector] public int shoutingBool;
    [HideInInspector] public int playerInSightBool;
    [HideInInspector] public int shotFloat;
    [HideInInspector] public int aimWeightFloat;
    [HideInInspector] public int angularSpeedFloat;
    [HideInInspector] public int openBool;

    private void Awake()
    {
        dyingState = Animator.StringToHash("Base Layer.Dying");
        locomotionState = Animator.StringToHash("Base Layer.Locomotion");
        shoutState = Animator.StringToHash("Shouting.Shout");
        deadBool = Animator.StringToHash("Dead");
        speedFloat = Animator.StringToHash("Speed");
        sneakingBool = Animator.StringToHash("Sneaking");
        shoutingBool = Animator.StringToHash("Shouting");
        playerInSightBool = Animator.StringToHash("PlayerInSight");
        shotFloat = Animator.StringToHash("Shot");
        aimWeightFloat = Animator.StringToHash("AimWeight");
        angularSpeedFloat = Animator.StringToHash("AngulaarSpeed");
        openBool = Animator.StringToHash("Open");
}
}
