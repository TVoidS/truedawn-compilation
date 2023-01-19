using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    //TODO: Make this the entire character stat and skill screen for all games. :D

    [Header("Spirit Vein UI Button Objects")]
    public Button test;

    [Header("Spirit Vein Qi Generation")]
    public TextMeshProUGUI qiCountDisplay;

    SkillControlls skill;

    protected long qiCount;

    // Start is called before the first frame update
    void Start()
    {
        
        qiCount = 0;
        // TODO: Automatically form a collection of buttons that meet a certain criteiria (have a specific component attatched)
        // That list will be used for connecting to skills or anything else, based on the ID of the attatched component
        Button testBtn = test.GetComponent<Button>();
        testBtn.onClick.AddListener(TestPrint);
        skill = gameObject.AddComponent<SkillControlls>();
    }

    void TestPrint() 
    {
        Debug.Log("Test Success");
        
    }

    // Update is called once per frame
    void Update()
    {
        skill.check();
    }

    private void FixedUpdate()
    {
        qiCount += 1;
        qiCountDisplay.SetText(qiCount+"");
    }
}
