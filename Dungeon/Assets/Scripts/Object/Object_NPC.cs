using UnityEngine;
using Fungus;

public class Object_NPC : Object_Interactable
{
    public Flowchart flowchart;

    public override void Interact()
    {
        flowchart.ExecuteBlock("Start");
    }
}
