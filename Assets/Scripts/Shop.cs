using UnityEngine;

public class Shop : MonoBehaviour
{
    public MineBlueprint standardMine;
    public MineBlueprint advancedMine;
    public MineBlueprint expertMine;

    BuildManager buildManager;

    //public bool showButtons = true;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardMine()
    {
        Debug.Log("Standard Mine Selected");
        buildManager.SelectMineToBuild(standardMine);
    }
    
    public void SelectAdvancedMine()
    {
        Debug.Log("Advanced Mine Selected");
        buildManager.SelectMineToBuild(advancedMine);
    }

    public void SelectExpertMine()
    {
        Debug.Log("Expert Mine Selected");
        buildManager.SelectMineToBuild(expertMine);
    }
}
