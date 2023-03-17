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


    /// <summary>
    /// Fills the display with the detailed data from a SaveData object
    /// </summary>
    /// <param name="saveData"> The detailed data to display. </param>
    public void Distribute(SaveData saveData)
    {
        Name.SetText(saveData.Name);
        QiPurity.SetText("T" + saveData.PurityTier + "G" + saveData.PurityGrade);
        SlagTier.SetText(EnumDescriptions.ToDiscriptionString(saveData.HighestSlagTier));
        SlagQuantity.SetText(saveData.SlagQuantity + "");
        LastSaveTime.SetText(saveData.LastSaveTime.ToString());
    }
}
