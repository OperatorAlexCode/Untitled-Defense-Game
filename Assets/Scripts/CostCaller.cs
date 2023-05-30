using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostCaller : MonoBehaviour
{
    public ShotType shotToUpgrade;
    public CostType costType;
    public ResourceType resourceType;
    CannonController Cannon;
    UIManager UI;
    bool Run;

    void Awake()
    {
        Cannon = GameObject.Find("Cannon").GetComponent<CannonController>();
        UI = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    private void Update()
    {
        if (Run)
            UI.DisplayCost(Cannon.GetCost(shotToUpgrade, costType), resourceType);
    }

    public void Execute(bool run)
    {
        Run = run;
    }
}