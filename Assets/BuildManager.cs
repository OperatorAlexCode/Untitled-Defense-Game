using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject standardMinePrefab;

    void Start ()
    {
        mineToBuild = standardMinePrefab;
    }

    private GameObject mineToBuild;

    public GameObject GetMineToBuild()
    {
        return mineToBuild;
    }
}
