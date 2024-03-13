using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hoverColor;

    private GameObject turret;
    private Color startColor;

    private void Start() {
        startColor = spriteRenderer.color;
    }

    private void OnMouseEnter() {
        spriteRenderer.color = hoverColor;
    }

    private void OnMouseExit() {
        spriteRenderer.color = startColor;
    }

    private void OnMouseDown() {
        if (turret != null) return;

        GameObject turretInProgress = BuildManager.main.GetSelectedTurret();
        turret = Instantiate(turretInProgress, transform.position, Quaternion.identity);
    }
}