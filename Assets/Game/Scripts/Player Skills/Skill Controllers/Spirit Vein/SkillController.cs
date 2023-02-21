using UnityEngine;

public partial class SkillController : MonoBehaviour
{
    private void Awake()
    {
        PlayerStats.Setup();
    }

    void Start()
    {
        PlayerStats.Display();
    }

    private void Update()
    {
        RunTimerSkills();
    }
}
