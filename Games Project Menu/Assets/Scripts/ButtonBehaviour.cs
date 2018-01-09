using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour {

    public void LoadLevelByIndex(int levelIndex)
    {
        Application.LoadLevel(1);
    }

	void Update () {
        if (Input.anyKeyDown)
            Application.LoadLevel(1);
	}
}
