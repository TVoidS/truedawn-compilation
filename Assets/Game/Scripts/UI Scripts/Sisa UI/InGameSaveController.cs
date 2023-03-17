using UnityEngine;

public class InGameSaveController : MonoBehaviour
{
    public DetailedSaveDisplayDistributor DetailedSaveDisplay;

    /// <summary>
    /// Fills the DetailedSaveDisplay gameObject's children with the data from a SaveData struct.
    /// </summary>
    /// <param name="save"> The SaveData struct with the rough rundown. </param>
    public void FillDetailedSaveDisplay(SaveData save) 
    {
        DetailedSaveDisplay.Distribute(save);
    }

    /// <summary>
    /// Fills the DetailedSaveDisplay with the current game's save data, despite there not being a save.
    /// This will activate whenever the screen is enabled, so it should happen whenever the window is open.
    /// </summary>
    private void OnEnable()
    {
        DetailedSaveDisplay.Distribute(SaveLoad.CurrentSaveDetails());
    }
}
