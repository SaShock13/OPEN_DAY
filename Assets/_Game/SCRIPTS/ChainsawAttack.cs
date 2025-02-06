using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChainsawAttack : MonoBehaviour
{
    [SerializeField] GameObject bloodFXPrefab;

    private GameObject bloodFx;
    private EnemyHealth enemyHealth;
    private Vector3 FXPosition;
    [SerializeField] private Image bloodSplashImage;

    

    private void Start()
    {
        bloodFx = Instantiate<GameObject>(bloodFXPrefab) ;
        bloodFx.SetActive(false) ;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out enemyHealth))
        {
            FXPosition = collision.GetContact(0).point;
            bloodFx.transform.position = FXPosition;
            bloodFx.transform.rotation = Quaternion.LookRotation(collision.GetContact(0).normal);
            bloodFx.SetActive(true);
            bloodFx.GetComponentInChildren<ParticleSystem>().Play();
            StartCoroutine(SawCutting());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            bloodSplashImage.enabled = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        bloodSplashImage.enabled = false;
        StopAllCoroutines();
    }

    IEnumerator SawCutting()
    {
        while (true)
        {
            enemyHealth.TakeDamage(100); 
            yield return new WaitForSeconds(1);
        }
    }
}
