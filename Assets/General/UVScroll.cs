using UnityEngine;

public class UVScroll : MonoBehaviour
{
    // Scroll main texture based on time

    public float scrollSpeedX = 0.5f;
    public float scrollSpeedY = 0.5f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offsetx = Time.time * scrollSpeedX;
        float offsety = Time.time * scrollSpeedY;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offsetx, offsety));
    }
}