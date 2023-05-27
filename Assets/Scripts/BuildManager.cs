using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject standardMinePrefab;
    public GameObject advancedMinePrefab;
    public GameObject expertMinePrefab;

    private MineBlueprint mineToBuild;

    public bool CanBuild { get { return mineToBuild != null; } }
    public bool HasGold { get { return GM.Resources[ResourceType.iron] >= mineToBuild.cost; } }

    public GameManager GM;
    public void BuildMineOn(Node node)
    {
        if (GM.Resources[ResourceType.iron] < mineToBuild.cost)
        {
            Debug.Log("Not enough gold!");
            return;
        }

        GM.Resources[ResourceType.iron] -= mineToBuild.cost;

        GameObject mine = (GameObject)Instantiate(mineToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);

        Debug.Log("Mine built! Gold left: " + GM.Resources[ResourceType.iron]);
    }

    public void SelectMineToBuild(MineBlueprint mine)
    {
        mineToBuild = mine;
    }
}
