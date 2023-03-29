using UnityEngine;
using UnityEngine.UI;

public class ProgressBarHook : MonoBehaviour, IGeneralizedProgressDisplay
{
    // Required Interface Implementations

    /// <summary>
    /// Defines which skill the display will handle.
    /// Required for the class to work.
    /// </summary>
    [SerializeField]
    private SkillEnums.Skill Skill;

    public SkillEnums.Skill DisplayedSkill => Skill;

    private float _MaxValue;
    public float MaxValue => _MaxValue;

    private float _Value;
    public float Value => _Value;

    public void UpdateMaxValue(float newMax)
    {
        _MaxValue = newMax;
    }

    public void UpdateValue(float newValue)
    {
        _Value = newValue;
        // TODO: Start the display function. May not be a todo? might already be done.
    }

    public void AddValue(float value) 
    {
        _Value += value;
    }

    // Internal Code for manipulating designated display:

    Slider slider;
    public void Start()
    {
        _MaxValue = 1f;
        // Attatch to the object's slider
        slider = GetComponent<Slider>();

        // Register Display to SkillController
        SkillController.AttatchProgressDisplay(Skill, this);
    }

    public void Update()
    {
        slider.value = (_Value / _MaxValue);
    }
}
