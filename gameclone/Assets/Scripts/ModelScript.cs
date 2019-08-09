using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ModelData;

public class ModelScript : MonoBehaviour
{
    // ######## Data ##########

    GameStaticData GameSD;  // child

    // ######## Unity functions #########
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ######## Public methods #########

    public void Initialize()
    {
        // Get children
        GameObject game = this.transform.Find("Game").gameObject;
        GameSD = game.GetComponent<GameStaticData>();

        // Initialize children
        GameSD.Initialize();
    }

    public List<FastCircleCell> getFastCircleCells()
    {
        return this.getGameSD().getFastCircleCells();
    }

    public List<RatCellType> getRatCells()
    {
        return this.getGameSD().getRatCells();
    }

    public Dictionary<RatCellType, string> getRatCellNames()
    {
        return this.getGameSD().getRatCellNames();
    }

    // ######### Events and event handlers ###########
    public delegate void DataChanged();
    public event DataChanged OnDataChanged;

    public void DataChangedHandler()
    {
        OnDataChanged();    // just forward the update event further
    }

    // ######### Utility functions ############

    GameStaticData getGameSD()
    {
        return GameSD;
    }
}
