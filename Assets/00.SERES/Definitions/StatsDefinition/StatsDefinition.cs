using System;
using UnityEngine;
namespace SERES.Definitions
{
    [Serializable]
    public class StatsDefinition
    {
        [Range(0, 100)]
        public float health;
        [Range(0, 100)]
        public float mentalHealth;
        public HealthDefinition healthContainer;
        public MentalHealthDefinition mentalHealthContainer;
        public CommonStatsDefinition commonStatsContainer;
    }

    [Serializable]
    public class HealthDefinition
    {
        [Range(0, 100)]
        public float thirst;
        public StatDelayByTimeDefinition[] thirstDelay;
        [Range(0, 100)]
        public float hunger;
        public StatDelayByTimeDefinition[] hungerDelay;
        [Range(0, 100)]
        public float oxygen;
        [Range(0, 100)]
        public float force;
        [Range(0, 100)]
        public float stamina;
    }

    [Serializable]
    public class MentalHealthDefinition
    {
        [Range(0, 100)]
        public float humanInteraction;
        [Range(0, 100)]
        public float affectivity;
        [Range(0, 100)]
        public float stress;
        [Range(0, 100)]
        public float fear;
        [Range(0, 100)]
        public float fun;
        public AffectivityDefinition affectivityContainer;
    }

    [Serializable]
    public class AffectivityDefinition
    {
        [Range(0, 100)]
        public float love;
        [Range(0, 100)]
        public float confidence;
        [Range(0, 100)]
        public float security;
        [Range(0, 100)]
        public float complicity;
    }

    [Serializable]
    public class CommonStatsDefinition
    {
        [Range(0, 100)]
        public float disease;
        [Range(0, 100)]
        public float energy;
        public StatDelayByTimeDefinition[] energyDelay;
        [Range(0, 100)]
        public float personalCare;

        public DiseaseDefinitionContainer diseaseContainer;
        public PersonalCareDefinition personalCareContainer;
    }

    [Serializable]
    public class DiseaseDefinitionContainer
    {
        public DiseaseDefinition[] pathology;
        [Range(0, 100)]
        public float dizziness;
    }

    [Serializable]
    public class PersonalCareDefinition
    {
        [Range(0, 100)]
        public float tastes;
        [Range(0, 100)]
        public float cleanliness;
        [Range(0, 100)]
        public float physiologicalNeeds;
    }

    [Serializable]
    public class StatDelayByTimeDefinition
    {
        [Header("Value of delay")]
        [Range(0, 100)]
        public float delay;
        [Header("Active time range")]
        [Range(0,23)]
        public int startHour;
        [Range(0,59)]
        public int startMinute;
        [Range(0,23)]
        public int endHour;
        [Range(0,59)]
        public int endMinute;
    }
}