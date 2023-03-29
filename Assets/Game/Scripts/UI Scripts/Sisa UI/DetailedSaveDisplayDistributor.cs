using TMPro;
using UnityEngine;

public class DetailedSaveDisplayDistributor : MonoBehaviour
{
    // These are to be filled in the editor, and updated when a save is selected.
    // Will display the current save data by default.
    public TextMeshProUGUI Name;
    public TextMeshProUGUI QiPurity;
    public TextMeshProUGUI SlagTier;
    public TextMeshProUGUI SlagQuantity;
    public TextMeshProUGUI LastSaveTime;

    private SaveData currDisplay;

    /// <summary>
    /// Fills the display with the detailed data from a SaveData object
    /// </summary>
    /// <param name="saveData"> The detailed data to display. </param>
    public void Distribute(SaveData saveData)
    {
        currDisplay = saveData;

        Name.SetText(saveData.Name);
        QiPurity.SetText("T" + saveData.PurityTier + "G" + saveData.PurityGrade);
        SlagTier.SetText(EnumDescriptions.ToDiscriptionString(saveData.HighestSlagTier));
        SlagQuantity.SetText(saveData.SlagQuantity + "");
        LastSaveTime.SetText(saveData.LastSaveTime.ToString());
    }

    /// <summary>
    /// Attempts to load the currently selected save.
    /// It will fail if you have selected a save that shares the same name as your current character.
    /// </summary>
    public void LoadSave() 
    {
        if (currDisplay.Name == PlayerStats.Name) 
        {
            // DON'T LOAD CURRENT SAVE
            // THEY AREN'T ALLOWED TO BREAK THE CONTINUITY!
        }
        else 
        {
            SaveLoad.LoadSave(currDisplay.Path);
            // NOTE?: Potentially load the scene again? idk.
            // At least trigger the scene update.
        }
    }

    /// <summary>
    /// The current SaveData.
    /// This is non-editable.
    /// </summary>
    public SaveData SaveData { get { return currDisplay; } }
}
