using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootScript : MonoBehaviour
{
    ModelScript model;
    ViewScript view;
    ControllerScript controller;

    // ############ Unity functions ################
    // Start is called before the first frame update
    void Start()
    {
        Initialize();   // Initialize all objects in the tree
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ########## Function to rule them all ############
    void DoBindings()
    {
        // hard binding : view gets a reference to the model
        view.SetModeScript(model);

        // Run initialization on subobjects
        view.Initialize();
        model.Initialize();
        controller.Initialize();

        // soft binding: Model registers a callback to View
        model.OnDataChanged += view.DataChangedHandler;     // event assignment (binding)

        // Perform initial view update
        view.UpdateView();
    }

    public void Initialize()
    {
        model = this.transform.Find("Model").gameObject.GetComponent<ModelScript>();    // Nice Unity object referencing
        view = this.transform.Find("View").gameObject.GetComponent<ViewScript>();
        controller = this.transform.Find("Controller").gameObject.GetComponent<ControllerScript>();

        if (model == null)
            throw new KeyNotFoundException("Failed to assign \"ModelScript\"");
        if (view == null)
            throw new KeyNotFoundException("Failed to assign \"ViewScript\"");
        if (controller == null)
            throw new KeyNotFoundException("Failed to assign \"ControllerScript\"");

        DoBindings();
    }
}
