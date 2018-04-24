using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour {
public Clock clock;


void Update()
{
    GetComponent<TextMeshProUGUI>().text=(clock.second[3].ToString()+clock.second[2].ToString()+":"+clock.second[1].ToString()+clock.second[0].ToString());
}




}
