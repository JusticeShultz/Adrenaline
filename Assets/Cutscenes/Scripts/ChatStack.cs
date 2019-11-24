using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatStack : MonoBehaviour
{
    public static ChatStack instance;

    public class Chat
    {
        public string Name;
        public string Dialogue;

        public Chat() { }

        public Chat(string name, string dialogue)
        {
            Name = name;
            Dialogue = dialogue;
        }
    }

    public Queue<Chat> chatStack = new Queue<Chat>();

    public Text chat_name;
    public Text chat;
    public GameObject visuals;

    bool tap = false;

    [HideInInspector] public bool firstChat = true;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (chatStack.Count > 0)
        {
            if(firstChat)
            {
                chat_name.text = chatStack.Peek().Name;
                chat.text = chatStack.Peek().Dialogue;
                chatStack.Dequeue();
                firstChat = false;
                tap = true;
            }

            visuals.SetActive(true);

            if (Input.GetAxis("Fire1") != 0 || Input.GetKeyDown(KeyCode.E))
            {
                if (!tap)
                {
                    tap = true;

                    chat_name.text = chatStack.Peek().Name;
                    chat.text = chatStack.Peek().Dialogue;
                    chatStack.Dequeue();
                }
            }
            else tap = false;
        }
        else
        {
            firstChat = false;
            visuals.SetActive(false);
        }
    }

    public void AddChat(string name, string dialogue)
    {
        chatStack.Enqueue(new Chat(name, dialogue));
    }
}