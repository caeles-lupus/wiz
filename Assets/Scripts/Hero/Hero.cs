using UnityEngine;

public class Hero : Entity
{
    /// <summary>
    /// Возвращает экзмепляр "героя"
    /// </summary>
    public static Hero Instance { get; set; }

    public float movePower = 10f;
    public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5
    public int lives = 10;

    private Rigidbody2D rb;
    private Animator anim;
    Vector3 movement;
    private int direction = 1;
    bool isJumping = false;
    private bool alive = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Instance = this;
    }

    // 
    private void Update()
    {
        Restart();
        if (alive)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2)) Hurt();
            if (Input.GetKeyDown(KeyCode.Alpha3)) Die();
            if (Input.GetKeyDown(KeyCode.Alpha1)) Attack();
            Jump();
            Run();

        }
    }

    // Касается другого коллайдера.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: при касании чего угодно, отключается анимация прыжка.
        anim.SetBool("isJump", false);
    }

    // Бежит.
    void Run()
    {
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
        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
        && !anim.GetBool("isJump"))
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
        anim.SetTrigger("attack");
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
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.SetTrigger("idle");
            alive = true;
        }
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