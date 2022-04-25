using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drapfel : MonoBehaviour
{
    public float speed;
    public float countdown;
    public float yRotation;
    public float xMax = 75f;
    public float xMin = 0f;
    public float zMin = -85f;
    public float zMax = 0f;
    public Transform firePoint;
    public GameObject bombPrefab;


    
    void Start()
    {
        countdown = 3f;
        transform.position += new Vector3(0f,6f,0f);
    }

    
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f)
        {
            yRotation = Random.Range(0f, 360f);
            Shoot();
            countdown = Random.Range(1f, 6f);
        }
        if (transform.position.z <= zMin)
        {
            yRotation = 270f;
        }
        if (transform.position.z >= zMax)
        {
            yRotation = 90f;
        }
        if (transform.position.x <= xMin)
        {
            yRotation = 0f;
        }
        if (transform.position.x >= xMax)
        {
            yRotation = 180f;
        }
        transform.rotation = Quaternion.Euler(90f, yRotation ,-90f);
        transform.position += transform.up * speed * Time.deltaTime;

    }
    public void Shoot()
    {
        Instantiate(bombPrefab, firePoint.position, firePoint.rotation);
        GameObject projectileGO = (GameObject)Instantiate(bombPrefab , firePoint.position, firePoint.rotation);
        Bomb  bomb = projectileGO.GetComponent<Bomb>();
        if (bomb != null)
        {
            bomb .Seek(gameObject);

        }
    }
}
