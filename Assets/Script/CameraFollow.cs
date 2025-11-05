using UnityEngine;

public class CameraTPS : MonoBehaviour
{
    [SerializeField] private Transform player;       // Capsule à suivre
    [SerializeField] private Vector3 offset = new Vector3(0f, 6f, -12f); // Position relative
    [SerializeField] private float smoothSpeed = 0.125f;                // Fluidité
    [SerializeField] private float collisionRadius = 0.5f;             // Distance minimale de la caméra au joueur

    void LateUpdate()
    {
        if (player == null) return;

        // Position désirée derrière le joueur
        Vector3 desiredPosition = player.position + offset;

        // Vérifier si un obstacle bloque la caméra
        RaycastHit hit;
        Vector3 direction = (desiredPosition - player.position).normalized;
        float distance = Vector3.Distance(player.position, desiredPosition);

        if (Physics.SphereCast(player.position, collisionRadius, direction, out hit, distance))
        {
            // Reculer la caméra juste devant l'obstacle
            desiredPosition = hit.point - direction * collisionRadius;
        }

        // Mouvement fluide
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Regarder légèrement au-dessus de la capsule
        Vector3 lookTarget = player.position + Vector3.up * 1.5f;
        transform.LookAt(lookTarget);
    }
}
