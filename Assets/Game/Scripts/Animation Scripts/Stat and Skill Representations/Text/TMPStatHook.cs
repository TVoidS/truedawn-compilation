using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class TMPStatHook : TMPHook, IStatTextDisplay
{
    // IGeneralizedTextHook Implementations:
    [field: SerializeField]
    public virtual StatEnums Stat { get; protected set; }
}
