using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vege : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody vegeRigidbody;
    private Collider vegeCollider;
    private ParticleSystem bitsParticleEffect;

    private void Awake()
    {
        vegeRigidbody = GetComponent<Rigidbody>();
        vegeCollider = GetComponent<Collider>();
        bitsParticleEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        FindObjectOfType<GameManager>().IncreaseScore();

        whole.SetActive(false);
        sliced.SetActive(true);

        vegeCollider.enabled = false;
        bitsParticleEffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.velocity = vegeRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Sword sword = other.GetComponent<Sword>();
            Slice(sword.direction, sword.transform.position, sword.sliceForce);
        }
    }
}
