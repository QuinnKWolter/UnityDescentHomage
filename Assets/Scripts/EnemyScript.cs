using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject l_cannon, r_cannon, projectile, explosion, shieldPickup;
  	public AudioClip ExplosionSound, AlertSound, DeathSound;
    private AudioSource ExplosionSource, AlertSource, DeathSource;
    float health = 100f;
    Transform player;
    RaycastHit hit;
    Vector3 rayDirection;
    float speed, distance;
    bool isAlerted, canShoot, ded;

    void Start () {
        player = GameObject.FindWithTag("Player").transform;
        speed = 0.2f;
        isAlerted = false;
        canShoot = true;
        ded = false;
        ExplosionSource = AddAudio(ExplosionSound, false, false, 1);
        AlertSource = AddAudio(AlertSound, false, false, 1);
        DeathSource = AddAudio(DeathSound, false, false, 1);
    }

    void Update () {
        distance = Vector3.Distance(transform.position, player.position);
       if (isAlerted && distance > 20 && !ded) {
            transform.LookAt(player.position);
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed);
       }

       if (isAlerted && distance < 50 && !ded) {
         transform.LookAt(player.position);
         if (canShoot){
            Fire(player.position);
            canShoot = false;
            StartCoroutine(ShootDelay());
          }
       }

       if (!isAlerted && !ded) {
         rayDirection = player.transform.position - transform.position;
         if (Physics.Raycast (transform.position, rayDirection, out hit)) {
             if (hit.transform == player) {
                 Debug.Log("I SEEEEEE YOUUU");
                 AlertSource.Play();
                 isAlerted = true;
             }
         }
       }
    }

    void Fire(Vector3 target)
    {
       GameObject bullet1 = (GameObject)Instantiate(projectile, l_cannon.transform.position + new Vector3(0, 0, 0), l_cannon.transform.rotation);
       GameObject bullet2 = (GameObject)Instantiate(projectile, r_cannon.transform.position + new Vector3(0, 0, 0), r_cannon.transform.rotation);

       //Add velocity to the projectile
 			bullet1.GetComponent<Rigidbody> ().velocity = bullet1.transform.up * 30;
 			bullet2.GetComponent<Rigidbody> ().velocity = bullet2.transform.up * 30;

 			//Destroy projectile after 2 seconds
 			Destroy (bullet1, 2.0f);
 			Destroy (bullet2, 2.0f);
    }

    public void Damage(float amount){
      health -= amount;
      if (health <= 0){
        ded = true;
        DeathSource.Play();
        StartCoroutine(DeathDelay());
      }
    }

    IEnumerator ShootDelay()
     {
       yield return new WaitForSeconds(2f);
       canShoot = true;
     }

     IEnumerator DeathDelay()
      {
        yield return new WaitForSeconds(2f);
        Instantiate(explosion, transform.position, transform.rotation);
    		ExplosionSource.Play();
        Instantiate(shieldPickup, transform.position, transform.rotation);
        Destroy(gameObject);
      }

      public AudioSource AddAudio (AudioClip clip, bool loop, bool playAwake, float vol) {
         AudioSource newAudio = gameObject.AddComponent<AudioSource>();
         newAudio.clip = clip;
         newAudio.loop = loop;
         newAudio.playOnAwake = playAwake;
         newAudio.volume = vol;
         return newAudio;
     }
}
