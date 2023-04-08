using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    /* Bot's Control Codes
 * 
 * controlCode == 6 -- MoveRight
 * controlCode== 4 -- MoveLeft
 * controlCode == 8 -- Jump
 * controlCode == 5 -- Shot
 * controlCode == 1 -- SwordAttack
 */

    public Vector2 spawnPos;
    public bool hitCooldown = false;
    public GameObject spawn, blood;
    public GameScore score;
    public GameObject SecondPl;
    public AudioSource deathSound;
    public PlayerScript PlS;
    public Bullet bul;
    public int controlCode;

    void Start()
    {
        PlS = GetComponent<PlayerScript>();
    }
    void Awake()
    {
        if (SettingsScript.isBotEnabled == true)
            SecondPl.SetActive(false);

        PlS.rigidBody.position = spawn.transform.position;
        PlS.animator.Play("fall");
    }
    void Update()
    {
        if (controlCode == 6)
        {
            PlS.MoveRight();
            PlS.isMoveKeyDown = true;
        }

        if (controlCode == 4)
        {
            PlS.MoveLeft();
            PlS.isMoveKeyDown = true;
        }

        if (controlCode == 6 || controlCode == 4)
        {
            PlS.isMoveKeyDown = false;
        }

        if (controlCode == 8 && PlS.jumpCount > 0)
        {
            PlS.Jump();
        }

        if (controlCode == 5 && PlS.bulletCooldown == true)
        {
            PlS.Shot();
        }

        if (controlCode == 1 && hitCooldown == false)
        {
            PlS.SwordHit();
            hitCooldown = true;
            StartCoroutine(HitCooldown());
        }
        

        spawnPos = spawn.transform.position;
    }

    public void Respawn()
    {
        PlS.rigidBody.position = spawnPos;
        PlS.rigidBody.velocity = new Vector2(0, 0);

        Instantiate(blood, new Vector3(PlS.pos.x, PlS.pos.y, -1), Quaternion.identity);

        score.player1Score += 1;
        PlS.jumpCount = 0;

        deathSound.pitch = Random.Range(0.9f, 1.1f);
        deathSound.Play();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Respawn();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "swordtrigger1")
        {
            Respawn();
        }
    }

    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(1f);
        hitCooldown = false;
    }

    

}
