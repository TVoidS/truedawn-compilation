using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    // TODO: Make this not a dropdown, but instead a list of prefab "slots" that will
    public TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate() {
            int saveName = dropdown.value;
            SaveLoad.Save("slot"+saveName);
        });
    }

    // TODO:
    // Render a quick confirmation window when a potential overwrite is detected.
}
