using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Enemy Copies
    [SerializeField] private GameObject waspCopy;

    //Enemy List
    [SerializeField] public List<GameObject> enemyList;
    [SerializeField] private List<GameObject> enemyCopies;

    //Spawn Locations
    [SerializeField] private List<GameObject> flyingLoc;
    //[SerializeField] private List<GameObject> groundLoc;

    public static int count;

    // Start is called before the first frame update
    void Start()
    {
        waspCopy.SetActive(false);

        enemyCopies.Add(waspCopy);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSystem.next)
        {
            for(int i = 0; i < 1; i++)
            {
                GameObject enemyCopy = enemyCopies[Random.Range(0, enemyCopies.Count - 1)];
                Vector3 newLocation = GetLocation(enemyCopy)[Random.Range(0, GetLocation(enemyCopy).Count - 1)].transform.position;
                enemyCopy = ObjectUtils.SpawnDefault(enemyCopy, this.transform.parent, newLocation);
                enemyList.Add(enemyCopy);
                enemyCopy.SetActive(true);
            }
            GameSystem.next = false;                    
        }
        
        for(int i = 0; i < enemyList.Count; i++)
        {
            if(enemyList[i] == null)
            {
                enemyList.RemoveAt(i);
            }
        }
        count = enemyList.Count;
    }

    private List<GameObject> GetLocation(GameObject toSpawn)
    {
        List<GameObject> location = null;

        switch (toSpawn.GetComponent<Enemy>().type)
        {
            case "WASP":
                location = this.flyingLoc;
                break;              
        }

        return location;
    }
}

public class ObjectUtils
{
    public static GameObject SpawnDefault(GameObject toSpawn, Transform parent, Vector3 localPos)
    {
        GameObject myObj = GameObject.Instantiate(toSpawn, parent);
        myObj.transform.localPosition = localPos;
        
        return myObj;
    }   
}
