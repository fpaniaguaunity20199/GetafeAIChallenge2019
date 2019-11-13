using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatGenerator : MonoBehaviour
{
    public GameObject catPrefab;
    public Transform higherLeftLimit;
    public Transform lowerRightLimit;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(catPrefab, 
            new Vector3(
            Random.Range(higherLeftLimit.position.x, lowerRightLimit.position.x), 
            0, 
            Random.Range(higherLeftLimit.position.z, lowerRightLimit.position.z)), 
            Quaternion.identity);
    }

}
