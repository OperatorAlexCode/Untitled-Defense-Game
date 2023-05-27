using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Miner : MonoBehaviour
{
    ResourceNode ParentResourceNode;
    Node ParentNode;

    void Awake()
    {
        ParentNode = GetComponentInParent<Node>();
        ParentResourceNode = GetComponentInParent<ResourceNode>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ParentResourceNode.MinerCollisionCheck(collision);
    }

    private void OnMouseDown()
    {
        ParentNode.OnMouseDown();
    }
}
