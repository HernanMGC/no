using UnityEngine;
using System;

[Serializable]
public class BehaviourReaction : DelayedReaction
{
    public Behaviour behaviour;
    public bool enabledState;


    protected override void ImmediateReaction()
    {
        behaviour.enabled = enabledState;
    }
}