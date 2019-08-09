using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using ModelData;

public class ViewScript : MonoBehaviour
{
    
    PlaygroundViewScript playgroundScript;  // Child
    public ModelScript model;
    public void SetModeScript(ModelScript model)
    {
        this.model = model;
    }

    // ######## Unity functions #########
    // Start is called before the first frame update
    void Start()
    {
        //Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ######## Public methods #######

    // #### Events and Event Handlers ####

    public void Initialize()
    {
        // Get children
        playgroundScript = transform.Find("Playground").gameObject.GetComponent<PlaygroundViewScript>();    // Messsss

        // Check horizonal connections
        if (model == null)
            throw new ArgumentNullException("ModelScript reference is not set");

        // Initialize children
        playgroundScript.Initialize();
    }

    public void UpdateView()
    {
        // Update children
        playgroundScript.UpdateView();
    }

    public void DataChangedHandler()
    {
        // Here the View respond to changes in the Data.
        // If something is changed in the game state, then this method is immediately called.
        playgroundScript.SetRatCells(getRatCells());
        playgroundScript.SetRatCellNames(getRatCellNames());
        playgroundScript.SetFastCircleCells(getFastCircleCells());
    }

    // ####### Utility functions #######

    ModelScript getModelScript()
    {
        return model;
    }

    // #### Methods for reading the Model ####
    List<FastCircleCell> getFastCircleCells()
    {
        return this.getModelScript().getFastCircleCells();
    }

    List<RatCellType> getRatCells()
    {
        return this.getModelScript().getRatCells();
    }

    Dictionary<RatCellType, string> getRatCellNames()
    {
        return this.getModelScript().getRatCellNames();
    }
}
