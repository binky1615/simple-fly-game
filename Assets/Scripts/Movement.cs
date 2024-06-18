using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space)){
            StartThrusting();
    }

    else{
        StopThrusting();
    }
    }
    
    void StopThrusting(){
        audioSource.Stop();
        mainEngineParticles.Stop();
        }

    void StartThrusting(){
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!audioSource.isPlaying){
                audioSource.PlayOneShot(mainEngine);
            }
            if(!mainEngineParticles.isPlaying){
              mainEngineParticles.Play();

            }
    }
    void ProcessRotation(){
    
        if(Input.GetKey(KeyCode.A)){
            Rotateleft();
        }
        else if(Input.GetKey(KeyCode.D)){
            Rotateright();
        }

        else {
            StopRotating();
        }
    }

    void Rotateleft(){
        ApplyRotation(rotationThrust);
        if(!rightThrusterParticles.isPlaying){
        rightThrusterParticles.Play();

        }
    }

    void Rotateright(){
        ApplyRotation(-rotationThrust);
        if(!leftThrusterParticles.isPlaying){
        leftThrusterParticles.Play();

        }
    }

    void StopRotating(){
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
