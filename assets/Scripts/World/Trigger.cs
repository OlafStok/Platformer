using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script triggers other objects. if move is selected, it will move a platformcontroller. It switches to active when hit.

public class Trigger : MonoBehaviour {

    public GameObject objectToTrigger;
    private PlatformController platformController;
    private Switcher switcher;
    public string input;
    public bool active;
	public int colorvalue;

    // Use this for initialization
    void Start() {
        getObjectComponent(input);
    }

    // Update is called once per frame
    void Update() {
    }

    public void GotHit()
    {
        SetValueOfComponent(active);
        print("collided!");
    }

    void getObjectComponent(string componentToGet) {
        {
            if (objectToTrigger != null)
            {
                switch (componentToGet)
                {
                    case "move":
                            platformController = objectToTrigger.GetComponent<PlatformController>();
                        break;
                    case "color":
                        switcher = objectToTrigger.GetComponent<Switcher>();
                        break;
                    case "":
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void SetValueOfComponent(bool boolValue)
    {
		//make platform move or not move
        if(platformController != null)
        {
            platformController.moving = boolValue;
        }

		//make colorswitcher changecolors to 1, 2, 3, or 4, or 0 if off
        if(switcher != null)
        {
            if(boolValue) {
            switcher.changeColors = colorvalue;
            }
            else
            {
                switcher.changeColors = 0;
            }
        }
    }



}
