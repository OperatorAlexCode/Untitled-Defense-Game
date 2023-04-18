using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color insufficientGoldColor;
    public Vector3 positionOffset;
    [Header("Optional")]
    public GameObject mine;
    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (mine != null)
        {
            Debug.Log("Can not build on an existing mine!");
            return;
        }

        buildManager.BuildMineOn(this);
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasGold)
        {
            rend.material.color = hoverColor;
        } else
        {
            rend.material.color = insufficientGoldColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
