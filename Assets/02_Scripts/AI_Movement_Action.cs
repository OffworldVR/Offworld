using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Move")]
public class MoveAction : Action
{
    public GameObject MovementScript;

    public override void Act(StateController controller)
    {
        Movement(controller);
    }

    private void Movement(StateController controller)
    {
        MovementScript.GetComponent<PlayerScript>().Move();
    }
}
