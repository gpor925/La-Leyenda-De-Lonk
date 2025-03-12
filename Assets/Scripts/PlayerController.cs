using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    CharacterController _control;
    Animator _anim;

    [SerializeField] GameObject _attackHitbox;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] Level2Door _level2Door;

    [SerializeField] float playerSpeed;
    [SerializeField] float knockbackForce = 5f; // Adjust the knockback force as needed
    [SerializeField] float invincibilityDuration = 0.5f; // Duration of invincibility


    public int playerHealth;
    public int maxHealth = 5;

    [SerializeField] int firstLevelGemsTotal;
    private bool isInvincible = false;

    public float gravity = 9.81f;

    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        Cursor.visible = false; // Hide the mouse cursor on start

        {
            Cursor.visible = false; // Hide the mouse cursor on start
                                    // Get components

            _control = GetComponent<CharacterController>();
            _anim = GetComponent<Animator>();

            _attackHitbox.SetActive(false);
            _anim.SetLayerWeight(1, 1);

            playerHealth = maxHealth;
            firstLevelGemsTotal = 0;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Check if the escape key is pressed
        {
            Application.Quit(); // Exit the application
        }

        {

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Small downward force to keep grounded
            }

            //Movement input
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            //Movement
            if (_control.enabled)
            {
                Vector3 movement = new Vector3(moveX, 0, moveZ).normalized;
                _control.Move(movement * playerSpeed * Time.deltaTime);
                if (movement != Vector3.zero)
                {
                    transform.forward = movement;
                }
                _anim.SetFloat("speed", movement.magnitude);
            }

            velocity.y -= gravity * Time.deltaTime;
            if (velocity.y < -5f)
            {
                velocity.y = -2f;
            }
            _control.Move(velocity * Time.deltaTime);

            //Attack
            if (Input.GetMouseButtonDown(0))
            {
                _anim.SetTrigger("playerAttack");
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            TakeDamage(other.transform.position);
        }
        if (other.tag == "Gem1")
        {
            Destroy(other.gameObject);
            firstLevelGemsTotal++;
            if (firstLevelGemsTotal >= 2)
            {
                _level2Door.Open();
            }
        }
        if (other.tag == "Lava")
        {
            Debug.Log("polla");
            SceneManager.LoadScene("Nivel 2");

        }
        if (other.tag == "Win")
        {
            SceneManager.LoadScene("Win");
        }
        if (other.tag == "Portal")
        {
            SceneManager.LoadScene("Nivel 2");
        }
    }

    public void TakeDamage(Vector3 enemyPosition)
    {
        Debug.Log("Damage taken");
        if (isInvincible) return; // Ignore damage if invincible

        playerHealth--;
        if (playerHealth <= 0)
        {
            Die();
        }

        isInvincible = true; // Set invincibility

        // Calculate knockback direction
        Vector3 knockbackDirection = (transform.position - enemyPosition).normalized;
        _control.Move(knockbackDirection * knockbackForce * Time.deltaTime);

        // Start coroutine for invincibility
        StartCoroutine(InvincibilityCoroutine());
    }

    private void Die()
    {
        _control.enabled = false; // Disable player control
        _anim.SetLayerWeight(1, 0); // Set the weight of the attack animation layer to 0


        _anim.SetTrigger("Die"); // Play death animation
        //load menu
        StartCoroutine(LoadMenuAfterDelay());

    }

    private IEnumerator LoadMenuAfterDelay()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        SceneManager.LoadScene("MENU"); // Load the MENU scene
    }

    private IEnumerator InvincibilityCoroutine()


    {
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false; // Reset invincibility
    }

    public void HitParticle()
    {
        _particleSystem.Play();

        Debug.Log("hit particle active");
    }
}
