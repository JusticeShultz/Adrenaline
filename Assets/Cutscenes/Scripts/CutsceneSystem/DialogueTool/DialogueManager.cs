using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPGDialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        [System.Serializable] public class OnTriggerEntered_Event : UnityEvent { }
        [System.Serializable] public class OnChatStarted_Event : UnityEvent { }
        [System.Serializable] public class OnChatEnded_Event : UnityEvent { }

        public OnTriggerEntered_Event OnTriggerEntered;
        public OnTriggerEntered_Event OnTriggerExited;
        public OnTriggerEntered_Event OnChatStarted;
        public OnTriggerEntered_Event OnChatEnded;

        [ReadOnlyField] public List<string> Names = new List<string>();
        [ReadOnlyField] public List<string> Chats = new List<string>();

        private void OnTriggerEnter(Collider other)
        {
            if(other.name == "Player")
                OnTriggerEntered.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "Player")
                OnTriggerExited.Invoke();
        }

        public void StartChat()
        {
            OnChatStarted.Invoke();
        }

        public void EndChat()
        {
            OnChatEnded.Invoke();
        }
    }
}