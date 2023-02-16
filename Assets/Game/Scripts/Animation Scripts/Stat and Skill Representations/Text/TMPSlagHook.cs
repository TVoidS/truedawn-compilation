using System.ComponentModel;
using UnityEngine;

[DisplayName("Slag Text Display")]
public class TMPSlagHook : TMPHook, ISlagTextDisplay
{
    [SerializeField]
    private SlagTypes DisplayedSlagType;

    // ISlagTextDisplay needed Additions
    public SlagTypes SlagType => DisplayedSlagType;

    
    public StatEnums Stat { get { return StatEnums.Slag; } }
}
