using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Header("Attribute")] 
    [SerializeField] private int defaultHP = 1;

    private bool isDestroyed = false;
    private int HP;

    public void Awake() {
        getHP();
    }

    public void getHP() {
        HP = Mathf.RoundToInt(defaultHP * Mathf.Pow(LevelManager.main.waveNum, LevelManager.main.diff_Factor));
    }

    public void TakeDamage(int dmg) {
        HP -= dmg;

        if (HP <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyDestroy.Invoke();
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
