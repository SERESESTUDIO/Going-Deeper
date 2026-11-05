using UnityEngine;

namespace SERES.Definitions
{
    [CreateAssetMenu(fileName = "DiseaseDefinition", menuName = "SERES/Definitions/DiseaseDefinition", order = 2)]
    public class DiseaseDefinition : ScriptableObject
    {
        public string DiseaseName;
        [Range(0, 100)]
        public float diseaseValue;
    }
}