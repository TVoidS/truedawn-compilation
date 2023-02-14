using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class SkillController : MonoBehaviour
{
    [Header("Spirit Vein Qi Generation")]
    public TextMeshProUGUI qiCountDisplay;

    [Header("Spirit Vein Qi Conversion")]
    public TextMeshProUGUI issDisplay;
    public TMP_Dropdown qiConvertSelector;

    [Header("Spirit Vein System Points")]
    public TextMeshProUGUI sysPointsDisp;
    public Button sellSlag;
}
