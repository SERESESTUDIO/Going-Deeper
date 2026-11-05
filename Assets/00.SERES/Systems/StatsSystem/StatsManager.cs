using SERES.Systems.PhaseSystem;
using SERES.Systems.TimeSystem;
using SERES.Definitions;
using UnityEngine;
namespace SERES.Systems.StatsSystem
{
    public class StatsManager : PhaseManager
    {
        public TimeManager timeManager;
        public StatsDefinitionObject startStats;
        [SerializeField]
        private StatsDefinition _statsDefinitions;

        //dinamic stats
        private int _thirstIndex = 0;
        private int _hungerIndex = 0;
        [SerializeField]
        private int _energyIndex = 0;

        private void Awake()
        {
            StatsDefinitionObject statsDefinitionObject = Instantiate(startStats);
            _statsDefinitions = statsDefinitionObject.statsDefinition;
        }

        public override void OnGamePlay()
        {
            base.OnGamePlay();
            if (timeManager.GetDate() != null && timeManager.GetDate() != "")
            {
                float timeMultiplier = GetMultiplier(timeManager.GetTimeScale());
                string time = timeManager.GetDate().Split(" ")[1];
                int hour = int.Parse(time.Split(':')[0]);
                int minute = int.Parse(time.Split(':')[1]);
                float timer = DateToTimer(hour, minute);
                
                // Update thirst based on the current time and multiplier
                float damageForThirst = SetThirst(timer, timeMultiplier);
                float damageForHunger = SetHanger(timer, timeMultiplier);
                float damageForOxygen = (_statsDefinitions.healthContainer.oxygen <= 0f) ? 100f : 0f;
                float damageForDisease = SetDisease(_statsDefinitions.commonStatsContainer.diseaseContainer.pathology);
                float damageForDizziness = SetDizziness();
                float damageForEnergy = SetEnergy(timer, timeMultiplier);

                _statsDefinitions.commonStatsContainer.disease += Time.deltaTime * (damageForDisease + damageForDizziness) * 0.1f;
                float finalDisease = _statsDefinitions.commonStatsContainer.disease;

                _statsDefinitions.healthContainer.force -= Time.deltaTime * timeMultiplier * 0.0006f;

                if(_statsDefinitions.healthContainer.stamina <= 0f)
                {
                    _statsDefinitions.healthContainer.oxygen -= Time.deltaTime * 10f;
                    if(_statsDefinitions.healthContainer.oxygen < 0f) _statsDefinitions.healthContainer.oxygen = 0f;
                }
                

                // Apply damage to health based on amount of variables
                float amountOfDamage = damageForThirst + damageForHunger + damageForOxygen + finalDisease + damageForEnergy;
                _statsDefinitions.health -= Time.deltaTime * amountOfDamage * timeMultiplier * 0.1f;
                if(_statsDefinitions.health < 0f) _statsDefinitions.health = 0f;

                //recover health
                if(_statsDefinitions.health > 0.5f) _statsDefinitions.health += Time.deltaTime;

                //recover disease
                _statsDefinitions.commonStatsContainer.disease -= Time.deltaTime;
                if(_statsDefinitions.commonStatsContainer.disease < 0f) _statsDefinitions.commonStatsContainer.disease = 0f;
            }
        }

        private float SetDizziness()
        {
            _statsDefinitions.commonStatsContainer.diseaseContainer.dizziness -= Time.deltaTime;
            if(_statsDefinitions.commonStatsContainer.diseaseContainer.dizziness <= 0f) _statsDefinitions.commonStatsContainer.diseaseContainer.dizziness = 0f; 
            if (_statsDefinitions.commonStatsContainer.diseaseContainer.dizziness >= 90)
            {
                return 40;
            }
            return 0f;
        }

        private float SetDisease(DiseaseDefinition[] diseaseDefinitions)
        {
            float amountOfDamage = 0f;
            foreach(DiseaseDefinition patology in diseaseDefinitions)
            {
                amountOfDamage += patology.diseaseValue;
            }
            return amountOfDamage;
        }
        
        private float SetHanger(float timer, float timeMultiplier)
        {
            if (_statsDefinitions.healthContainer.hungerDelay.Length > 0)
            {
                _hungerIndex = GetDelayIndex(_statsDefinitions.healthContainer.hungerDelay, _hungerIndex, timer);
                _statsDefinitions.healthContainer.hunger += Time.deltaTime * _statsDefinitions.healthContainer.hungerDelay[_hungerIndex].delay * timeMultiplier * 0.001f;
                if (_statsDefinitions.healthContainer.hunger > 100f)
                {
                    _statsDefinitions.healthContainer.hunger = 100f;
                    return 40f;
                }
            }
            return 0f;
        }

        private float SetThirst(float timer, float timeMultiplier)
        {
            if (_statsDefinitions.healthContainer.thirstDelay.Length > 0)
            {
                _thirstIndex = GetDelayIndex(_statsDefinitions.healthContainer.thirstDelay, _thirstIndex, timer);
                _statsDefinitions.healthContainer.thirst += Time.deltaTime * _statsDefinitions.healthContainer.thirstDelay[_thirstIndex].delay * timeMultiplier * 0.001f;
                if (_statsDefinitions.healthContainer.thirst > 100f)
                {
                    _statsDefinitions.healthContainer.thirst = 100f;
                    return 40f;
                }
            }
            return 0f;
        }

        private float SetEnergy(float timer, float timeMultiplier)
        {
            if (_statsDefinitions.commonStatsContainer.energyDelay.Length > 0)
            {
                _energyIndex = GetDelayIndex(_statsDefinitions.commonStatsContainer.energyDelay, _energyIndex, timer);
                _statsDefinitions.commonStatsContainer.energy -= Time.deltaTime * _statsDefinitions.commonStatsContainer.energyDelay[_energyIndex].delay * timeMultiplier * 0.001f;
                if (_statsDefinitions.commonStatsContainer.energy < 0f)
                {
                    _statsDefinitions.commonStatsContainer.energy = 0f;
                    return 40f;
                }
            }
            return 0f;
        }

        public float GetMultiplier(TimeManager.TimeScale timeScale)
        {
            switch (timeScale)
            {
                case TimeManager.TimeScale.x1:
                    return 1f;
                case TimeManager.TimeScale.x2:
                    return 60f;
                case TimeManager.TimeScale.x3:
                    return 1440f;
                case TimeManager.TimeScale.x4:
                    return 44640f;
            }
            return 1f;
        }

        public float DateToTimer(int hour, int minute)
        {
            return hour * 3600f + minute * 60f;
        }
        public int GetDelayIndex(StatDelayByTimeDefinition[] delayArray, int currentValue, float timer)
        {
            int nextValue = (currentValue + 1 < delayArray.Length) ? currentValue + 1 : 0;
            if (delayArray.Length > 0 && timer < (24 * 3600f) - 60f)
            {
                float currentEndTime = DateToTimer(delayArray[currentValue].endHour, delayArray[currentValue].endMinute);

                float nextStartTime = DateToTimer(delayArray[nextValue].startHour, delayArray[nextValue].startMinute);
                float nextEndTime = DateToTimer(delayArray[nextValue].endHour, delayArray[nextValue].endMinute);
                if (timer > currentEndTime && timer >= nextStartTime && timer < nextEndTime)
                {
                    return nextValue;
                }
                else
                {
                    return currentValue;
                }
            }
            return 0;
        }
    }
}