using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool hitCooldown = false, isPlayerAttaking = false;
    public Vector2 spawnPosition;
    public PlayerScript playerScript;
    public Bullet bullet;
    public GameObject spawn, blood;
    public GameScore score;
    public AudioSource deathSound;
    public float getAxis;
    
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
    }

    void Awake()
    {
        playerScript.rigidBody.position = spawn.transform.position;
        playerScript.animator.Play("fall");
    }

    void Update()
    {
        getAxis = Input.GetAxisRaw("p1Horizontal");


        if (getAxis == 1 || Input.GetButton("p1MoveRight"))
        {
            playerScript.MoveRight();
            bullet.nVec = bullet.vecPlus;
            playerScript.isMoveKeyDown = true;
        }

        else if (getAxis == -1 || Input.GetButton("p1MoveLeft"))
        {
            playerScript.MoveLeft();
            bullet.nVec = bullet.vecMinus;
            playerScript.isMoveKeyDown = true;
        }

        else 
        {
            playerScript.isMoveKeyDown = false;
        }

        if (Input.GetButtonDown("p1Jump") && playerScript.jumpCount > 0)
            playerScript.Jump();
        
        if (Input.GetButtonDown("p1Kunai") && playerScript.bulletCooldown == true)
            playerScript.Shot();

        if (Input.GetButtonDown("p1SwordHit") && hitCooldown == false)
        {
            playerScript.SwordHit();
            hitCooldown = true;
            StartCoroutine(HitCooldown());
            isPlayerAttaking = true;
        }

        spawnPosition = spawn.transform.position;
    }

    public void Respawn()
    {
        playerScript.rigidBody.position = spawnPosition;
        playerScript.rigidBody.velocity = new Vector2(0, 0);
        
        Instantiate(blood, new Vector3(playerScript.pos.x, playerScript.pos.y, -1), Quaternion.identity);
        
        score.player2Score += 1;
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
        if (collision.gameObject.name == "swordtrigger2")
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
