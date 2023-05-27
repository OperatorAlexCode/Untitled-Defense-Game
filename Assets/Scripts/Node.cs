using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color insufficientGoldColor;
    public Vector3 positionOffset;
    [Header("Optional")]
    private Renderer rend;
    private Color startColor;
    public GameObject Miner;
    public GameObject hoverText;

    [SerializeField]
    int constructionCost = 200;

    GameManager gameManager;

    ResourceNode RN;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        RN = gameObject.GetComponent<ResourceNode>();
        Miner = gameObject.transform.Find("Miner").gameObject;

        //hoverText.SetActive(false);
    }

    private void Update()
    {
        if (!RN.IsMined && Miner.activeSelf)
            Miner.SetActive(false);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Miner.activeSelf)
        {
            Debug.Log("Can not build on an existing mine!");
            return;
        }
        else if (gameManager.Resources[ResourceType.iron] >= constructionCost && !gameManager.InWave)
        {
            Miner.SetActive(true);
            RN.BuildMiner();
            gameManager.Resources[ResourceType.iron] -= constructionCost;
        }
    }

    void OnMouseEnter()
    {
        if (!RN.IsMined && !gameManager.InWave)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (gameManager.Resources[ResourceType.iron] >= constructionCost)
            {
                rend.material.color = hoverColor;
            }
            else
            {
                rend.material.color = insufficientGoldColor;
            }
        }

        //hoverText.SetActive(true);
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;

        //hoverText.SetActive(false);
    }
}
