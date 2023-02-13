using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class SkillController : MonoBehaviour
{
    [Header("Spirit Vein Qi Generation")]
    public TextMeshProUGUI qiCountDisplay;
    public Slider qiRegenProgressBar;
    public Button QiLevelUpTrigger;

    [Header("Spirit Vein Qi Conversion")]
    public TextMeshProUGUI issDisplay;
    public Slider qiConvertSlider;
    public Button slagConvertBtn;
    public TMP_Dropdown qiConvertSelector;
    public Button conversionLevelUpTrigger;

    [Header("Spirit Vein System Points")]
    public TextMeshProUGUI sysPointsDisp;
    public Button sellSlag;
}
