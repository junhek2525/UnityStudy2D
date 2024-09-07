using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerManager : MonoBehaviour
{
    public float leftLimit = 0.0f; //哭率 胶内费 力绢
    public float rightLimit = 0.0f; //坷弗率 
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;

    public GameObject subScreen;

    public bool isForceScrollx = false;
    public float forceScrollSpeedx = 0.5f;
    public bool isForceScrolly = false;
    public float forceScrollSpeedy = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = transform.position.z;

            if (isForceScrollx)
            {
                x = transform.position.x + (forceScrollSpeedx * Time.deltaTime);
            }

        if(x < leftLimit)
            {
                x = leftLimit;
            }else if(x > rightLimit)
            {
                x = rightLimit;
            }
            if (isForceScrolly)
            {
                y = transform.position.y + (forceScrollSpeedy * Time.deltaTime);
            }

            if (y < bottomLimit)
            {
                y = bottomLimit;
            }else if(y > topLimit)
            {
                y = topLimit;
            }
                Vector3 v3 = new Vector3(x, y, z);
            transform.position = v3;
            
            if(subScreen != null)
            {
                y = subScreen.transform.position.y;
                z = subScreen.transform.position.z;
                Vector3 v = new Vector3(x /2.0f,y,z);
                subScreen.transform.position = v;
            }        
                    }
    }
}
