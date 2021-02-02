using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class PlayerJetpack: MonoBehaviour
{
    private JetpackModel model;
    public JetpackConfigData jetpackConfig;
    private JetpackView view;

    private PlayerButtonInteraction buttonScript;
    private Rigidbody2D rb;

    void Start()
    {
        model = new JetpackModel();
        view = new JetpackView();

        buttonScript = this.GetComponent<PlayerButtonInteraction>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (buttonScript.isJetpackActive && !model.isActive)
        {
            model.isActive = buttonScript.isJetpackActive;
            view.UpdateJetpackChargeTextUI(100);
        }

        if ((model.isActive && Input.GetKey(KeyCode.G)) || (model.jetpackCharge <= 0.0f))
        {
            model.isActive = false;
            buttonScript.isJetpackActive = false;
            view.UpdateJetpackChargeTextUI(-1);
        }
    }

    private void FixedUpdate()
    {
        if (model.isActive && Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector2(0, 5 * jetpackConfig.jetpackPower), ForceMode2D.Impulse);
            model.jetpackCharge -= jetpackConfig.jetpackChargeRate;
            view.UpdateJetpackChargeTextUI((int)model.jetpackCharge);
        }
    }
}

class JetpackModel
{
    public float jetpackCharge;
    public bool isActive;

    public JetpackModel()
    {
        jetpackCharge = 100.0f;
        isActive = false;
    }
}

class JetpackView
{
    GameObject textUI;

    public JetpackView()
    {
        textUI = GameObject.FindGameObjectsWithTag("JetpackChargeText")[0];
    }

    public void UpdateJetpackChargeTextUI(int value)
    {
        var textComponent = textUI.GetComponent<Text>();

        if (value != -1)
            textComponent.text = "JETPACK CHARGE: " + value + "%";
        else
            textComponent.text = "";
    }
}

[CreateAssetMenu(menuName = "ScriptableObjects/JetpackConfig", order = 3)]
class JetpackConfigData : ScriptableObject
{
    public float jetpackChargeRate; // 0.3
    public float jetpackPower; // 0.3
}
