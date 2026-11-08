using SERES.Systems.PhaseSystem;
using SERES.Systems.TimeSystem;
using SERES.Definitions;
using UnityEngine;
using System.Collections.Generic;
namespace SERES.Systems.StatsSystem
{
    public class StatsManager : PhaseManager
    {
        public TimeManager timeManager;
        public StatsDefinitionObject startStats;
        public ConsumeStatsDefinition consumeStatsDefinition;
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
                float damageForTastes = SetTastes(timeMultiplier);
                float damageFroPhisiologycalNeeds = SetPhisiologycalNeeds(timeMultiplier);
                float damageFroClealiness = SetCleanliness(timeMultiplier, damageFroPhisiologycalNeeds * 0.1f);
                float damageForHumanInteraction = SetHumanInteraction(timeMultiplier);
                float valueForLove = SetLove(timeMultiplier);
                float valueForConfidence = SetConfidence(timeMultiplier);
                float valueForSecurity = SetSecurity(timeMultiplier);
                float valueForComplicity = SetComplicity(timeMultiplier);
                float[] affectivityVariables = {valueForLove, valueForConfidence, valueForSecurity, valueForComplicity};
                float damageForAffectivity = SetAffectivity(affectivityVariables);
                float damageForStress = SetStress(timeMultiplier);
                float damageForFear = SetFear(timeMultiplier);
                float damageForFun = SetFun(timeMultiplier);


                float amountOfCareDamage = damageForTastes + damageFroClealiness;
                _statsDefinitions.commonStatsContainer.personalCare -= Time.deltaTime * timeMultiplier * amountOfCareDamage * consumeStatsDefinition.personalCare;
                float finalDamageForPersonalCare = (_statsDefinitions.commonStatsContainer.personalCare <= 1f) ? 30f : 0f;
                _statsDefinitions.commonStatsContainer.personalCare += Time.deltaTime * timeMultiplier * 0.1f;
                if (_statsDefinitions.commonStatsContainer.personalCare < 0f) _statsDefinitions.commonStatsContainer.personalCare = 0f;
                if (_statsDefinitions.commonStatsContainer.personalCare > 100f) _statsDefinitions.commonStatsContainer.personalCare = 100f;

                _statsDefinitions.commonStatsContainer.disease += Time.deltaTime * (damageForDisease + damageForDizziness) * consumeStatsDefinition.disease;
                float finalDisease = _statsDefinitions.commonStatsContainer.disease;

                _statsDefinitions.healthContainer.force -= Time.deltaTime * timeMultiplier * consumeStatsDefinition.force;

                if (_statsDefinitions.healthContainer.stamina <= 0f)
                {
                    _statsDefinitions.healthContainer.oxygen -= Time.deltaTime * consumeStatsDefinition.oxygen * 100f;
                    if (_statsDefinitions.healthContainer.oxygen < 0f) _statsDefinitions.healthContainer.oxygen = 0f;
                }


                // Apply damage to health based on amount of variables
                float amountOfDamage = damageForThirst + damageForHunger + damageForOxygen + finalDisease + damageForEnergy + (finalDamageForPersonalCare * 0.5f);
                _statsDefinitions.health -= Time.deltaTime * amountOfDamage * timeMultiplier * consumeStatsDefinition.health;
                if (_statsDefinitions.health < 0f) 
                {
                    _statsDefinitions.health = 0f;
                    ChangePhaseMode(PhaseMode.GameOver);
                }
                if (_statsDefinitions.health > 100f) _statsDefinitions.health = 100f;

                //recover health
                if (_statsDefinitions.health > 0.5f) _statsDefinitions.health += Time.deltaTime;

                //recover disease
                _statsDefinitions.commonStatsContainer.disease -= Time.deltaTime;
                if (_statsDefinitions.commonStatsContainer.disease < 0f) _statsDefinitions.commonStatsContainer.disease = 0f;

                //Aply damage to mental health on amount of variables
                float amountOfMentalDamage = finalDisease + damageForEnergy + finalDamageForPersonalCare + damageForHumanInteraction + damageForAffectivity + damageForStress + damageForFear + damageForFun;
                _statsDefinitions.mentalHealth -= Time.deltaTime * amountOfMentalDamage * timeMultiplier * consumeStatsDefinition.mentalHealth;
                if (_statsDefinitions.mentalHealth < 0f) 
                {
                    _statsDefinitions.mentalHealth = 0f;
                    ChangePhaseMode(PhaseMode.GameOver);
                }
                if (_statsDefinitions.mentalHealth > 100f) _statsDefinitions.mentalHealth = 100f;

                //recover mental health
                if (_statsDefinitions.mentalHealth > 0.5f) _statsDefinitions.mentalHealth += Time.deltaTime;
            }
        }

        private float SetDizziness()
        {
            _statsDefinitions.commonStatsContainer.diseaseContainer.dizziness -= Time.deltaTime * consumeStatsDefinition.dizziness * 10;
            if (_statsDefinitions.commonStatsContainer.diseaseContainer.dizziness <= 0f) _statsDefinitions.commonStatsContainer.diseaseContainer.dizziness = 0f;
            if (_statsDefinitions.commonStatsContainer.diseaseContainer.dizziness >= 90)
            {
                return 40;
            }
            return 0f;
        }

        private float SetDisease(DiseaseDefinition[] diseaseDefinitions)
        {
            float amountOfDamage = 0f;
            foreach (DiseaseDefinition patology in diseaseDefinitions)
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
                _statsDefinitions.healthContainer.hunger += Time.deltaTime * _statsDefinitions.healthContainer.hungerDelay[_hungerIndex].delay * timeMultiplier * consumeStatsDefinition.hunger;
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
                _statsDefinitions.healthContainer.thirst += Time.deltaTime * _statsDefinitions.healthContainer.thirstDelay[_thirstIndex].delay * timeMultiplier * consumeStatsDefinition.thirst;
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
                _statsDefinitions.commonStatsContainer.energy -= Time.deltaTime * _statsDefinitions.commonStatsContainer.energyDelay[_energyIndex].delay * timeMultiplier * consumeStatsDefinition.energy;
                if (_statsDefinitions.commonStatsContainer.energy < 0f)
                {
                    _statsDefinitions.commonStatsContainer.energy = 0f;
                    return 40f;
                }
            }
            return 0f;
        }

        private float SetTastes(float timeMultiplier)
        {
            _statsDefinitions.commonStatsContainer.personalCareContainer.tastes -= Time.deltaTime * timeMultiplier * consumeStatsDefinition.tastes;
            if (_statsDefinitions.commonStatsContainer.personalCareContainer.tastes <= 1f)
            {
                _statsDefinitions.commonStatsContainer.personalCareContainer.tastes = 0f;
                return 15f;
            }
            return 0f;
        }

        private float SetCleanliness(float timeMultiplier, float extraDecrease)
        {
            float extra = 1 + extraDecrease;
            _statsDefinitions.commonStatsContainer.personalCareContainer.cleanliness -= Time.deltaTime * timeMultiplier * extra * consumeStatsDefinition.cleanliness;
            if (_statsDefinitions.commonStatsContainer.personalCareContainer.cleanliness <= 1f)
            {
                _statsDefinitions.commonStatsContainer.personalCareContainer.cleanliness = 0f;
                return 15f;
            }
            return 0f;
        }

        private float SetPhisiologycalNeeds(float timeMultiplier)
        {
            _statsDefinitions.commonStatsContainer.personalCareContainer.physiologicalNeeds += Time.deltaTime * timeMultiplier * consumeStatsDefinition.phisiologycalNeeds;
            if (_statsDefinitions.commonStatsContainer.personalCareContainer.physiologicalNeeds >= 100f)
            {
                _statsDefinitions.commonStatsContainer.personalCareContainer.physiologicalNeeds = 100f;
                return 30f;
            }
            return 0f;
        }

        private float SetHumanInteraction(float timeMultiplier)
        {
            _statsDefinitions.mentalHealthContainer.humanInteraction -= Time.deltaTime * timeMultiplier * consumeStatsDefinition.humanInteraction;
            if (_statsDefinitions.mentalHealthContainer.humanInteraction <= 0f)
            {
                _statsDefinitions.mentalHealthContainer.humanInteraction = 0f;
                return 40f;
            }
            return 0f;
        }

        private float SetAffectivity(float[] variables)
        {
            float midValue = 0f;
            foreach(float variable in variables)
            {
                midValue += variable;
            }
            _statsDefinitions.mentalHealthContainer.affectivity = midValue / variables.Length;
            if (_statsDefinitions.mentalHealthContainer.affectivity <= 0f)
            {
                _statsDefinitions.mentalHealthContainer.affectivity = 0f;
                return 30f;
            }
            return 0f;
        }

        private float SetLove(float timeMultiplier)
        {
            return _statsDefinitions.mentalHealthContainer.affectivityContainer.love -= Time.deltaTime * timeMultiplier * consumeStatsDefinition.love;
        }

        private float SetConfidence(float timeMultiplier)
        {
            return _statsDefinitions.mentalHealthContainer.affectivityContainer.confidence -= Time.deltaTime * timeMultiplier * consumeStatsDefinition.confidence;
        }

        private float SetSecurity(float timeMultiplier)
        {
            return _statsDefinitions.mentalHealthContainer.affectivityContainer.security -= Time.deltaTime * timeMultiplier * consumeStatsDefinition.security;
        }

        private float SetComplicity(float timeMultiplier)
        {
            return _statsDefinitions.mentalHealthContainer.affectivityContainer.complicity -= Time.deltaTime * timeMultiplier * consumeStatsDefinition.complicity;
        }

        private float SetStress(float timeMultiplier)
        {
            _statsDefinitions.mentalHealthContainer.stress -= Time.deltaTime * timeMultiplier;
            if (_statsDefinitions.mentalHealthContainer.stress <= 0f)
            {
                _statsDefinitions.mentalHealthContainer.stress = 0f;
            }
            if(_statsDefinitions.mentalHealthContainer.stress >= 100f) _statsDefinitions.mentalHealthContainer.stress = 100f;
            if(_statsDefinitions.mentalHealthContainer.stress >= 90f)
            {
                return 40f;
            }
            return 0f;
        }

        private float SetFear(float timeMultiplier)
        {
            _statsDefinitions.mentalHealthContainer.fear -= Time.deltaTime * timeMultiplier;
            if (_statsDefinitions.mentalHealthContainer.fear <= 0f)
            {
                _statsDefinitions.mentalHealthContainer.fear = 0f;
            }
            if(_statsDefinitions.mentalHealthContainer.fear >= 100f) _statsDefinitions.mentalHealthContainer.fear = 100f;
            if(_statsDefinitions.mentalHealthContainer.fear >= 90f)
            {
                return 40f;
            }
            return 0f;
        }

        private float SetFun(float timeMultiplier)
        {
            _statsDefinitions.mentalHealthContainer.fun -= Time.deltaTime * timeMultiplier * 0.01f;
            if (_statsDefinitions.mentalHealthContainer.fun <= 0f)
            {
                _statsDefinitions.mentalHealthContainer.fun = 0f;
                return 40f;
            }
            return 0f;
        }

        private float GetMultiplier(TimeManager.TimeScale timeScale)
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

        private float DateToTimer(int hour, int minute)
        {
            return hour * 3600f + minute * 60f;
        }
        private int GetDelayIndex(StatDelayByTimeDefinition[] delayArray, int currentValue, float timer)
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
        public StatsDefinition GetStats()
        {
            return _statsDefinitions;
        }
    }
}