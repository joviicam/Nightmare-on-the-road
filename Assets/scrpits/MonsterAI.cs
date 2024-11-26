using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public NavMeshAgent agent; // Agente de navegaci�n
    public Transform[] patrolPoints; // Puntos de patrulla
    public float detectionRadius = 10f; // Radio de detecci�n
    public float damageRadius = 6f; // Radio para hacer da�o
    public float chaseSpeed = 5f; // Velocidad de persecuci�n
    public float patrolSpeed = 2f; // Velocidad de patrulla
    public int damage = 50; // Da�o por golpe
    public float damageCooldown = 5f; // Tiempo entre golpes
    public LayerMask detectionLayer; // Capa del jugador
    public AudioSource chaseSound; // Sonido de persecuci�n

    private int currentPatrolIndex = 0; // �ndice de patrulla
    private Transform player; // Referencia al jugador
    private bool isChasing = false; // Estado de persecuci�n
    private float nextDamageTime = 5f; // Control del tiempo entre golpes

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Configura el primer punto de patrulla
        if (patrolPoints.Length > 0)
        {
            agent.destination = patrolPoints[currentPatrolIndex].position;
            agent.speed = patrolSpeed;
        }

        // Asegura que el sonido de persecuci�n est� desactivado al inicio
        if (chaseSound != null)
        {
            chaseSound.loop = true; // Hacer que el sonido se repita
            chaseSound.Stop();
        }
    }

    void Update()
    {
        if (isChasing)
        {
            if (player != null)
            {
                agent.destination = player.position;

                // Comprobar si el jugador est� dentro del rango de da�o
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                if (distanceToPlayer <= damageRadius && Time.time >= nextDamageTime)
                {
                    player.GetComponent<PlayerHealth>().TakeDamage(damage); // Llamada al jugador
                    nextDamageTime = Time.time + damageCooldown; // Espera para el pr�ximo golpe
                }
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (agent.remainingDistance < 0.5f && !agent.pathPending)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.destination = patrolPoints[currentPatrolIndex].position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            isChasing = true;
            agent.speed = chaseSpeed;

            // Reproducir sonido de persecuci�n
            if (chaseSound != null && !chaseSound.isPlaying)
            {
                chaseSound.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false;
            agent.speed = patrolSpeed;

            // Detener sonido de persecuci�n
            if (chaseSound != null && chaseSound.isPlaying)
            {
                chaseSound.Stop();
            }

            // Reiniciar patrulla
            if (patrolPoints.Length > 0)
            {
                agent.destination = patrolPoints[currentPatrolIndex].position;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja el radio de detecci�n y da�o en la escena
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
