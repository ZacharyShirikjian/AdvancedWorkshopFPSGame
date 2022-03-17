using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//ATTACHED TO EVERY BUTTON/OPTION in the jukebox
//ADJUST THE PRICING IN THE INSPECTOR
public class JukeboxButton : MonoBehaviour
{
    //PURCHASE COST OF UPGRADE/MOD, IN MANTI COINS
    public int cost;

    //CHECK TO SEE IF THIS PARTICULAR BUTTON WAS USED
    //IF TRUE, DISABLE THAT BUTTON
    //IF FALSE, ENABLE THAT BUTTON
    public bool buttonUsed;

    public string upgradeName;

    [TextArea]
    public string upgradeDescription;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
