using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTriggerScript : MonoBehaviour
{
    public Vector2 SparksPos;

    public PlayerScript playerScript;
    public BoxCollider2D swordTrigger;
    public GameObject Sparks;

    private void Awake()
    {
        if(gameObject.name == "swordtrigger2" && SettingsScript.isBotEnabled == true)
        {
            playerScript = GameObject.Find("Bot").GetComponent<PlayerScript>();
        }
    }
    void FixedUpdate()
    {
        transform.position = playerScript.pos;
        
        if (playerScript.sidePos == false)
        {
            swordTrigger.offset = new Vector2(-0.7f, swordTrigger.offset.y);
        }

        if (playerScript.sidePos == true)
        {
            swordTrigger.offset = new Vector2(0.6f, swordTrigger.offset.y);
        }

        if (playerScript.groundcheck == true)
        {
            swordTrigger.offset = new Vector2(swordTrigger.offset.x, -0.2f);
        }

        if (playerScript.groundcheck == false)
        {
            swordTrigger.offset = new Vector2(swordTrigger.offset.x, 0.3f);
        }

        SparksPos = new Vector2(transform.position.x + swordTrigger.offset.x, transform.position.y + swordTrigger.offset.y);
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
