using UnityEngine;
using Zenject;

namespace Assets._Game.SCRIPTS
{
    public class Flamable:MonoBehaviour
    {
        [SerializeField] float maxHeatToFire = 200;
        [SerializeField] private bool forcedIgnition = false;
        public float heatingSpeed = 100;
        [SerializeField] private GameObject fireTrigger;


        public bool isOnFire = false;
        private ParticleSystem fireFX;
        public float currentHeat;

        private void Start()
        {
            fireFX = GetComponentInChildren<ParticleSystem>();
            currentHeat = 0;
            if (fireTrigger!=null)
            {
                fireTrigger.SetActive(false);   
            } 
        }
        private void Update()
        {
            if (forcedIgnition) Heating();
            CheckIfBurned();
        }

        
        public void Heating() 
        {            
            if (!isOnFire)
            {
                currentHeat += Time.deltaTime * heatingSpeed;
                Debug.Log($"{gameObject.name} heat is {currentHeat}");

            }
            Debug.Log($"CurrenHeat: {currentHeat}");
        }

        private void CheckIfBurned()
        {
            if (!isOnFire)
            {
                if (currentHeat >= maxHeatToFire)
                {                   
                    fireFX.Play();
                    isOnFire = true;
                    forcedIgnition = false;

                    gameObject.AddComponent<FireSpreading>();
                    if (fireTrigger != null)
                    {
                        fireTrigger.SetActive(true);
                    }
                    Debug.Log($"{gameObject.name} is on fire , Added FireSpreading");
                } 
            }

        }
    }
}
