using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using ModelData;

public class PlaygroundViewScript : MonoBehaviour
{
    // ####### Data #######
    // constants
    const int indexOffset = -3;
    const double innerRadius = 1.0;
    const double outerRadius = 2.0;
    const double boundaryVal = 0.1;     // space between rat cells

    // These fields are set externally
    public GameObject playgroundElementPrefab;

    public Material cashdayMat;
    public Material opportunityMat;
    public Material marketMat;
    public Material doodleMat;
    public Material charityMat;
    public Material childMat;
    public Material firedMat;

    // Should be modified externally
    List<Material> ratCircleMats;
    List<FastCircleCell> fastCircleCellData;
    List<RatCellType> ratCellData;
    Dictionary<RatCellType, string> ratCellNames;

    // Generated at start
    List<GameObject> ratCells;
    List<Vector3> ratCellCenters;   // center position of each rat cell

    // ####### Unity functions #######

    // Start is called before the first frame update
    void Start()
    {
        // Should be empty
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ####### Utility functions #######

    // Generates small circle meshes
    void InstantiateRatCircle()
    {
        int numOfCells = ratCircleMats.Count;
        double angleStep = 2 * Math.PI / numOfCells;

        for (int iter = 0; iter < numOfCells; iter++)
        {
            int i = indexOffset + iter;

            // Instantiate locally the playground element
            GameObject instance = GameObject.Instantiate(playgroundElementPrefab, this.transform);

            // Generate the mesh
            MeshGenerator generator = instance.GetComponent<MeshGenerator>();
            double phi = angleStep * i;
            generator.SetRoundedMesh(outerRadius, innerRadius, phi - (0.5 - boundaryVal/2) * angleStep, phi + (0.5 - boundaryVal/2) * angleStep);

            instance.GetComponent<MeshRenderer>().material = ratCircleMats[numOfCells - iter - 1];  // inverted indexes as the order of movement is clockwise
            instance.name = "ratcell" + iter.ToString();

            ratCells.Add(instance);
            double centerR = outerRadius / 2 + innerRadius / 2;
            
            double cx = centerR * Math.Cos(phi);
            double cy = centerR * Math.Sin(phi);
            ratCellCenters.Add(new Vector3((float) cx, (float) cy, 0.0f));
        }
    }

    void InstantiateFastCircle()
    {

    }

    // ######### Hardcoded part ###########
    void InitMatList()
    {
        ratCircleMats = new List<Material>
        {
            opportunityMat,
            doodleMat,
            opportunityMat,
            charityMat,
            opportunityMat,
            cashdayMat,
            opportunityMat,
            marketMat,
            opportunityMat,
            doodleMat,
            opportunityMat,
            childMat,
            opportunityMat,
            cashdayMat,
            opportunityMat,
            marketMat,
            opportunityMat,
            cashdayMat,
            opportunityMat,
            marketMat,
            opportunityMat,
            doodleMat,
            opportunityMat,
            firedMat,
            opportunityMat,
            cashdayMat,
            opportunityMat,
            marketMat
        };
    }

    // ########### Public functions ###########

    public void Initialize()
    {
        // Initialize fields
        ratCircleMats = new List<Material>();
        ratCells = new List<GameObject>();
        ratCellCenters = new List<Vector3>();
    }

    public void UpdateView()
    {
        // TODO: perform object reinstantiation

        // Create objects
        InitMatList();
        InstantiateRatCircle();
    }

    // ####### Model reading #######
    public void SetFastCircleCells(List<FastCircleCell> cells)
    {
        fastCircleCellData = cells;
    }

    public void SetRatCells(List<RatCellType> cells)
    {
        ratCellData = cells;
    }

    public void SetRatCellNames(Dictionary<RatCellType, string> dict)
    {
        ratCellNames = dict;
    }
}
