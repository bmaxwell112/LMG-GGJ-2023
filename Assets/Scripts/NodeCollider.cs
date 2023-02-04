using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCollider : MonoBehaviour
{
    RootNode node;
    // Start is called before the first frame update
    void Start()
    {
        node = GetComponentInParent<RootNode>();
    }

    private void OnMouseDown() {
        MakeActiveNode();
    }

    private void MakeActiveNode()
    {
        Root.activeNode = node;
    }
}
