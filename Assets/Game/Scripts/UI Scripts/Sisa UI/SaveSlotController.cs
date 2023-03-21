using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotContoller : MonoBehaviour
{
    private SaveData save;
    private DetailedSaveDisplayDistributor distributor;
    private TextMeshProUGUI Name;
    private TextMeshProUGUI Date;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("SlotClick").GetComponent<Button>().onClick.AddListener(() => {
            UpdateDisplay();
        });

        distributor = GameObject.FindGameObjectWithTag("detaileddistributor").GetComponent<DetailedSaveDisplayDistributor>();

        Name = gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        Date = gameObject.transform.Find("SaveTime").GetComponent<TextMeshProUGUI>();
    }

    public void Fill(SaveData data) 
    {
        save = data;
        Name.SetText(data.Name);
        Date.SetText(data.LastSaveTime.ToString());
    }

    private void UpdateDisplay() 
    {
        distributor.Distribute(save);
    }
}
