using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    public Vector3 fromScale;
    public Vector3 toScale;
    public float scaleUp = 1.5f;
    public float speed = 20.0f;

    private AudioSource source;
    private UITest uiScript;
    // Start is called before the first frame update
    void Start()
    {
        source = this.GetComponent<AudioSource>();
        fromScale = transform.localScale;
        toScale = new Vector3(fromScale.x * scaleUp, fromScale.y * scaleUp, fromScale.z * scaleUp);
        uiScript = GameObject.Find("Canvas").GetComponent<UITest>();
        StartCoroutine(Grow(2.0f));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.right * Time.deltaTime * speed, Space.Self);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(source.clip, gameObject.transform.position);
            uiScript.numCoins++;
            uiScript.coinText.SetText(uiScript.numCoins.ToString());
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
