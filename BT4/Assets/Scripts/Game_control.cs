using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// vi du delegate
public class Game_control : MonoBehaviour
{
    private Character_control characterControl;
    // Start is called before the first frame update
    void Start()
    {
        characterControl = new Character_control();
        characterControl.callBack = CharacterLifeEvent;
    }

    // Update is called once per frame
    void Update()
    {
        characterControl.Update();
    }

    void CharacterLifeEvent (float value)
    {

    }    
}
