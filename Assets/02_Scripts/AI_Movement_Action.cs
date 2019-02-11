using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Move")]
public class MoveAction : Action
{
    public GameObject MovementScript;

    public override void Act(StateController controller)
    {
        Move(controller);
    }

    private void Move(StateController controller)
    {
        MovementScript.GetComponent<PlayerScript>().Move();
    }
}
