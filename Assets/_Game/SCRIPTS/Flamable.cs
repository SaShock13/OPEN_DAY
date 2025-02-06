using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._Game.SCRIPTS
{
    public class Flamable:MonoBehaviour
    {
        [SerializeField] float maxHeatToFire = 200;
        [SerializeField] private bool forcedIgnition = false;
        [SerializeField] private GameObject fireTrigger;
        [SerializeField] private float burningTimeInSec;
        [SerializeField] private Material burnedMaterial;

        public bool isMolotov = false;
        public float heatingSpeed = 100;
        public bool isOnFire = false;
        private FireSpreading fireSpreading;
        private bool isBurned = false;
        private ParticleSystem fireFX;
        private Renderer meshRenderer;
        public float currentHeat;


        
        private void Start()
        {
            fireFX = GetComponentInChildren<ParticleSystem>();
            meshRenderer = GetComponent<Renderer>();
            currentHeat = 0;
            if (fireTrigger!=null)
            {
                fireTrigger.SetActive(false);   
            } 
        }
        private void Update()
        {
            if (!isBurned)
            {
                if (forcedIgnition) Heating(5);
                CheckIfBurned(); 
            }
        }

        
        public void Heating(float coeff) 
        {            
            if (!isOnFire & !isBurned)
            {
                currentHeat += Time.deltaTime * heatingSpeed * coeff;
                Debug.Log($"{gameObject.name} heat is {currentHeat}");

                Debug.Log($"CurrenHeat: {currentHeat}");
            }
        }

        private void CheckIfBurned()
        {
            if (!isOnFire & !isBurned)
            {

                if (currentHeat >= maxHeatToFire)
                {
                    if (isMolotov)
                    {
                        var molotov = GetComponentInParent<Molotov>();
                        molotov.SetActive();
                        Debug.Log($"{molotov.name} Molotov Activated!!!");
                    }
                    else
                    {
                        fireFX.Play();
                        isOnFire = true;
                        forcedIgnition = false;
                        StartCoroutine(SetBurnedCoroutine());
                        fireSpreading = gameObject.AddComponent<FireSpreading>();
                        if (fireTrigger != null)
                        {
                            fireTrigger.SetActive(true);
                        }
                        Debug.Log($"{gameObject.name} is on fire , Added FireSpreading");
                    }
                } 
            }
        }

        IEnumerator SetBurnedCoroutine()
        {
            yield return new WaitForSeconds(burningTimeInSec);
            meshRenderer.material = burnedMaterial;            
            Destroy(fireSpreading);
            //fireFX.Stop();
            Destroy(fireFX.gameObject);
            Destroy(fireTrigger);
            isBurned = true;
            Destroy(this);
        }
    }
}
