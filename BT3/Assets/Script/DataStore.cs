using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCsharp;
using System.IO;
using UnityEngine.UI;
public class DataStore : MonoBehaviour
{
    public Dropdown playerSelect;
    public static DataStore instance;
    public void CreateDemoData()
    {
        PlayerModel playerModel = new PlayerModel(1);
        Debug.Log(PlayerModel.GetJsonFromModel(playerModel, true));
    }

    void LoadPlayerData()
    {
        TextAsset data = Resources.Load("Data") as TextAsset;
        PlayerModel.Model[] playerModel = PlayerModel.GetModelFromJson(data.text);
        Debug.Log(playerModel[0].name);
    }
    public PlayerModel.Model[] playerModel;
    void LoadPlayerData2()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Data.txt");
        string data = File.ReadAllText(path);
        playerModel = PlayerModel.GetModelFromJson(data);
        List<string> m_DropOptions = new List<string>();
        for (int i = 0; i<playerModel.Length; i++)
        {
            m_DropOptions.Add(playerModel[i].name);
        }
        playerSelect.ClearOptions();
        playerSelect.AddOptions(m_DropOptions);
    }
    void WritePlayerData()   
    {
        playerModel[0].name = "Latina";
        string path = Path.Combine(Application.streamingAssetsPath, "Data.txt");
        PlayerModel p = new PlayerModel(playerModel.Length);
        p.models = playerModel;
        File.WriteAllText(path, PlayerModel.GetJsonFromModel(p, true));
    }
    public int currentPlayerSelected;
    public void OnDropDownChange(int valua)
    {
        currentPlayerSelected = valua;
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        LoadPlayerData2();
        WritePlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
