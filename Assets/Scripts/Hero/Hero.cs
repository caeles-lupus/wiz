using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Entity
{
    /// <summary>
    /// Возвращает экзмепляр "героя"
    /// </summary>
    public static Hero Instance;

    public GameObject attack_Staff;

    [Header("Характеристики")]
    public float MovementSpeed = 10f;
    public float JumpForce = 15f; //Set Gravity Scale in Rigidbody2D Component to 5
    public float Attack = 1f;
    [Header("Связи с другими компонентами")]
    public HealthBar healthBar;

    private Rigidbody2D rb;
    private Animator anim;

    private int direction = 1;

    bool isJumping = false;

    bool isGrounded = true;
    public Transform GroundCheck;
    public float CheckRaduis;
    public LayerMask whatIsGround;
    private CapsuleCollider2D bodyCollider;

    private bool alive = true;
    private bool isAttacking = false;

    private bool isStopped;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Instance = this;
        bodyCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //attack_Staff = GameObject.Find("/Hero/Skeletal/15 Staff/Attack_Staff");
        attack_Staff.SetActive(false);
        healthBar.UpdateValue(Health, MaxHealth);

        //checkGrounded();
    }

    /// <summary>
    /// 
    /// </summary>
    void checkGrounded()
    {
        //isGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRaduis, whatIsGround);
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        
        if (Physics2D.OverlapCollider(bodyCollider, filter.NoFilter(), results) > 0)
        {
            isGrounded = results.Find(elem => TagsSets.tagsOfRealObjects.Contains(elem.tag));
        }
    }

    // 
    private new void Update()
    {
        base.Update();
        //checkGrounded();
        if (!isStopped)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0)) Restart();
            if (alive)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2)) Hurt();
                if (Input.GetKeyDown(KeyCode.Alpha3)) Die();
                if (Input.GetButtonDown("Fire1") && !isAttacking) Attacks();
                if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)) Jump();
                Run();
            }
        }
    }

    // Касается другого коллайдера.
    private new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (TagsSets.tagsOfRealObjects.Contains(other.tag))
        {
            isGrounded = true;
            anim.SetBool("isJump", false);
        }

        // Для сбора монеток.
        if (other.tag.Equals("Coin"))
        {
            CoinCollect.CoinCount += 1;
            Destroy(other.gameObject);
        }
        // Для сбора кристаллов.
        else if (other.tag.Equals("Crystal"))
        {
            CrystalCollect.CrystalCount += 1;
            Destroy(other.gameObject);
        }
        // Для сбора цветочков.
        else if (other.tag.Equals("Flowers"))
        {
            //CrystalCollect.CrystalCount += 1;
            Heal(1f);
            Destroy(other.gameObject);
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
        transform.position += moveVelocity * MovementSpeed * Time.deltaTime;
    }

    // Прыгает.
    void Jump()
    {
        if (isGrounded)
        {
            if (!anim.GetBool("isJump"))
            {
                isJumping = true;
                anim.SetBool("isJump", true);
            }
            if (!isJumping)
            {
                return;
            }

            rb.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, JumpForce);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

            isJumping = false;
        }
    }

    // Атакует.
    void Attacks()
    {
        isAttacking = true;
        anim.SetTrigger("attack");
        StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        attack_Staff.SetActive(true);
        yield return new WaitForSeconds(.5f);
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
    public override void Die()
    {
        anim.SetTrigger("die");
        alive = false;
    }

    // Воскрешение?
    void Restart()
    {
        anim.SetTrigger("idle");
        alive = true;
        Heal(MaxHealth); //Health = MaxHealth;
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
    public override void GetDamage(float DamageValue)
    {
        //base.GetDamage();
        Health -= DamageValue;
        if (Health > 0)
        {
            Hurt();
        }
        else
        {
            Health = 0;
            Die();
        }
        healthBar.UpdateValue(Health, MaxHealth);
    }
}