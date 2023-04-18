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
    public bool HasGold { get { return PlayerStats.Gold >= mineToBuild.cost; } }

    public void BuildMineOn(Node node)
    {
        if (PlayerStats.Gold < mineToBuild.cost)
        {
            Debug.Log("Not enough gold!");
            return;
        }

        PlayerStats.Gold -= mineToBuild.cost;

        GameObject mine = (GameObject)Instantiate(mineToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.mine = mine;

        Debug.Log("Mine built! Gold left: " + PlayerStats.Gold);
    }

    public void SelectMineToBuild(MineBlueprint mine)
    {
        mineToBuild = mine;
    }
}
