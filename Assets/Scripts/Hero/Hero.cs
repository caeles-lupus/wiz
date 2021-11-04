using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeAbility
{
    Wall,
    Fireball,
    Fly,
    Heal
}

public class Hero : Entity
{
    /// <summary>
    /// Возвращает экзмепляр "героя"
    /// </summary>
    public static Hero Instance;

    /// <summary>
    /// Направление
    /// </summary>
    public int Direction
    {
        get
        {
            return direction;
        }
    }

    [Header("Доп.объекты героя")]
    public GameObject attack_Staff;

    [Header("Характеристики")]
    [Range(2f, 99f)]
    public float Speed = 2f;
    public float Attack = 1f;
    [Range(0, 99)]
    public int Protection = 2;
    //Health in Entity
    //Mana in Entity

    [Header("Не игровые харак-ки")]
    public float JumpForce = 15f; //Set Gravity Scale in Rigidbody2D Component to 5
    public float MovementSpeed = 5f;

    [Header("Связи с другими компонентами")]
    public ParentBar healthBar;
    public ParentBar manaBar;
    public Magic magic;
    public WizAnim wizAnim;

    // Вспомогательные.
    private Rigidbody2D rb;
    private Animator anim;

    // Стандартная длительность анимации атаки.
    float lengthOfAttack;
    // Направление.
    private int direction = 1;
    // Состояния.
    bool isGrounded = true;
    private bool isAlive = true;
    private bool isAttacking = false;
    private bool isStopped;
    // Работа с сопрограммами.
    private Coroutine coroutineLanding;
    private Coroutine coroutineAttack;
    // Доступность магии.
    bool allowedMagicWall = false;
    bool allowedSelfTreatment = false;
    bool allowedFlight = false;
    bool allowedFireball = false;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Instance = this;

        myType = TypeOfEntity.Hero;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        attack_Staff.SetActive(false);

        healthBar.UpdateValue(Health, MaxHealth);
        manaBar.UpdateValue(Mana, MaxMana);

        //TODO: длина анимации атаки.
        lengthOfAttack = .467f;
        
        Pause();
    }

    // 
    private new void Update()
    {
        base.Update();

        if (!isStopped)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0)) Restart();
            if (isAlive)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2)) Hurt();
                if (Input.GetKeyDown(KeyCode.Alpha3)) Die();
                if (Input.GetButtonDown("Fire1") && !isAttacking) Attacks();
                if (Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0) Jump();
                
                // Magic:
                if (Input.GetKeyDown(KeyCode.Q) && allowedMagicWall && isGrounded) 
                    magic.ToCast(typeAbility.Wall);
                if (Input.GetKeyDown(KeyCode.F) && allowedFireball) 
                    magic.ToCast(typeAbility.Fireball);
                if (Input.GetKeyDown(KeyCode.Z) && allowedSelfTreatment) 
                    magic.ToCast(typeAbility.Heal);
                if (Input.GetKeyDown(KeyCode.LeftShift) && allowedFlight) 
                    magic.ToCast(typeAbility.Fly);

                //For test:
                if (Input.GetKeyDown(KeyCode.T)) IncreasedSpeed();
                
                Run();
            }
        }
    }

    // Касание другого коллайдера.
    private new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (TagsSets.tagsOfRealObjects.Contains(other.tag))
        {
            coroutineLanding = StartCoroutine(Landing());
        }

        // Для сбора монеток.
        else if(other.tag.Equals("Coin"))
        {
            StatisticCollector.CoinCount += 1;
            Destroy(other.gameObject);
        }
        // Для сбора кристаллов.
        else if (TagsSets.tagsCrystals.Contains(other.tag))
        {
            // Считаем собранный кристалл.
            StatisticCollector.CrystalCount += 1;
            // Определяем что он нам даст.
            collectingCrystals(other.tag);
            // Уничтожаем с игрового поля.
            Destroy(other.gameObject);
        }
        // Для сбора цветочков.
        else if (other.tag.Equals("Flowers"))
        {
            //FlowerCollect.FlowerCount += 1;
            Heal(1f);
            Destroy(other.gameObject);
        }


    }
    void collectingCrystals(string tagOfCrystall)
    {
        switch (tagOfCrystall)
        {
            // Земля. Защита. Стена.
            case "Crystal_Orange":
                IncreasedProtection(1);
                if (StatisticCollector.CrystalCount == 10 && !allowedMagicWall)
                {
                    allowedMagicWall = true;
                    Hint.ShowHint("Поздравляю! Собрав 10 эссенций земли, ты получаешь возможность воспользоваться магией полёта! Для этого зажми клавишу Shift и лети! Но помни, пока ты летишь - тратятся запасы магии! Если магия закончится, то полёт прервётся и ты упадёшь!");
                }
                break;
            // Воздух. Скорость. Полёт.
            case "Crystal_Blue":

                break;
            // Вода. Макс.здоровье. Исцеление.
            case "Crystal_DarkBlue":

                break;
            // Огонь. Атака. Фаербол.
            case "Crystal_Red":

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Останавливает анимацию героя, включает анимацию "простоя" и блокирует управление.
    /// </summary>
    public void Pause()
    {
        anim.SetBool("isJump", false);
        anim.SetBool("isRun", false);
        anim.SetTrigger("idle");
        isStopped = true;
    }
    public void Play()
    {
        isStopped = false;
    }

    // Бежит.
    void Run()
    {
        // Если происходит атака, то прерываем движение.
        if (isAttacking) return;

        Vector3 moveVelocity = Vector3.zero;
        anim.SetBool("isRun", false);

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = -1;
            moveVelocity = Vector3.left;

            transform.localScale = new Vector3(direction, 1, 1);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);

        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = 1;
            moveVelocity = Vector3.right;

            transform.localScale = new Vector3(direction, 1, 1);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);

        }
        transform.position += moveVelocity * (MovementSpeed + Speed) * Time.deltaTime;
    }

    /// <summary>
    /// Приземление.
    /// </summary>
    IEnumerator Landing()
    {
        anim.SetBool("isJump", false);
        yield return new WaitForSeconds(0.1f);
        isGrounded = true;
    }

    // Прыгает.
    void Jump()
    {
        if (isGrounded)
        {
            if (coroutineLanding != null) StopCoroutine(coroutineLanding);

            anim.SetBool("isJump", true);
            rb.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(0, JumpForce + (Speed * 0.9f));
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    // Атакует.
    void Attacks()
    {
        isAttacking = true;
        anim.SetTrigger("attack");
        if (coroutineAttack != null) StopCoroutine(coroutineAttack);
        coroutineAttack = StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        attack_Staff.SetActive(true);

        yield return new WaitForSeconds(lengthOfAttack / (Speed - 1f));
        attack_Staff.SetActive(false);
        isAttacking = false;
    }

    /// <summary>
    /// Получает урон.
    /// </summary>
    void Hurt()
    {
        anim.SetTrigger("hurt");
        if (direction == 1)
            rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
        else
            rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
    }

    /// <summary>
    /// Умирает.
    /// </summary>
    public override void Die(GameObject attacker = null)
    {
        anim.SetTrigger("die");
        isAlive = false;
    }

    // Воскрешение?
    void Restart()
    {
        anim.SetTrigger("idle");
        isAlive = true;
        Heal(MaxHealth); //Health = MaxHealth;
    }

    /// <summary>
    /// Увеличение брони.
    /// </summary>
    /// <param name="valInPercentage">Сколько процентов добавить к проценту брони</param>
    public void IncreasedProtection(int valInPercentage)
    {
        Protection += valInPercentage;
        if (Protection > 99) Protection = 99;
        else if (Protection < 0) Protection = 0;
    }

    public void IncreasedSpeed(float val = 0.2f)
    {
        Speed += val;
        anim.SetFloat("speedAttack", Speed - 1f);
    }

    /// <summary>
    /// Увеличивает текущее здоровье на HealValue, но не более MaxHealth.
    /// </summary>
    /// <param name="HealValue"></param>
    public override void Heal(float HealValue)
    {
        base.Heal(HealValue);
        //TODO: эффект какой-нибудь?
        healthBar.UpdateValue(Health, MaxHealth);
    }

    // Получает урон.
    public override void GetDamage(float DamageValue, GameObject attacker = null)
    {
        //base.GetDamage();
        Health -= (DamageValue - DamageValue * Protection / 100f);
        if (Health > 0)
        {
            Hurt();
        }
        else
        {
            Health = 0;
            Die(attacker);
        }
        healthBar.UpdateValue(Health, MaxHealth);
    }

    public override void SpendMana(float spentMana)
    {
        base.SpendMana(spentMana);
        manaBar.UpdateValue(Mana, MaxMana);
    }
}