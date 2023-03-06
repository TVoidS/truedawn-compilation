using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    public TMP_InputField saveNameInput;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate() {
            string saveName = saveNameInput.text;
            if (saveName != "")
            {
                SaveLoad.Save(saveName);
            }
            else 
            {
                // Display an Invalid Save Name in the field, or just save to QuickSave
                SaveLoad.Save("QuickSave");
            }
        });
    }
}
