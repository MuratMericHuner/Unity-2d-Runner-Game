using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    public Text count;
    // Update is called once per frame
    void Update()
    {
        count.text = PlayerMovement.lifeCount.ToString();
    }
}
