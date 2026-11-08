using UnityEngine;

namespace SERES.Definitions
{
    [CreateAssetMenu(fileName = "ConsumeStatsDefinition", menuName = "SERES/Definitions/ConsumeStatsDefinition", order = 2)]
    public class ConsumeStatsDefinition: ScriptableObject
    {
        [Range(0,1)]
        public float health;
        [Range(0,1)]
        public float mentalHealth;
        [Range(0,0.01f)]
        public float thirst;
        [Range(0,0.01f)]
        public float hunger;
        [Range(0,1)]
        public float oxygen;
        [Range(0,0.01f)]
        public float force;
        [Range(0,0.1f)]
        public float humanInteraction;
        [Range(0,0.1f)]
        public float fun;
        [Range(0,0.01f)]
        public float love;
        [Range(0,0.01f)]
        public float confidence;
        [Range(0,0.01f)]
        public float security;
        [Range(0,0.01f)]
        public float complicity;
        [Range(0,1f)]
        public float disease;
        [Range(0,0.01f)]
        public float energy;
        [Range(0,1)]
        public float personalCare;
        [Range(0,1)]
        public float dizziness;
        [Range(0,0.1f)]
        public float tastes;
        [Range(0,0.1f)]
        public float cleanliness;
        [Range(0,0.1f)]
        public float phisiologycalNeeds;
    }
}