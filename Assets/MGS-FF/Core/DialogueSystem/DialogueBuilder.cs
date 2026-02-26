using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueBuilder
    {
        private Dictionary<string, Func<DialogueNodesContainer>> _dialoguesMap;
        
        public DialogueBuilder()
        {
            _dialoguesMap = new()
            {
                {"Cube1", BuildCube1}
            };
        }

        public DialogueNodesContainer GetDialogueByID(string ID)
        {
            if (_dialoguesMap.TryGetValue(ID, out var builder))
            {
                return builder?.Invoke();
            }

            return DialogueNodesContainer.GetEmpty();
        }

        private DialogueNodesContainer BuildCube1()
        {
            DialogueNodesContainer container = new DialogueNodesContainer();
            container.Append(new SetPlayerNode(new Vector3(-0.5f,0,-1.9f), Quaternion.Euler(0,-90,0)));
            container.Append(new SetDialogueCameraNode(new Vector3(0,3,-5), Quaternion.Euler(25,-25,0)));
            container.Append(new ToggleCameraNode(true));
            container.Append(new ShowTextDialogueNode("Hi! I'm Cube"));
            container.Append(new ShowTextDialogueNode("Wow!"));
            container.Append(new ShowTextDialogueNode("Yeah"));
            container.Append(new ToggleCameraNode(false));
            return container;
        }
    }
}