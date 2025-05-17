using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    public float speed = 0;
    public float jumpForce = 5f;
    private bool isGrounded = true;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    

    private Renderer playerRenderer;
    private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.white };
    private int colorIndex = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        playerRenderer = GetComponent<Renderer>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void Update()
    {
        var keyboard = Keyboard.current;

        // �������
        if (keyboard.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // ���� �������
        if (keyboard.cKey.wasPressedThisFrame)
        {
            colorIndex = (colorIndex + 1) % colors.Length;
            playerRenderer.material.color = colors[colorIndex];
        }

        // ������� ���
        if (keyboard.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Blue"))
        {
            other.gameObject.SetActive(false);
            speed += 2f;
            jumpForce += 2f;
        }

     
        else if (other.gameObject.CompareTag("Red"))
        {
            other.gameObject.SetActive(false);
            speed = Mathf.Max(1f, speed - 2f);      // ��������� �������� = 1
            jumpForce = Mathf.Max(2f, jumpForce - 2f); // ��������� ���� ������� = 2
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        

        if (count >= 5)
        {
            winTextObject.SetActive(true);
            
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            speed = 0;
            jumpForce = 0;
            rb.linearVelocity = Vector3.zero;

            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            
        }


        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
