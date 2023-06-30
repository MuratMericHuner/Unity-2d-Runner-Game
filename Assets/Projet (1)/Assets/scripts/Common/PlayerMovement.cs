using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Variable pour le point de respawn
    public Vector4 RespawnPoint;
    #region
    private Animator animator; // gestion des animations
    public static bool isJumping = false; // lorsqu'il saute
    public static bool isRunning = false; // lorsqu'il bouge
    private bool grounded = false; // touche le sol
    public static bool isDied = false; // est mort
    public static int lifeCount = 3;
    public float velocityX; // vitesse horizontale
    public float velocityY; // vitesse verticale
    private Rigidbody2D player; // Le corps
    private bool isBouncing; // 

    public bool Dash = false; // capacité dash
    private bool DashAlreadyUsed = false; // Dash déjà utilisé
    private bool DashUsing = false; // en cours d'utilisation

    public bool DoubleJump = false; // Double saut
    public bool DoubleJumpAlreadyUsed = false; // double saut déjà utilisé

    private Direction direction = Direction.RIGHT; // direction du joueur

    public GameObject WinScreen;
    #endregion

    // Direction droite, gauche
    public enum Direction
    {
        LEFT, RIGHT
    }

    // Chargement des composants
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        RespawnPoint = transform.position;
    }

    // Met à jour toutes les méthodes 
    void Update()
    {
        AnimationManager();
        Movements();
        OnDashActivate();
        OnDoubleJumpActivate();
        checkMovements();
        Dead(isDied, lifeCount);
    }

    /**
     * Commentaire: Lors d'une collision on met la variable "grounded" à vrai et on attache la plateforme si le joueur rentre en collision avec celle-ci
     * Arguments: 
     * -Collision2D collision: C'est la boite de collision du joueur
     **/
    void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
        if (collision.collider.CompareTag("Platform"))
        {
            transform.parent = collision.transform; // on attache la plateforme au parent du joueur
        }
    }

    /**
     * Commentaire: Si le joueur n'est plus en collision on met la variable "grounded" à faux et on détache la plateforme du joueur
     * Arguments: 
     * -Collision2D collision: C'est la boite de collision du joueur
     **/
    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        if (collision.collider.CompareTag("Platform"))
        {
            transform.parent = null; // on détache la plateforme du parent du joueur
        }
    }

    /**
     * Commentaire: On met à jour la variable "isRunnning" selon les déplacements du joueur
     **/
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


    void Dead(bool _isDead, int _lifeCount) //fonction gérant la mort du joueur
    {
        
        if (_isDead)
        {
            FindObjectOfType<EventMusic>().Play("Death");   //lancement de la musique de mort
            isDied = false;                                 // réinitialisation de la variable isDied sur false
                if (_lifeCount > 0)                         // si encore des vies on fait respawn le joueur
                {
                transform.position = RespawnPoint;
                Debug.Log("Perte d'une vie");
                }
                else                                        //sinon on lui affiche l'écran de GameOver
                {
                    SceneManager.LoadScene("GameOver");
                    Debug.Log("Plus de vie");                   
                }
        }
        
        
    }

    private void Movements()
    {
        if (EventOption.control == 1)   //si selection des control1 dans option
        {
            // On bloque la rotation du joueur en x y et z
            transform.localEulerAngles = new Vector3(0, 0, 0);
            // déplacement vers la droite en appuyant sur la touche en question
            if (Input.GetKey(KeyCode.D) && !DashUsing)
            {
                player.velocity = new Vector2(velocityX, player.velocity.y);
                player.transform.localScale = new Vector3(1, 1, 1);
                direction = Direction.RIGHT;
            }

            // déplacement vers la droite en appuyant sur la touche en question
            if (Input.GetKey(KeyCode.Q) && !DashUsing)
            {
                player.velocity = new Vector2(-velocityX, player.velocity.y);
                player.transform.localScale = new Vector3(-1, 1, 1);
                direction = Direction.LEFT;
            }
            // Si le joueur appuie sur la touche en question on effectue un déplacement verticale pour produire un saut et on active l'animation de saut
            if (Input.GetKeyDown(KeyCode.Space) && grounded && !DashUsing) 
            {
                player.velocity = new Vector2(player.velocity.x, velocityY);
                isJumping = true;
                animator.SetBool("Jumping", true);
            }
        }
        if (EventOption.control == 2) // si selection des control2 dans les options
        {
            // On bloque la rotation du joueur en x y et z
            transform.localEulerAngles = new Vector3(0, 0, 0);
            // déplacement vers la droite en appuyant sur la touche en question
            if (Input.GetKey(KeyCode.RightArrow) && !DashUsing)
            {
                player.velocity = new Vector2(velocityX, player.velocity.y);
                player.transform.localScale = new Vector3(1, 1, 1);
                direction = Direction.RIGHT;
            }

            // déplacement vers la droite en appuyant sur la touche en question
            if (Input.GetKey(KeyCode.LeftArrow) && !DashUsing)
            {
                player.velocity = new Vector2(-velocityX, player.velocity.y);
                player.transform.localScale = new Vector3(-1, 1, 1);
                direction = Direction.LEFT;
            }

            // Si le joueur appuie sur la touche en question on effectue un déplacement verticale pour produire un saut et on active l'animation de saut
            if (Input.GetKeyDown(KeyCode.UpArrow) && grounded && !DashUsing)
            {
                
                player.velocity = new Vector2(player.velocity.x, velocityY); 
                isJumping = true;
                animator.SetBool("Jumping", true);
            }
        }
    }

    /**
     * Commentaire: Responsable de l'ouverture des coffres pour activer les capacités
     * -Collision2D collision: C'est la boite du trigger
     **/
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Si l'objet se nomme double jump ou dash
        if (collision.name.Contains("Dash") || collision.name.Contains("DoubleJump")) 
        {
            // Si le joueur appuie sur X
            if (Input.GetKey("x"))
            {
                // On vérifie l'état de l'animation
                Animator anim = collision.gameObject.GetComponent<Animator>();
                // Si l'animation n'est pas actif
                if (!anim.GetBool("Opening"))
                {
                    // On l'active
                    anim.SetBool("Opening", true);
                    WinScreen.SetActive(true);
                    // On débute le cooldown
                    StartCoroutine(endAnimChest(anim));
                }
            }
        }
    }
    // récupération d'une pièce
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("coin"))                //sur le contact entre le player et une pièce
        {           
            other.gameObject.SetActive(false);                  // on désactive l'affichage de la pièce
            CoinScore.score++;                                  // on incrémente le score
            FindObjectOfType<EventMusic>().Play("Coin");        // on lit le son approprié
        }
        if (other.gameObject.CompareTag("Winwall"))             //sur le contact entre le player et le mur de fin
        {
            WinScreen.SetActive(true);                          // on active le screen de victoire
            int level = SceneManager.GetActiveScene().buildIndex;   
            EventNiveaux.setUnlockedLevel(level);
        }

        if (other.tag == "FallDetector")
        {
            transform.position = RespawnPoint;
        }
        if (other.tag == "ChestSave")
        {
            RespawnPoint = other.transform.position;
        }
    }

    /**
     * Commentaire: Responsable de la gestion des animations
     **/
    private void AnimationManager()
    {
        // Activation de l'animation selon les déplacements horizontales du joueur
        if (isRunning)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        // Si le joueur touche le sol
        if (grounded)
        {
            // Si le joueur a la capacité dash d'actif
            if (Dash)
            {
                DashAlreadyUsed = false; // On met permet l'utilisation de la capacité à chaque frame
            }
            // même chose pour le double saut
            if (DoubleJump)
            {
                DoubleJumpAlreadyUsed = false;
            }
            isJumping = false; // le joueur n'effectue au saut
        }

        // on arrête l'animation du saut
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

    /**
     * Commentaire: On active le dash
     **/
    private void OnDashActivate()
    {
        
        if(EventOption.control==1)
        {
            if (Dash && !DashAlreadyUsed && !grounded && Input.GetKey(KeyCode.E))
            {
                DashAlreadyUsed = true;
                StartCoroutine(push());
            }
        }
        if(EventOption.control==2)
        {
            if (Dash && !DashAlreadyUsed && !grounded && Input.GetKey(KeyCode.RightControl))
            {
                DashAlreadyUsed = true;
                StartCoroutine(push());
            }
        }
        
    }

    /**
     * Commentaire: On active le double saut
     **/
    private void OnDoubleJumpActivate()
    {
        if (DoubleJump && !DoubleJumpAlreadyUsed && !grounded && Input.GetKeyDown(KeyCode.Space))
        {
            DoubleJumpAlreadyUsed = true;
            player.velocity += new Vector2(player.velocity.x, velocityY);
        }
    }

    /**
     * Commentaire: Cooldown avant la prochaine utilisation
     **/
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
