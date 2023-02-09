using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHook : MonoBehaviour, IGeneralizedProgressDisplay
{
    private float _MaxValue;
    public float MaxValue => _MaxValue;

    private float _Value;
    public float Value => _Value;

    public void UpdateMaxValue(float newMax)
    {
        _MaxValue = newMax;
    }

    public void UpdateValue(float newValue)
    {
        _Value = newValue;
        // TODO: Start the display function.
    }
}
