using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Делать массив GameObjects С Обрывами

public class Bot : MonoBehaviour
{
    public PlayerScript PlS;
    public BotController BC;
    public Bullet bullet;
    public PlayerController plCont;
    public GameObject player, bot;
    [SerializeField]
    private Vector2 playerPos;
    private Vector2 botPos;
    private Vector2 bulletPos;
    public float swordAtackDistanceX = 1.8f, swordAtackDistanceY = 0.9f, additionalDistance = 0.5f, bulletStraightFly = 7f;
    private bool HeightLevel;
    //public Vector2[] LeftFalls  = new Vector2[] { -35.93, -9.23, 17.75 }; 
    //public float[] RightFalls = new float[] { -17.23f, 9.47f, 36.45f };

    private bool scorpActive = false;
    private int scorp = 0;

    void Awake()
    {
        if (SettingsScript.isBotEnabled == false)
            gameObject.SetActive(false);
    }
    void Start()
    {
        PlS = GetComponent<PlayerScript>();
        BC = GetComponent<BotController>();
        plCont = FindObjectOfType<PlayerController>();
    }
    //Функции передвижения(Наконец-то)()НЕТ()
    void MoveLeft()
    {
        BC.controlCode = 4;
       // if(botPos.x - > )
    }
    void MoveRight()
    {
        BC.controlCode = 6;
    }
    void Jump()
    {
        BC.controlCode = 8;
        ControlCodeCoroutine();
    }
    void DoubleJump()
    {
        BC.controlCode = 8;
        ControlCodeCoroutine();
        BC.controlCode = 8;
        ControlCodeCoroutine();
    }



    void SwordAttack() {
        if (BC.hitCooldown == false) BC.controlCode = 1;
        StartCoroutine(ControlCodeCoroutine());
    }
    /*void KunaiThrow() {
        if (PlS.bulletCooldown == true) BC.controlCode = 5;
        StartCoroutine(ControlCodeCoroutine());
    }*/
  

    void Update()
    {
        

        //Отслеживание позиции Бота \\ Игрока \\ Пули
        botPos = transform.position;
        playerPos = player.transform.position;
        bulletPos = bullet.bulletPos;

        //Проверка на крыше или нет находится БОТ
        if (botPos.y > 3.56f) HeightLevel = true;
        else HeightLevel = false;

        //Поворот в сторону игрока
        if (botPos.x - playerPos.x < 0)
        {
            PlS.sidePos = true;
        }

        else
        {
            PlS.sidePos = false;
            BC.controlCode = 6;
            ControlCodeCoroutine();
            BC.controlCode = 0;
        }


        //
        float BotPlayerDeltaPosX = Mathf.Abs(botPos.x - playerPos.x);
        float BotPlayerDeltaPosY = Mathf.Abs(botPos.y - playerPos.y) - additionalDistance;

        if (((BotPlayerDeltaPosX) < swordAtackDistanceX) && ((BotPlayerDeltaPosY) < swordAtackDistanceY))
        {
                SwordAttack();
        }


        //Пока так двигается)))
        if (Input.GetKeyDown(KeyCode.S)) scorp *= 1;
        if (Input.GetKeyDown(KeyCode.C)) scorp *= 2;
        if (Input.GetKeyDown(KeyCode.A)) scorp *= 3;
        if (Input.GetKeyDown(KeyCode.R)) scorp *= 4;
        if (Input.GetKeyDown(KeyCode.P)) scorp *= 5;
        if (Input.GetKeyDown(KeyCode.I)) scorp *= 6;
        if (Input.GetKeyDown(KeyCode.O)) scorp *= 7;
        if (Input.GetKeyDown(KeyCode.N)) scorp *= 8;
        if (Input.GetKeyDown(KeyCode.RightControl)) scorp = 1;
        if (scorp >= 40321) scorp = 0;
        Debug.Log(scorp);
        if (scorp == 40320)
        {
            scorpActive = true; 
        }

        if (scorpActive == true)
        {
            if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.F))) HellPortal();
        }


         //Парирование (Меч) 
        if ((Mathf.Abs(BotPlayerDeltaPosX) < (swordAtackDistanceX + additionalDistance)))
        {
            if (Input.GetKeyDown(KeyCode.F)) SwordAttack();        
        }

        // Бросок вблизи
        /* if ((BotPlayerDeltaPosX < bulletStraightFly) && (BotPlayerDeltaPosY < swordAtackDistanceY)) {
             KunaiThrow();
         }*/
       
        MoveToPlayerForSword();


        //Функции требующие динамического отслеживания переменных из Update(Поэтому и находятся в Update)
        void MoveToPlayerForSword()
        {
            if (BotPlayerDeltaPosX > 3)
            {
                if (PlS.sidePos == true)
                {
                    MoveRight();
                }
            }

            if (BotPlayerDeltaPosX > 3)
            {
                if (PlS.sidePos == false)
                {
                    MoveLeft();
                }
            }
         
           
        }
     
    }
    //Для того чтобы разовые события не зацикливались бесконечно
    IEnumerator ControlCodeCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        BC.controlCode = 0;
    }

   

    void HellPortal()
    {
        if (PlS.sidePos == true)
        {
            transform.position = new Vector2(playerPos.x + 0.8f, playerPos.y);
            SwordAttack();
        }
        if (PlS.sidePos == false)
        {
            transform.position = new Vector2(playerPos.x - 0.8f, playerPos.y);
            SwordAttack();
        }
        ScorpCoolDown();
    }

    IEnumerator ScorpCoolDown()
    {
        yield return new WaitForSeconds(1);
    }
}
