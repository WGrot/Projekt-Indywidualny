using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class FakeFloor : MonoBehaviour
{
    
    [SerializeField] private bool repairItself;
    [SerializeField] private GameObject floorBody;

    [Header("Time Settings")]
    [SerializeField] private float timeToCrack;
    [SerializeField] private float timeToRepair;

    [Header("Material Settings")]
    [SerializeField] private Material standardMaterial;
    [SerializeField] private Material crackedMaterial;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip crackingSound;
    [SerializeField] private AudioClip breakingSound;
    [SerializeField] private AudioClip repairSound;


    private MeshRenderer meshRenderer;
    private AudioSource audioSource;
    private ParticleSystem particleSystem;
    private bool isCracked = false;
    
    void Start()
    {
        meshRenderer = floorBody.GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Player Touched Platform");
        if (other.CompareTag("Player") && !isCracked)
        {
            IEnumerator crackingCoroutine = CrackFloor();
            StartCoroutine(crackingCoroutine);
        }
    }

    private IEnumerator CrackFloor()
    {
        isCracked = true;
        audioSource.clip = crackingSound;
        audioSource.Play();
        meshRenderer.material = crackedMaterial;
        yield return new WaitForSeconds(timeToCrack);
        PlayBreakParticles();
        audioSource.clip = breakingSound;
        audioSource.Play();
        floorBody.SetActive(false);
        yield return new WaitForSeconds(timeToRepair);
        if (repairItself)
        {
            audioSource.clip = repairSound;
            audioSource.Play();
            PlayRepairParticles();
            yield return new WaitWhile(() => audioSource.isPlaying);
            floorBody.SetActive(true);
            meshRenderer.material = standardMaterial;
            isCracked = false;
        }

    }

    private void PlayBreakParticles()
    {
        var main = particleSystem.main;
        var shape = particleSystem.shape;
        main.startLifetime = 1;
        main.startSpeed = 3;
        main.gravityModifier= 3;
        shape.position = Vector3.zero;
        particleSystem.Play();

    }

    private void PlayRepairParticles()
    {
        var main = particleSystem.main;
        var shape = particleSystem.shape;
        main.startSpeed = -1;
        main.gravityModifier = -0.1f;
        main.startLifetime = repairSound.length;
        shape.position = new Vector3(0, -4, 0);
        particleSystem.Play();

    }
}
