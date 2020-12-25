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

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        jetpackCharge = 100.0f;
        buttonScript = this.GetComponent<PlayerButtonInteraction>();
        rb = GetComponent<Rigidbody2D>();
        UpdateTextUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonScript.isJetpackActive && !isActive)
        {
            isActive = buttonScript.isJetpackActive;
        }

        if ((isActive && Input.GetKey(KeyCode.G)) || (jetpackCharge <= 0.0f))
        {
            isActive = false;
            buttonScript.isJetpackActive = false;
        }

        UpdateTextUI();
    }

    private void FixedUpdate()
    {
        if (isActive && Input.GetKey(KeyCode.W))
        {
            Debug.Log("ASFUGADFUAJSD");
            rb.AddForce(new Vector2(0, 5 * jetpackPower), ForceMode2D.Impulse);
            jetpackCharge -= jetpackChargeRate;
            UpdateTextUI();
        }
    }

    void UpdateTextUI()
    {
        GameObject jetpackChargeText = GameObject.FindGameObjectsWithTag("JetpackChargeText")[0];

        var text = jetpackChargeText.GetComponent<Text>();

        text.text = "JETPACK CHARGE: " + (int)jetpackCharge + "%"; 
    }
}
