using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillControlls : MonoBehaviour
{

    Event e;
    // Update is called once per frame
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
}
