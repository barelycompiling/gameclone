using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using ModelData;

[System.Serializable]
public class GameStaticData : MonoBehaviour
{
    private string gameDataFilePath = "StreamingAssets/gamecells.json";

    // ########### Data ###########
    [SerializeField]
    private List<RatCellType2Name> RatCellNames;    // Dictionary cannot be serialized (WAT?). So a list is kept instead.

    private Dictionary<RatCellType, string> RatCellNamesDict;  // Cached RatCellNames in a Dictionary form

    [SerializeField]
    private List<FastCircleCell> FastCircleCells;

    public List<RatCellType> getRatCells()
    {
        return new List<RatCellType>
        {
            RatCellType.Opportynity,
            RatCellType.Doodle,
            RatCellType.Opportynity,
            RatCellType.Charity,
            RatCellType.Opportynity,
            RatCellType.CashDay,
            RatCellType.Opportynity,
            RatCellType.Market,
            RatCellType.Opportynity,
            RatCellType.Doodle,
            RatCellType.Opportynity,
            RatCellType.Child,
            RatCellType.Opportynity,
            RatCellType.CashDay,
            RatCellType.Opportynity,
            RatCellType.Market,
            RatCellType.Opportynity,
            RatCellType.CashDay,
            RatCellType.Opportynity,
            RatCellType.Market,
            RatCellType.Opportynity,
            RatCellType.Doodle,
            RatCellType.Opportynity,
            RatCellType.LostTheJob,
            RatCellType.Opportynity,
            RatCellType.CashDay,
            RatCellType.Opportynity,
            RatCellType.Market
        };
    }

    // ############# Public Methods ################

    public delegate void DataChanged();
    public event DataChanged OnDataChanged = delegate { };  // POSSIBLE BUG: spurious initialization, check that the event works

    public void Initialize()
    {
        LoadGameData();

        print("[MESSAGE]: Loaded FastCircleCells: " + FastCircleCells.Count.ToString());
    }

    public List<FastCircleCell> getFastCircleCells()
    {
        return FastCircleCells;
    }

    public Dictionary<RatCellType, string> getRatCellNames()
    {
        if (RatCellNamesDict == null)
        {
            // Generate the dictionary and cache it
            RatCellNamesDict = new Dictionary<RatCellType, string>();
            foreach (RatCellType2Name t2n in RatCellNames)
            {
                RatCellNamesDict[t2n.First] = t2n.Second;
            }
        }

        return RatCellNamesDict;
    }

    // ############# Unity functions ################
    public void Start()
    {
        // Should be empty
    }

    // ########### Utility functions ############

    public void genData()
    {
        RatCellNames = genRatCellNames();
        FastCircleCells = genFastCells();
    }

    private void LoadGameData()
    {
        string filePath = Path.Combine(Application.dataPath, gameDataFilePath);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(dataAsJson, this);
        }
        else
        {
            Debug.Log("Unable to load game static data");
        }

        OnDataChanged();
    }

    private void SaveGameData()
    {

        string dataAsJson = JsonUtility.ToJson(this, true);

        string filePath = Path.Combine(Application.dataPath, gameDataFilePath);
        File.WriteAllText(filePath, dataAsJson);

        Debug.Log("Saved game static data to " + filePath);
        Debug.Log("Size of rat cells was equal to " + RatCellNames.Count.ToString());
    }

    // This function can be used to initialize the serialization structure
    List<RatCellType2Name> genRatCellNames()
    {
        var names = new List<RatCellType2Name>();
        names.Add(new RatCellType2Name(RatCellType.CashDay, "Cash Day"));
        names.Add(new RatCellType2Name(RatCellType.Opportynity, "Opportunity"));
        names.Add(new RatCellType2Name(RatCellType.Market, "Market"));
        names.Add(new RatCellType2Name(RatCellType.Doodle, "Doodle"));
        names.Add(new RatCellType2Name(RatCellType.Charity, "Charity"));
        names.Add(new RatCellType2Name(RatCellType.Child, "Birth of a Child"));
        names.Add(new RatCellType2Name(RatCellType.LostTheJob, "Fired!"));

        return names;
    }

    // This function can be used to initialize the serialization structure
    List<FastCircleCell> genFastCells()
    {
        List<FastCircleCell> cells = new List<FastCircleCell>();
        cells.Add(new FastCircleCell(FastCellType.NonCommertial, "Save the forest"));
        cells.Add(new FastCircleCell(FastCellType.LostTheMoney, "CourtProcess"));

        return cells;
    }
}
