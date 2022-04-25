using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    public float kills;
    // Start is called before the first frame update
    void Start()
    {
        kills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void KillCounting(float value)
    {
        kills += value;
    }
    public float  GetDamage()
    {
        return kills ;
    }
}
