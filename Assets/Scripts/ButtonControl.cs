using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    private SpriteRenderer spriteRen;
    public Sprite defaultImage;
    public Sprite pressed;
    public KeyCode keyPressed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyPressed)) {
            spriteRen.sprite = pressed;
        }

        if (Input.GetKeyUp(keyPressed)) {
            spriteRen.sprite = defaultImage;
        }
    }
}
