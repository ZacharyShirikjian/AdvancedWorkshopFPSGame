using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    public Vector3 fromScale;
    public Vector3 toScale;


    // Start is called before the first frame update
    void Start()
    {
        fromScale = transform.localScale;
        toScale = new Vector3(1.5f, 1.5f, 1.5f);
        StartCoroutine(Grow(1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //ZACH: insert UI scripts here
            Destroy(gameObject);
        }
    }

    IEnumerator Grow(float time)
    {
        float i = 0;
        float rate = 1 / time;

        while(i < 1)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(fromScale, toScale, i);
            yield return 0;
        }
    }


}
