using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public int damage = 1;
    public int range = 50;
    public int force = 150;
    public Camera cam;
    public Transform blasterPart;
    public ParticleSystem blast;
    public Light blastFlare;
    //public Transform shotPart;
    public Transform blasterParent;
    public GameObject blasterPrefab;

    float rotationY = 0.0f;

    // Update is called once per frame
    void Update()
    {
        PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (!player.gameOver)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit blaster;
                blast.Play();
                //StartCoroutine(LightDisplay());
                //blasterPrefab.transform.localRotation = Quaternion.Euler(90, 0, 0);
                rotationY = player.transform.rotation.y;
                GameObject bullet = GameObject.Instantiate(blasterPrefab, blasterPart.position, blasterPart.rotation, blasterParent);
                BlasterController blasterController = bullet.GetComponentInChildren<BlasterController>();
                
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out blaster, range))
                {
                    Debug.Log(blaster.transform.name);
                    blasterController.target = blaster.point;
                    blasterController.hit = true;
                    
                    PacmanButler pacmanButler = blaster.transform.GetComponent<PacmanButler>();
                    if (pacmanButler != null)
                    {
                        pacmanButler.TakeDamage(damage);
                    }
                    UnlockedDoor door = blaster.transform.GetComponent<UnlockedDoor>();
                    if (door != null)
                    {
                        door.TakeDamage(damage);
                    }
                    PacmanWizard pacmanBoss = blaster.transform.GetComponent<PacmanWizard>();
                    if (pacmanBoss != null)
                    {
                        pacmanBoss.BossTakeDamage(damage);
                    }
                    PacmanTopHat pacmanBoss2 = blaster.transform.GetComponent<PacmanTopHat>();
                    if (pacmanBoss2 != null)
                    {
                        pacmanBoss2.BossTakeDamage(damage);
                    }
                    if (blaster.rigidbody != null && pacmanBoss == null && pacmanBoss2 == null)
                    {
                        blaster.rigidbody.AddForce(-blaster.normal * force);
                    }
                } 
                else
                {
                    blasterController.target = cam.transform.position + cam.transform.forward * 25;
                    blasterController.hit = false;
                }
            }
        }
    }
    IEnumerator LightDisplay()
    {
        blastFlare.gameObject.SetActive(true);
        yield return new WaitForSeconds(.01f);
        blastFlare.gameObject.SetActive(false);
    }
}
