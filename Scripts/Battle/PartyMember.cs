using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : Unit
{
    private int _currentExperience;
    private int _experienceNeededToNextLevel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) LevelUp(); 
    }
}
