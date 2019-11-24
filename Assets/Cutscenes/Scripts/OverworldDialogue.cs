using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGDialogueSystem;

public class OverworldDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    bool InChatRange = false;
    bool tap = false;
    bool talkedTo = false;
    float talkCooldown = 0f;

    public void Update()
    {
        if(talkedTo)
        {
            talkCooldown += Time.deltaTime;

            if (talkCooldown >= 0.75f)
                talkedTo = false;

            return;
        }

        if (dialogueManager.Names.Count == dialogueManager.Chats.Count)
        {
            if (Input.GetAxis("Fire1") != 0 || Input.GetKey(KeyCode.E))
            {
                if (!tap && InChatRange && ChatStack.instance.chatStack.Count == 0)
                {
                    tap = true;
                    talkedTo = true;
                    
                    for (int i = 0; i < dialogueManager.Names.Count; i++)
                    {
                        ChatStack.instance.AddChat(dialogueManager.Names[i], dialogueManager.Chats[i]);

                        if (i == 0)
                            ChatStack.instance.firstChat = true;
                    }
                }
            }
            else tap = false;
        }
        else
        {
            Debug.LogError("Warning! Tried to read a Dialogue Manager with inconsistent names and chats.");
        }
    }

    public void SetChatRangeToggle(bool input)
    {
        InChatRange = input;
    }
}