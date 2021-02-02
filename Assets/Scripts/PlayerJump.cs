using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class PlayerJump: MonoBehaviour
{
    public JumpConfigData jumpConfig;
    private JumpModel jumpModel;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpModel = new JumpModel();
    }

    void Update()
    {
        /*if (isJumpPressed && !twoTimesPressed && Input.GetKey(KeyCode.Space))
        {
            twoTimesPressed = true;
        }
        else if (!isJumpPressed && !twoTimesPressed && Input.GetKey(KeyCode.Space))
        {
            isJumpPressed = true;
        }*/

        if (!jumpModel.isJumpPressed && Input.GetKeyDown(KeyCode.Space))
        {
            jumpModel.isJumpPressed = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Floor")
        {
            jumpModel.isOnGround = true;
            jumpModel.isJumpPressed = false;
            //twoTimesPressed = false;
        }
    }

    void FixedUpdate()
    {
        if (jumpModel.isJumpPressed && jumpModel.isOnGround)
        {
            //Debug.Log("ONE TIME");
            rb.AddForce(new Vector2(0, 15 * jumpConfig.jumpTime), ForceMode2D.Impulse);

            jumpModel.isOnGround = false;
        }

        /*else if (isJumpPressed && !isOnGround && twoTimesPressed)
        {
            Debug.Log("TWO TIMES");
            rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }*/

    }

}

class JumpModel
{
    public bool isJumpPressed = false;
    public bool isOnGround = true;

    public JumpModel()
    {
        isJumpPressed = false;
        isOnGround = true;
    }
}


[CreateAssetMenu(menuName = "ScriptableObjects/JumpConfig", order = 2)]
class JumpConfigData : ScriptableObject
{
    public float jumpTime = 1.5f; // 1.5
}
