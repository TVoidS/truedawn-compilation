using UnityEngine;

public class DisplayUpdater : MonoBehaviour
{
    void Start()
    {
        PlayerStats.Display();
    }

    private void Update()
    {
        SkillController.RunTimerSkills();
    }
}
