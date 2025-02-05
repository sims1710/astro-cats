using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Cat : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 10f;

    public int score = 0;
    public int lives = 3;

    public GameObject heartSprite1;
    public GameObject heartSprite2;
    public GameObject heartSprite3;

    public TextMeshProUGUI ScoreText;

    public string gameOverSceneName = "GameOver";

    public GameObject furballPrefab;
    public float shootingForce = 10f;

    private bool isGrounded;
    private float moveX = 0f, moveY = 0f;
    private Animator animator;
    //private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        if (Mathf.Abs(moveX) > 0f || Mathf.Abs(moveY) > 0f)
        {
            animator.SetBool("isRunnin", true);

            if (moveX > 0f)
                transform.localScale = new Vector3(8, 8, 8);
            else if (moveX < 0f)
                transform.localScale = new Vector3(-8, 8, 8);
        }
        else
        {
            animator.SetBool("isRunnin", false);
        }

        transform.Translate(moveX, moveY, 0);

        ScoreText.text = "Score: " + score;

        if (lives < 1)
        {
            PlayerPrefs.SetInt("Score", score);
            SceneManager.LoadScene(gameOverSceneName);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            score++;
        }
        else if (other.gameObject.CompareTag("FishBone"))
        {
            lives--;
            UpdateHearts();
        }
        else if (other.gameObject.CompareTag("Meteorite"))
        {
            lives--;
            UpdateHearts();
        }
        else if (other.gameObject.CompareTag("Heart"))
        {
            if (lives == 3)
            {
                score += 10;
            }
            else
            {
                lives++;
            }

            UpdateHearts();
        }
    }

    void UpdateHearts()
    {
        heartSprite1.SetActive(lives >= 1);
        heartSprite2.SetActive(lives >= 2);
        heartSprite3.SetActive(lives >= 3);
    }

    void Shoot()
    {
        animator.SetTrigger("shoot");

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        GameObject furballBullet = Instantiate(furballPrefab, transform.position, Quaternion.identity);

        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position);
        direction.z = 0f;

        Rigidbody2D rb = furballBullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * shootingForce;

        animator.ResetTrigger("shoot");
    }

    
}