using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionText : MonoBehaviour {
    [SerializeField] Text version_Text;
    void Start() {

        version_Text.text = ("Application Version: " + Application.version);
        
    }
}
