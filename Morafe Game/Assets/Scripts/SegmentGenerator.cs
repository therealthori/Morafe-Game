using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    public GameObject[] segment;
    [SerializeField] private int zPos = 50;
    [SerializeField] private bool creatingSegment = false;
    [SerializeField] private int segmentNum;
    
    void Update()
    {
        if (creatingSegment == false)
        {
            creatingSegment = true;
            StartCoroutine(SegmentGen());
        }
    }

    IEnumerator SegmentGen()
    {
        segmentNum = Random.Range(0, 3);
        Instantiate(segment[segmentNum],new Vector3(0,0,zPos),Quaternion.identity);
        zPos += 50;
        yield return new WaitForSeconds(3);
        creatingSegment = false;
    }

}
