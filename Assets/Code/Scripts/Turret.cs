using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")] 
    [SerializeField] private float targetingRange;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bps; //bullet per second

    private Transform target;
    private float timeUntilFire;
    private float defaultBPS = 1f;
    private float defaultTargetingRange = 5f;
    public static UnityEvent onNewWave = new UnityEvent();

     private void Awake() {
        onNewWave.AddListener(ApplyNerfs);
    }

    private void Update() {

        if (target == null) {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange()){
            target=null;
        } else {

            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f/bps) {
                Shoot();
                timeUntilFire = 0f;
            }
        }


    }

    private void Shoot() {

        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(turretRotationPoint.position, targetingRange, (Vector2)turretRotationPoint.position, targetingRange, enemyMask);

        if (hits.Length > 0) {
            target = hits[0].transform;
        } else {
            target = null; // No target found
        }
    }

    private bool CheckTargetIsInRange() {
        return (Vector2.Distance(target.position, transform.position) <= targetingRange);
    }
    private void RotateTowardsTarget() {
        float angle;
        Vector2 turretPosition = transform.position;
        Vector2 targetPosition = target.position;
        angle = Mathf.Atan2((targetPosition.y - turretPosition.y), (targetPosition.x - turretPosition.x)) * Mathf.Rad2Deg - Mathf.PI;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f , 0f, angle));
        //turretRotationPoint.rotation = targetRotation;
        
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ApplyNerfs() {
        if (LevelManager.main.weather == "Heatwave") {
            bps = defaultBPS * LevelManager.main.diff_Factor;
            targetingRange = defaultTargetingRange;

        } else if (LevelManager.main.weather == "Snowstorm") {
            targetingRange = defaultTargetingRange * LevelManager.main.diff_Factor;
            bps = defaultBPS;

        // } else if (LevelManager.main.weather == "Rainstorm") {
        }
    }
    
    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}