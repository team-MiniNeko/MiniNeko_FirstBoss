using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3AudioManagerReceive : MonoBehaviour
{
    public void Receive(int i)
    {
        Boss3Audiomanager.instance.PlayAudio(i);
    }
}
