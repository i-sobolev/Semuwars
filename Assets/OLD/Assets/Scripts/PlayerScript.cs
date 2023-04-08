using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float acceleration, jumpForce;
    public Vector2 moveDirection, jumpDirection, pos, bulletInstPos;
    public int jumpCount;
    public bool sidePos, bulletCooldown = true, isMoveKeyDown = false, groundcheck = false;
    public Rigidbody2D rigidBody;
    public GameObject bullet;
    public Bullet bulletScript;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public GameObject swordTrigger;
    public SwordTriggerScript swordTrigerSctipt;

    public AudioSource swordCrossSound, swordHitSound, bulletHitSound, fallSound;
    //public Vector2 viewportPoint;
    //public float stereoPan;
    //public Camera cam;

    public bool Buff0 = false, Buff1 = false, Buff2 = false;
    public GameObject buffKunai1, buffKunai2;
    public Bullet buffKunaiScript1, buffKunaiScript2;
    // 0 - буст скорости куная; 1 - множество кунаев; 2 - буст скорости передвижения;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        buffKunaiScript1 = buffKunai1.GetComponent<Bullet>();
        buffKunaiScript2 = buffKunai2.GetComponent<Bullet>();
    }

    void FixedUpdate()
    {
        //viewportPoint = cam.ScreenToViewportPoint(transform.position);// 3d звук
        //stereoPan = viewportPoint.x;

        pos = rigidBody.position;

        bulletScript.speed = 8;

        if (bulletScript.speed > acceleration)
            bulletScript.speed = acceleration + 2;
        

        spriteRenderer.flipX = !sidePos;

        if (sidePos == true)
        {
            bulletInstPos = new Vector2(pos.x + 1, pos.y);
            buffKunaiScript1.nVec = buffKunaiScript1.vecPlus;
            buffKunaiScript2.nVec = buffKunaiScript2.vecPlus;
        }

        if (sidePos == false)
        {
            bulletInstPos = new Vector2(pos.x - 1, pos.y);
            buffKunaiScript1.nVec = buffKunaiScript1.vecMinus;
            buffKunaiScript2.nVec = buffKunaiScript2.vecMinus;
        }

        if (rigidBody.velocity.x > 15)
            rigidBody.velocity = new Vector2 (15, rigidBody.velocity.y);
        
        if (rigidBody.velocity.x < -15)
            rigidBody.velocity = new Vector2(-15, rigidBody.velocity.y);
        
        animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
        animator.SetBool("IsMoveKeysDown", isMoveKeyDown);
        animator.SetBool("GroundCheck", groundcheck);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground" || other.gameObject.tag == "Player")
            jumpCount = 2;
        
        if (other.gameObject.tag == "ground")
        {
            animator.Play("up");
            groundcheck = true;

            fallSound.pitch = Random.Range(0.5f, 1.5f);
            fallSound.Play();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            groundcheck = false;
        }
    }
    

    public void MoveRight()
    {
        rigidBody.AddForce(moveDirection.normalized * acceleration);
        sidePos = true;

        if(Buff2 == true)
        {
            rigidBody.velocity = new Vector2(15f, rigidBody.velocity.y);
        }
    }

    public void MoveLeft()
    {
        rigidBody.AddForce(-moveDirection.normalized * acceleration);
        sidePos = false;

        if (Buff2 == true)
        {
            rigidBody.velocity = new Vector2(-15f, rigidBody.velocity.y);
        }
    }

    public void Jump()
    {
        rigidBody.AddForce(jumpDirection.normalized * jumpForce);
        jumpCount -= 1;
        animator.Play("jump");
    }

    public void Shot()
    {
        if (Buff0 == true)
            bulletScript.speed = 16;

        Instantiate(bullet, bulletInstPos, Quaternion.identity);
        
        if (Buff1 == true)
        {
            Instantiate(buffKunai1, new Vector2(bulletInstPos.x, bulletInstPos.y + 1f), Quaternion.identity);
            Instantiate(buffKunai2, new Vector2(bulletInstPos.x, bulletInstPos.y - 1f), Quaternion.identity);
        }

        bulletCooldown = false;
        Buff0 = false;
        Buff1 = false;

        StartCoroutine(ShotCooldown());
        bulletHitSound.pitch = Random.Range(0.9f, 1.1f);
        bulletHitSound.Play();

        

        if (groundcheck == false && jumpCount < 2)
        {
            animator.Play("kunaifall");
        }

        if (groundcheck == true && (rigidBody.velocity.x > 0 || rigidBody.velocity.x < 0))
        {
            animator.Play("kunairun");
        }

        if (groundcheck == true && rigidBody.velocity.x == 0)
        {
            animator.Play("kunaiidle");
        }
    }

    public void SwordHit()
    {
        swordTrigerSctipt.swordTrigger.enabled = true;
        StartCoroutine(SwordTriggerTime());

        swordHitSound.pitch = Random.Range(0.9f, 1.1f);
        swordHitSound.Play();

        if (groundcheck == false && jumpCount < 2)
        {
            animator.Play("swordfall");
        }

        if (groundcheck == true && (rigidBody.velocity.x > 0 || rigidBody.velocity.x < 0))
        {
            animator.Play("swordrun");
        }

        if (groundcheck == true && rigidBody.velocity.x == 0)
        {
            animator.Play("swordidle");
        }

    }
    public void SwordCrossForce()
    {
        rigidBody.velocity = new Vector2(-rigidBody.velocity.x, rigidBody.velocity.y + 10);

        swordCrossSound.pitch = Random.Range(0.9f, 1.1f);
        swordCrossSound.Play();
    }

    IEnumerator SwordTriggerTime()
    {
        yield return new WaitForSeconds(0.1f);
        swordTrigerSctipt.swordTrigger.enabled = false;
    }

    IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(3f);
        bulletCooldown = true;
    }

    IEnumerator BoostCooldown()
    {
        yield return new WaitForSeconds(15);
        Buff2 = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Buff0")
            Buff0 = true;

        if (collision.gameObject.name == "Buff1")
            Buff1 = true;

        if (collision.gameObject.name == "Buff2")
        {
            Buff2 = true;
            StartCoroutine(BoostCooldown());
        }
    }
}
