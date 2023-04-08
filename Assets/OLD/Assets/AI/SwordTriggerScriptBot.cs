using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTriggerScriptBot : MonoBehaviour
{
    public Vector2 SparksPos;

    public PlayerScript playerScript;
    public BoxCollider2D hitbox;
    public GameObject Sparks;
    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        transform.position = playerScript.pos;

        if (playerScript.sidePos == false)
        {
            hitbox.offset = new Vector2(-0.7f, hitbox.offset.y);
        }

        if (playerScript.sidePos == true)
        {
            hitbox.offset = new Vector2(0.6f, hitbox.offset.y);
        }

        if (playerScript.groundcheck == true)
        {
            hitbox.offset = new Vector2(hitbox.offset.x, -0.2f);
        }

        if (playerScript.groundcheck == false)
        {
            hitbox.offset = new Vector2(hitbox.offset.x, 0.3f);
        }

        SparksPos = new Vector2(transform.position.x + hitbox.offset.x, transform.position.y + hitbox.offset.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "swordtrigger")
        {
            playerScript.SwordCrossForce();
            Instantiate(Sparks, SparksPos, Quaternion.identity);
        }
    }
}
