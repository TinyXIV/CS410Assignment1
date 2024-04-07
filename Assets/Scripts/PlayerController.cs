using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public float jumpForce = 0;
    private bool canDoubleJump;
    public TextMeshProUGUI countText;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        rb = GetComponent <Rigidbody>();
        SetCountText();
        canDoubleJump = true;
    }

    



    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && canDoubleJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canDoubleJump = false; //disable double jump after the player jumps
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canDoubleJump = true; //enable double jump when the player touches the ground
        }
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText(){
        countText.text = "Count: " + count.ToString();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }


    // Update is called once per frame

}
