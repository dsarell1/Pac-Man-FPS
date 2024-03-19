using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterController : MonoBehaviour
{
    public GameObject blasterShot;

    [SerializeField]
    private float blasterSpeed = 50f;
    [SerializeField]
    private float timeToDestroy = 10f;


    public Vector3 target { get; set; }
    public bool hit { get; set; }

    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, blasterSpeed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position, target) < 0.01f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        GameObject blastObj = GameObject.Instantiate(blasterShot, contact.point + contact.normal * 0.001f, Quaternion.LookRotation(contact.normal));
        Destroy(gameObject);
        Destroy(blastObj, 2f);
    }
}
