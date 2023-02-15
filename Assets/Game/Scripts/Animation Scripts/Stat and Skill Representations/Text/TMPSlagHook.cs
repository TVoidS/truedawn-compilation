using TMPro;
using UnityEngine;

public class TMPSlagHook : TMPStatHook, ISlagTextDisplay
{
    [SerializeField]
    private SlagTypes DisplayedSlagType;

    // ISlagTextDisplay needed Additions
    public SlagTypes SlagType => DisplayedSlagType;
}
