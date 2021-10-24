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

    public float movePower = 10f;
    public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5
    public int lives = 10;

    private Rigidbody2D rb;
    private Animator anim;

    private int direction = 1;
    bool isJumping = false;
    private bool alive = true;
    private bool isAttacking = false;

    private bool isStopped;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //attack_Staff = GameObject.Find("/Hero/Skeletal/15 Staff/Attack_Staff");
        attack_Staff.SetActive(false);
    }

    // 
    private void Update()
    {
        if (!isStopped)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0)) Restart();
            if (alive)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2)) Hurt();
                if (Input.GetKeyDown(KeyCode.Alpha3)) Die();
                if (Input.GetButtonDown("Fire1") && !isAttacking) Attack();
                Jump();
                Run();
            }
        }
    }

    // Касается другого коллайдера.
    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (TagsSets.tagsOfRealObjects.Contains(other.tag)) anim.SetBool("isJump", false);

        // Для сбора монеток.
        if (other.tag.Equals("Coin"))
        {
            CoinCollect.CoinCount += 1;
            Destroy(other.gameObject);
        }
        // Для сбора кристаллов.
        else if (other.tag.Equals("Crystal"))
        {
            //CrystalCollect.CrystalCount += 1;
            Destroy(other.gameObject);
        }

    }

    /// <summary>
    /// Останавливает анимацию героя, включает анимацию "простоя" и блокирует управление.
    /// </summary>
    public void Stop()
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
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    // Прыгает.
    void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0) && !anim.GetBool("isJump"))
        {
            isJumping = true;
            anim.SetBool("isJump", true);
        }
        if (!isJumping)
        {
            return;
        }

        rb.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }

    // Атакует.
    void Attack()
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
        lives = 10;
    }

    // Получает урон.
    public override void GetDamage()
    {
        //base.GetDamage();
        lives --;
        if (lives > 0)
        {
            Hurt();

        }
        else
        {
            Die();

        }
        Debug.Log(lives);
    }
}