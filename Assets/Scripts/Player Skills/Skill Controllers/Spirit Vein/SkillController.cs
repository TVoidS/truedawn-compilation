using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillController : MonoBehaviour
{

    [Header("Spirit Vein UI Button Objects")]
    public Button qiConvertBtn;

    [Header("Spirit Vein Qi Generation")]
    public TextMeshProUGUI qiCountDisplay;

    private QiRegen qiRegener;

    private void Start()
    {
        // TODO: Automatically form a collection of buttons that meet a certain criteiria (have a specific component attatched)
        // That list will be used for connecting to skills or anything else, based on the ID of the attatched component
        Button qiConvertBtn_ = qiConvertBtn.GetComponent<Button>();
        qiConvertBtn_.onClick.AddListener(TestPrint);

        PlayerStats.setup(gameObject);

        // This will instead be handled by a Skill Initiator later.
        float qiRegenTime = 10f;


        skillSetup();

        startAlwaysOnPassives();
    }

    private void skillSetup()
    {
        // Make this load from PlayerStats file
        qiRegener = new QiRegen(0, 0);
    }

    private void startAlwaysOnPassives()
    {
        Invoke("qiRegener.regen", qiRegener.cooldown);
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
