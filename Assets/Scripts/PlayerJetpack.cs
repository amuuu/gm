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
            
            view.ControlJetpackParticleSystem(true);
        }
        else
        {
            view.ControlJetpackParticleSystem(false);
        }
    }

    public float GetJetpackCharge()
    {
        return model.jetpackCharge;
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
    GameObject particleSystemGameObject;
    ParticleSystem particleSystem;

    public JetpackView()
    {
        textUI = GameObject.FindGameObjectsWithTag("JetpackChargeText")[0];
        particleSystemGameObject = GameObject.FindGameObjectsWithTag("JetpackParticleSystem")[0];
        particleSystem = particleSystemGameObject.GetComponent<ParticleSystem>(); 

    }

    public void UpdateJetpackChargeTextUI(int value)
    {
        var textComponent = textUI.GetComponent<Text>();

        if (value != -1)
            textComponent.text = "JETPACK CHARGE: " + value + "%";
        else
            textComponent.text = "";
    }

    public void ControlJetpackParticleSystem(bool on)
    {
        if (on)
        {
            if(!particleSystem.isPlaying)
                particleSystem.Play();
        }
        else
        {
            if (particleSystem.isPlaying)
                particleSystem.Stop();
        }
    }
}

[CreateAssetMenu(menuName = "ScriptableObjects/JetpackConfig", order = 3)]
class JetpackConfigData : ScriptableObject
{
    public float jetpackChargeRate = 0.3f; // 0.3
    public float jetpackPower = 0.3f; // 0.3
}
