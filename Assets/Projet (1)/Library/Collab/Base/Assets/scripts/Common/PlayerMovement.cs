using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variable pour le point de respawn
    public Vector4 RespawnPoint;
    #region
    private Animator animator; // gestion des animations
    public static bool isJumping = false; // lorsqu'il saute
    public static bool isRunning = false; // lorsqu'il bouge
    private bool grounded = false;
    public static bool isDied = false;
    public static int lifeCount = 3;
    public float velocityX; // vitesse horizontale
    public float velocityY; // vitesse verticale
    private Rigidbody2D player;
    private bool isBouncing;

    private bool Dash = false;
    private bool DashAlreadyUsed = false;
    private bool DashUsing = false;

    private bool DoubleJump = false;
    public bool DoubleJumpAlreadyUsed = false;

    private Direction direction = Direction.RIGHT;

    public GameObject WinScreen;
    #endregion

    public enum Direction
    {
        LEFT, RIGHT
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        RespawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //print(isDied);
        AnimationManager();
        Movements();
        OnDashActivate();
        OnDoubleJumpActivate();
        checkMovements();
        Dead(isDied, lifeCount);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
        if (collision.collider.CompareTag("Platform"))
        {
            transform.parent = collision.transform; // on attache la plateforme au parent du joueur
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        if (collision.collider.CompareTag("Platform"))
        {
            transform.parent = null; // on détache la plateforme du parent du joueur
        }
    }

    private void checkMovements()
    {
        if (player.velocity.x != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

    }
    void Dead(bool _isDead, int _lifeCount)
    {
        if(_isDead)
        {
            if(_lifeCount>0)
            {
                //Respawn
                Debug.Log("Perte d'une vie");
            }
            else
            {
                //Ecran Game Over
                Debug.Log("Plus de vie");
            }
        }
    }

    private void Movements()
    {
        if (EventOption.control == 1)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0); // bloque la rotation du joueur
            if (Input.GetKey(KeyCode.D) && !DashUsing) // déplacement vers la droite
            {
                player.velocity = new Vector2(velocityX, player.velocity.y);
                player.transform.localScale = new Vector3(1, 1, 1);
                direction = Direction.RIGHT;
            }

            if (Input.GetKey(KeyCode.Q) && !DashUsing) // déplacement vers la droite
            {
                player.velocity = new Vector2(-velocityX, player.velocity.y);
                player.transform.localScale = new Vector3(-1, 1, 1);
                direction = Direction.LEFT;
            }

            if (Input.GetKeyDown(KeyCode.Space) && grounded && !DashUsing) // pour produire un saut
            {
                player.velocity = new Vector2(player.velocity.x, velocityY);
                isJumping = true;
                animator.SetBool("Jumping", true);
            }
        }
        if (EventOption.control == 2)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0); // bloque la rotation du joueur
            if (Input.GetKey(KeyCode.RightArrow) && !DashUsing) // déplacement vers la droite
            {
                player.velocity = new Vector2(velocityX, player.velocity.y);
                player.transform.localScale = new Vector3(1, 1, 1);
                direction = Direction.RIGHT;
            }

            if (Input.GetKey(KeyCode.LeftArrow) && !DashUsing) // déplacement vers la droite
            {
                player.velocity = new Vector2(-velocityX, player.velocity.y);
                player.transform.localScale = new Vector3(-1, 1, 1);
                direction = Direction.LEFT;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && grounded && !DashUsing) // pour produire un saut
            {
                player.velocity = new Vector2(player.velocity.x, velocityY);
                isJumping = true;
                animator.SetBool("Jumping", true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Contains("Dash") || collision.name.Contains("DoubleJump"))
        {
            if (Input.GetKey("x"))
            {
                Animator anim = collision.gameObject.GetComponent<Animator>();
                if (!anim.GetBool("Opening"))
                {
                    anim.SetBool("Opening", true);
                    WinScreen.SetActive(true);
                    StartCoroutine(endAnimChest(anim));
                }
            }
        }
    }
    // récupération d'une pièce
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
            CoinScore.score++;
        }
        /*if(other.gameObject.CompareTag("water"))
        {
            isDied = true;
            lifeCount--;
            Debug.Log("EAU TOUCHER");
        }*/
        if(other.gameObject.CompareTag("Winwall"))
        {
            WinScreen.SetActive(true);
        }
    }


    private void AnimationManager()
    {
        if (isRunning)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        if (grounded)
        {
            if (Dash)
            {
                DashAlreadyUsed = false;
            }
            if (DoubleJump)
            {
                DoubleJumpAlreadyUsed = false;
            }
            isJumping = false;
        }

        if (!isJumping)
        {
            animator.SetBool("Jumping", false);
        }
    }

    // La methode qui sauvegarde la derniere position du joueur grace au coffre qui est un point de sauvegarde.
    // ChestSave (le tag du coffre) permet de donner la position avec la collision du coffre. 
    private IEnumerator endAnimChest(Animator anim)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 2.0f)
            {
                if(anim.gameObject.name.Contains("Dash"))
                {
                    Dash = true;
                }else if (anim.gameObject.name.Contains("DoubleJump"))
                {
                    DoubleJump = true;
                }
                Destroy(anim.gameObject);
                break;
            }
        }
    }

    private void OnDashActivate()
    {
        //print("Dash: "+Dash+" "+Input.GetKey(KeyCode.W)+" "+(!DashAlreadyUsed)+" "+ isJumping);
        if (Dash && !DashAlreadyUsed && !grounded && Input.GetKey(KeyCode.W))
        {
            DashAlreadyUsed = true;
            StartCoroutine(push());
        }
    }

    private void OnDoubleJumpActivate()
    {
        //print("DoubleJump: " + DoubleJump + " " + Input.GetKey(KeyCode.Space) + " " + (!DoubleJumpAlreadyUsed) + " " + isJumping);
        if (DoubleJump && !DoubleJumpAlreadyUsed && !grounded && Input.GetKeyDown(KeyCode.Space))
        {
            DoubleJumpAlreadyUsed = true;
            player.velocity += new Vector2(player.velocity.x, velocityY);
        }
    }

    private IEnumerator push()
    {
        DashUsing = true;
        if (direction == Direction.RIGHT)
            player.velocity += new Vector2(velocityX * 2, 0);
        else if (direction == Direction.LEFT)
            player.velocity += new Vector2(-velocityX * 2, 0);
        yield return new WaitForSeconds(0.5f);
        DashUsing = false;
    }
}
