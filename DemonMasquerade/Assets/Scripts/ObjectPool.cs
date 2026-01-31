using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool SharedInstance;

    public List<GameObject> ammoList; //List of ammo.
    public GameObject ammo;

    public List<GameObject> enemyList;
    public GameObject enemy;

    public List<GameObject> maskList;
    public GameObject mask;

    public int amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ammoList = new List<GameObject>();
        GameObject tmpAmmo;
        for (int i = 0; i < amountToPool; i++)
        {
            tmpAmmo = Instantiate(ammo);
            tmpAmmo.SetActive(false);
            ammoList.Add(tmpAmmo);
        }

        enemyList = new List<GameObject>();
        GameObject tmpEnemy;
        for (int i = 0; i < amountToPool; i++)
        {
            tmpEnemy = Instantiate(enemy);
            tmpEnemy.SetActive(false);
            enemyList.Add(tmpEnemy);
        }

        maskList = new List<GameObject>();
        GameObject tmpMask;
        for (int i = 0; i < amountToPool; i++)
        {
            tmpMask = Instantiate(mask);
            tmpMask.SetActive(false);
            maskList.Add(tmpMask);
        }
    }

    public GameObject GetAmmo()  //Method to get ammo GameObject.
    {


        for (int i = 0; i < amountToPool; i++)
        {
            if (!ammoList[i].activeInHierarchy)
            {


                return ammoList[i];

            }
        }

        return null;
    }

    public GameObject GetEnemy()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!enemyList[i].activeInHierarchy)
            {


                return enemyList[i];

            }
        }

        return null;
    }

    public GameObject GetMask()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!maskList[i].activeInHierarchy)
            {


                return maskList[i];

            }
        }

        return null;
    }
}
