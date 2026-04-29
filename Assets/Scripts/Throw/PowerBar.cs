using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PowerBar : MonoBehaviour
{
    public Slider slider;
    public float speed = 2f;
    public float maxForce = 15f;

    private float value;
    private bool increasing = true;
    private bool active = false;

    public bool IsActive => active;
    public float CurrentForce => value * maxForce;

    void Update()
    {
        if (!active) return;

        // Movimiento automático
        if (increasing)
            value += Time.deltaTime * speed;
        else
            value -= Time.deltaTime * speed;

        if (value >= 1f) increasing = false;
        if (value <= 0f) increasing = true;

        slider.value = value;

        // Click para confirmar
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            active = false;
        }
    }

    public void StartPower()
    {
        active = true;
        value = 0f;
        increasing = true;
        gameObject.SetActive(true);
    }

    public void StopPower()
    {
        active = false;
        gameObject.SetActive(false);
    }
}