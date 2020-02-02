using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour
{
    public KeyCode _KeyTimeWalkBack = KeyCode.Q;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_KeyTimeWalkBack))
        {
            TimeWalkBack();
        }
    }

    void TimeWalkBack()
    {

    }
}
