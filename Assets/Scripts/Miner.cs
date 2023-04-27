using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    ResourceNode ParentNode;

    private void OnCollisionEnter(Collision collision)
    {
        if (ParentNode == null)
            ParentNode = GetComponentInParent<ResourceNode>();

        ParentNode.MinerCollisionCheck(collision);
    }
}
