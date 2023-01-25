using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillController : MonoBehaviour
{

    [Header("Spirit Vein Qi Generation")]
    public TextMeshProUGUI qiCountDisplay;
    public Slider qiRegenProgressBar;

    [Header("Spirit Vein Qi Conversion")]
    public TextMeshProUGUI issDisplay;
    public Slider qiConvertSlider;
    public Button qiConvertBtn;
    public TMP_Dropdown qiConvertSelector;

    private QiRegen qiRegener;
    private QiConvert qiConverter;

    private void Start()
    {
        // TODO: Automatically form a collection of buttons that meet a certain criteiria (have a specific component attatched)
        // That list will be used for connecting to skills or anything else, based on the ID of the attatched component
        Button qiConvertBtn_ = qiConvertBtn.GetComponent<Button>();
        qiConvertBtn_.onClick.AddListener(TestPrint);

        PlayerStats.setup(gameObject, qiCountDisplay, issDisplay);

        skillSetup();

        startAlwaysOnPassives();
    }

    private void skillSetup()
    {
        // Make this load from PlayerStats file
        qiRegener = new QiRegen(0, 0, qiRegenProgressBar);
        qiConverter = new QiConvert(qiConvertSlider, qiConvertSelector);
    }

    private void Update()
    {
        QiCount.add(qiRegener.regen());
        qiConverter.convert();
        
    }

    private void startAlwaysOnPassives()
    {

    }

    public void check()
    {
        if (Input.GetKeyDown("n")) 
        {
            SkillExecute("N");
        }
    }

    void SkillExecute(string name) 
    {
        Debug.Log(name + " has been pressed");
    }
    void TestPrint()
    {
        Debug.Log("Test Success");

    }
}
