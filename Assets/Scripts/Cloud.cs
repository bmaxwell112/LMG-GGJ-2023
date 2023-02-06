using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] Sprite[] cloudSprites;
    [SerializeField] float speed = 1f;
    [SerializeField] float speedModifier = 0.25f;
    SpriteRenderer spriteRenderer;
    
    public void Initialize(float speed)
    {        
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.speed = speed;
        int sprite = Random.Range(0, cloudSprites.Length);
        spriteRenderer.sprite = cloudSprites[sprite];
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        transform.Translate(Vector3.down * (speed * speedModifier) * Time.deltaTime);
        if(transform.position.x > 30){
            Destroy(gameObject);
        }
    }
}
