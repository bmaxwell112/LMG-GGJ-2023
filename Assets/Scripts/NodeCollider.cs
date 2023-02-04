using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCollider : MonoBehaviour
{
    RootNode node;
    [SerializeField] Sprite[] sprites;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        node = GetComponentInParent<RootNode>();
        sr = GetComponentInChildren<SpriteRenderer>();
        int r = Random.Range(0, sprites.Length-1);
        sr.sprite = sprites[r];        
    }

    private void OnMouseDown() {
        MakeActiveNode();
    }

    private void MakeActiveNode()
    {
        Root.ChangeActiveNode(node);
        // Root.activeNode = node;
    }
}
