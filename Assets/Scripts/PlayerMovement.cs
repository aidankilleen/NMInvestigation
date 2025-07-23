using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;

    private Rigidbody rb;
    private Animator animator;
    public NavMeshSurface navMeshSurface;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        float rotateInput = Input.GetAxis("Horizontal"); // Left/Right arrows
        float rotation = rotateInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotation, 0f);

        float moveInput = Input.GetAxis("Vertical"); // Up/Down arrows
        Vector3 moveDirection = transform.forward * moveInput * moveSpeed;
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);

        animator?.SetFloat("Speed", rb.velocity.magnitude);


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InventoryItem"))
        {
            // Handle collision with obstacle
            Debug.Log("Collided with an InvengtoryItem");
            navMeshSurface?.BuildNavMesh(); // Rebuild the NavMesh after collision
        }
    }
}
