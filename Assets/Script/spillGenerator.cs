using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spillGenerator : MonoBehaviour
{
    public GameObject spillWater;
    // Start is called before the first frame update
    public IEnumerator Spill(Transform transform)
    {
        Debug.Log("spill greenwater on "+ transform.position.x+", "+ transform.position.y);
        GameObject spillWaterobj=Instantiate(spillWater, transform.position, transform.rotation);
        spillWaterobj.SetActive(true);
        
        yield return new WaitForSeconds(1);
        SpriteRenderer spillWaterSprite = spillWaterobj.GetComponent<SpriteRenderer>();  
        
        for(int i = 0; i < 20; i++)
        {
            spillWaterSprite.color = new Color(1f, 1f, 1f, 1f - 0.1f*i);
            yield return new WaitForSeconds(0.05f);
        }
        spillWaterSprite.color = new Color(1f, 1f, 1f, 0f);

        Debug.Log("dry greenwater on " + transform.position.x + ", " + transform.position.y);
        Destroy(spillWaterobj);
    }
}
