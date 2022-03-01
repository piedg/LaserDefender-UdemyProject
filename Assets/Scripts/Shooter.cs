using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.5f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minimumSpawnTime = 0.2f;

    [HideInInspector]
    public bool isShooting;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isShooting = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isShooting && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isShooting && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            audioPlayer.PlayShootingClip();

            Destroy(instance, projectileLifetime);
            if (useAI)
            {
                yield return new WaitForSeconds(GetRandomSpawnTime());
            }
            else
            {
                yield return new WaitForSeconds(firingRate);
            }
        }
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(firingRate - spawnTimeVariance, firingRate + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
