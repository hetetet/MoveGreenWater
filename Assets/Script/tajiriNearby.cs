using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tajiriNearby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Kane")
        {
            Debug.Log("��ó�� ����");
        }
        else if (collision.name == "sqeezer")
        {
            Debug.Log("��ó�� �����");
        }
    }
}
