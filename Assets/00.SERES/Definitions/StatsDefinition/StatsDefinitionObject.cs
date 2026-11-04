using UnityEngine;
namespace SERES.Definitions
{
    [CreateAssetMenu(fileName = "StatsDefinitionObject", menuName = "SERES/Definitions/StatsDefinitionObject", order = 1)]
    public class StatsDefinitionObject : ScriptableObject
    {
        public StatsDefinition statsDefinition;
    }
}