using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJetpack : MonoBehaviour
{

    public float jetpackPower;
    public float jetpackChargeRate;

    private float jetpackCharge;

    private bool isActive;
    private PlayerButtonInteraction buttonScript;
    private Rigidbody2D rb;

    void Start()
    {
        isActive = false;
        jetpackCharge = 100.0f;
        buttonScript = this.GetComponent<PlayerButtonInteraction>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (buttonScript.isJetpackActive && !isActive)
        {
            isActive = buttonScript.isJetpackActive;
            UpdateTextUI(100);
        }

        if ((isActive && Input.GetKey(KeyCode.G)) || (jetpackCharge <= 0.0f))
        {
            isActive = false;
            buttonScript.isJetpackActive = false;
            UpdateTextUI(-1);
        }

    }

    private void FixedUpdate()
    {
        if (isActive && Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector2(0, 5 * jetpackPower), ForceMode2D.Impulse);
            jetpackCharge -= jetpackChargeRate;
            UpdateTextUI((int) jetpackCharge);
        }
    }

    void UpdateTextUI(int value)
    {
        GameObject jetpackChargeText = GameObject.FindGameObjectsWithTag("JetpackChargeText")[0];

        var text = jetpackChargeText.GetComponent<Text>();

        if (value != -1)
            text.text = "JETPACK CHARGE: " + value + "%";
        else
            text.text = "";
    }
}
