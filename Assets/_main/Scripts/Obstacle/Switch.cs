using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Character
{
    public UnityEngine.Events.UnityEvent SwitchOn;
    public UnityEngine.Events.UnityEvent SwitchOff;

    public enum SwitchStatus { On, Off };
    public SwitchStatus Status = SwitchStatus.Off;

    public bool IsDestructable = true;


    // Start is called before the first frame update
    new private void Start()
    {
        base.Start();

        opposingLayer = LayerMask.NameToLayer("Player Ability");

        cHealth.OnHit += CallEvent;

        if(!IsDestructable) { cHealth.OnHit += cHealth.FullHeal; }
    }

    private void CallEvent()
    {
        switch(Status)
        {
            case SwitchStatus.Off:
                SwitchOn?.Invoke();
                Status = SwitchStatus.On;
                break;
            case SwitchStatus.On:
                SwitchOn?.Invoke();
                Status = SwitchStatus.Off;
                break;
            default:
                Debug.LogError("Logic not yet implemented for status " + Status);
                break;
        }
    }
}
