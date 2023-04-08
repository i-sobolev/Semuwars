using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerController : MonoBehaviour
{
    public Vector2 spawnPos;
    public bool hitCooldown = false, isPlayerAttaking = false;
    public PlayerScript playerScript;
    public Bullet bullet;
    public GameObject spawn, blood;
    public GameObject Bot;
    public GameScore score;
    public float getAxis;

    public AudioSource deathSound;
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
    }
    void Awake()
    {
        if (SettingsScript.isBotEnabled == false)
            Bot.SetActive(false);

        playerScript.rigidBody.position = spawn.transform.position;
        playerScript.animator.Play("fall");
    }
    void Update()
    {
        getAxis = Input.GetAxisRaw("p2Horizontal");

        if (getAxis == 1 || Input.GetButton("p2MoveRight"))
        {
            playerScript.MoveRight();
            bullet.nVec = bullet.vecPlus;
            playerScript.isMoveKeyDown = true;
        }

        else if (getAxis == -1 || Input.GetButton("p2MoveLeft"))
        {
            playerScript.MoveLeft();
            bullet.nVec = bullet.vecMinus;
            playerScript.isMoveKeyDown = true;
        }

        else
        {
            playerScript.isMoveKeyDown = false;
        }

        if (Input.GetButtonDown("p2Jump") && playerScript.jumpCount > 0)
            playerScript.Jump();

        if (Input.GetButtonDown("p2Kunai") && playerScript.bulletCooldown == true)
            playerScript.Shot();

        if (Input.GetButtonDown("p2SwordHit") && hitCooldown == false)
        {
            playerScript.SwordHit();
            hitCooldown = true;
            StartCoroutine(HitCooldown());
            isPlayerAttaking = true;
        }


        spawnPos = spawn.transform.position;
    }

    public void Respawn()
    {
        playerScript.rigidBody.position = spawnPos;
        playerScript.rigidBody.velocity = new Vector2(0, 0);

        Instantiate(blood, new Vector3(playerScript.pos.x, playerScript.pos.y, -1), Quaternion.identity);

        score.player1Score += 1;
        playerScript.jumpCount = 0;

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
