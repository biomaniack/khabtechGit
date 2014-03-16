using UnityEngine;
using System.Collections;

public class PathFind : MonoBehaviour
{
    public bool go2 = false;
    public bool go3 = false;
    public bool go4 = false;    

    public Vector3 pos0, pos1, pos2, pos3, pos4;
    public float speed = 0.025f;
    public string nameBase;
   
    void Start()
    {
        pos1 = transform.position;
        go2 = true;      
    }

    void Update()
    {        
        pos0 = transform.position;
        if (go2)
        {
            pos0 = Vector3.MoveTowards(transform.position, pos2, speed);
            if (pos0 == pos2)
            {
                go3 = true;
                go2 = false;
            }
        }
        if (go3)
        {
            pos0 = Vector3.MoveTowards(transform.position, pos3, speed);
            if (pos0 == pos3)
            {
                go4 = true;
                go3 = false;
            }
        }
        if (go4)
        {
            pos0 = Vector3.MoveTowards(transform.position, pos4, speed);
        }

        if (pos0 == pos4)
        {
            GameObject.Find(nameBase).GetComponent<Movement>().ted2 += 100;
            Destroy(gameObject);
        }
        transform.position = pos0;
    }
}
