using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Attributes
    Animator playerAnimator;
    [SerializeField] GameObject bulletGO;
    [SerializeField] Transform bulletSpawnTransform;
    [SerializeField] float bulletSpeed;
    bool isShootingOn = false;
    private Transform playerSpawnerCenter;
    [SerializeField] float goToCenterSpeed;


    [SerializeField] AudioSource playerAudioSource;
    [SerializeField] AudioClip ShootClip;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerSpawnerCenter = transform.parent.gameObject.transform;
        Player_Spawner_Controller playerSpawner = transform.parent.gameObject.GetComponent<Player_Spawner_Controller>();
        playerAudioSource = playerSpawner.playerSpawnerAudioSource;
    }


    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerSpawnerCenter.position, Time.fixedDeltaTime * goToCenterSpeed);
    }

    public void StartShooting()
    {
        StartShootingAnim();
        isShootingOn = true;
        StartCoroutine("Shooting");
    }

    public void StopShooting()
    {
        isShootingOn = false;
        StartRunningAnim();
    }

    private void StartShootingAnim()
    {
        playerAnimator.SetBool("isShooting", true);
        playerAnimator.SetBool("isRunning", false);
    }


    private void StartRunningAnim()
    {
        playerAnimator.SetBool("isRunning", true);
        playerAnimator.SetBool("isShooting", false);
    }

    public void StartIdleAnim()
    {
        playerAnimator.SetBool("isRunning", false);
        playerAnimator.SetBool("isLevelFinished", true);
    }

    IEnumerator Shooting()
    {
        while(isShootingOn)
        {
            yield return new WaitForSeconds(.5f);
            Shoot();
            yield return new WaitForSeconds(2f);
        }

    }

    private void Shoot()
    {
        PlayShootingAudio();
        // Instantiate bullet in easy way => Create the place we want to instantiate them
        GameObject bullet = Instantiate(bulletGO, bulletSpawnTransform.position, Quaternion.identity);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.velocity = transform.forward * bulletSpeed;
    }

    private void PlayShootingAudio()
    {
        if(playerAudioSource != null)
        {
            playerAudioSource.PlayOneShot(ShootClip, 0.3f);
        }
    }
}
