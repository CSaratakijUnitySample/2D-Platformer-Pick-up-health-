﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveForce;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    float gravity;

    [SerializeField]
    float terminalVelocity;

    [SerializeField]
    Transform ground;

    [SerializeField]
    LayerMask groundLayer;


    bool isCanJump;
    bool isPressedJump;

    Vector2 inputVector;
    Vector2 velocity;
    Vector2 rayDirection;

    RaycastHit2D hit;
    Rigidbody2D rigid;

    Status health;


    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        InputHandler();
    }

    void FixedUpdate()
    {
        CheckGround();
        MovementHandler();
    }

    void Initialize()
    {
        isPressedJump = false;
        isCanJump = false;
        rayDirection = new Vector2(1.0f, -1.0f);
        rigid = GetComponent<Rigidbody2D>();
        health = GetComponent<Status>();
    }

    void InputHandler()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
        isPressedJump = Input.GetButtonDown("Jump");

        if (Input.GetButtonDown("Cancel")) {
            ReloadCurrentScene();
        }
    }

    void CheckGround()
    {
        hit = Physics2D.Raycast(ground.position, rayDirection, 0.2f, groundLayer);
        isCanJump = (hit.collider != null);
    }

    void MovementHandler()
    {
        velocity.x = (moveForce * inputVector.x);

        if (isCanJump && isPressedJump) {
            velocity.y = jumpForce;
        }
        else {
            velocity.y -= gravity;
            velocity.y = Mathf.Clamp(velocity.y, -terminalVelocity, jumpForce);
        }

        rigid.velocity = (velocity * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.activeSelf && collision.CompareTag("Health")) {
            health.Restore(25);
            collision.gameObject.SetActive(false);
        }
    }

    void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}

