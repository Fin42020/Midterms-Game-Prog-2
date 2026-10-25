using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour,ISwitch
{
    [Header("Bullet Types")]
    [SerializeField] public GameObject FireBullet;
    [SerializeField] public GameObject WaterBullet;
    [SerializeField] public GameObject EarthBullet;

    [Header("Look Here Attributes")]
    [SerializeField] GameObject Target;
    public GameObject[] AllObjectsWithTag;

    Quaternion spawnRotation;
    public InputActionReference SwitchColor;
    public BulletType AmmoType;

    /*===================================================*/

    private void OnEnable()
    {
        StartCoroutine(Shoot());
        SwitchColor.action.started += Switch;
    }

    private void OnDisable()
    {
        SwitchColor.action.started -= Switch;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Time.timeScale = 0;
            TakeDamage();
        }
    }


    private void Update()
    {
        AllObjectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestObject = AllObjectsWithTag[0];
        float distanceToNearest = Vector3.Distance(Target.transform.position, nearestObject.transform.position);

        for (int i = 1; i < AllObjectsWithTag.Length; i++)
        {
            float distanceToCurrent = Vector3.Distance(Target.transform.position, AllObjectsWithTag[i].transform.position);
            
            if (distanceToCurrent < distanceToNearest) 
            {
                nearestObject = AllObjectsWithTag[i];
                distanceToNearest = distanceToCurrent;
            }
        }
        Vector3 look = transform.InverseTransformPoint(nearestObject.transform.position);
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg + 180;
        transform.Rotate(0,0,angle);
    }
    /*===================================================*/

    IEnumerator Shoot()
    {
        while (true)
        {
            SpawnBullet();
            yield return new WaitForSeconds(1);
        }
    }

    public void Switch(InputAction.CallbackContext obj)
    {
        switch (AmmoType)
        {
            case BulletType.Fire:
                AmmoType = BulletType.Water;
                Debug.Log("I'm Water");
                break;
            case BulletType.Water:
                AmmoType = BulletType.Earth;
                Debug.Log("I'm Earth");
                break;
            case BulletType.Earth:
                AmmoType = BulletType.Fire;
                Debug.Log("I'm Fire");
                break;
        }
    }

    public void SpawnBullet()
    {
        switch (AmmoType)
        {
            case BulletType.Fire:
            Instantiate(FireBullet, Target.transform.position, Target.transform.rotation);
                break;
            case BulletType.Water:
            Instantiate(WaterBullet, Target.transform.position, Target.transform.rotation);
                break;
            case BulletType.Earth:
            Instantiate(EarthBullet, Target.transform.position, Target.transform.rotation);
                break;
        }
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }

}
