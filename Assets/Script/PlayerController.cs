using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    private Vector3 deplacement;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // empêche la capsule de tomber sur le côté
    }

    void Update()
    {
        // Lire les touches
        deplacement = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            deplacement += Vector3.forward;
        if (Input.GetKey(KeyCode.DownArrow))
            deplacement += Vector3.back;
        if (Input.GetKey(KeyCode.LeftArrow))
            deplacement += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow))
            deplacement += Vector3.right;

        deplacement = deplacement.normalized;
    }

    void FixedUpdate() // 🔹 Utilisé pour tout ce qui touche la physique
    {
        if (deplacement != Vector3.zero)
        {
            Vector3 newPosition = rb.position + deplacement * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition); // ✅ le moteur physique gère les collisions
        }
    }
}
