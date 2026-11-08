using SERES.Definitions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SERES.UI
{
    public class UIStatsItem: MonoBehaviour
    {
        public ExternalDataCore dataCore;
        public StatsMode statsMode;
        public TMP_Text title;
        public Slider slider;
        
        public enum StatsMode {health, mentalHealth, Thirst, Hunger, Oxygen, Force, Stamina, HumanInteraction, Affectivity, Stress, Fear, Fun, Love, Confidence, Security, Complicity, Disease, Energy, PersonalCare, Dizziness, Tastes, Cleanliness, PhisiologycalNeeds}
        private void Update()
        {
            if(dataCore.GetStatsManager())
            {
                StatsDefinition stats = dataCore.GetStatsManager().GetStats();
                switch(statsMode)
                {
                    case StatsMode.health:
                        title.text = "Health";
                        slider.value = stats.health;
                        break;
                    case StatsMode.mentalHealth:
                        title.text = "Mental health";
                        slider.value = stats.mentalHealth;
                        break;
                    case StatsMode.Thirst:
                        title.text = "Thirst";
                        slider.value = stats.healthContainer.thirst;
                        break;
                    case StatsMode.Hunger:
                        title.text = "Hunger";
                        slider.value = stats.healthContainer.hunger;
                        break;
                    case StatsMode.Oxygen:
                        title.text = "Oxygen";
                        slider.value = stats.healthContainer.oxygen;
                        break;
                    case StatsMode.Force:
                        title.text = "Force";
                        slider.value = stats.healthContainer.force;
                        break;
                    case StatsMode.Stamina:
                        title.text = "Stamina";
                        slider.value = stats.healthContainer.stamina;
                        break;
                    case StatsMode.HumanInteraction:
                        title.text = "H.Interaction";
                        slider.value = stats.mentalHealthContainer.humanInteraction;
                        break;
                    case StatsMode.Affectivity:
                        title.text = "Affectivity";
                        slider.value = stats.mentalHealthContainer.affectivity;
                        break;
                    case StatsMode.Stress:
                        title.text = "Stress";
                        slider.value = stats.mentalHealthContainer.stress;
                        break;
                    case StatsMode.Fear:
                        title.text = "Fear";
                        slider.value = stats.mentalHealthContainer.fear;
                        break;
                    case StatsMode.Fun:
                        title.text = "Fun";
                        slider.value = stats.mentalHealthContainer.fun;
                        break;
                    case StatsMode.Love:
                        title.text = "Love";
                        slider.value = stats.mentalHealthContainer.affectivityContainer.love;
                        break;
                    case StatsMode.Confidence:
                        title.text = "Confidence";
                        slider.value = stats.mentalHealthContainer.affectivityContainer.confidence;
                        break;
                    case StatsMode.Security:
                        title.text = "Security";
                        slider.value = stats.mentalHealthContainer.affectivityContainer.security;
                        break;
                    case StatsMode.Complicity:
                        title.text = "Complicity";
                        slider.value = stats.mentalHealthContainer.affectivityContainer.complicity;
                        break;
                    case StatsMode.Disease:
                        title.text = "Disease";
                        slider.value = stats.commonStatsContainer.disease;
                        break;
                    case StatsMode.Energy:
                        title.text = "Energy";
                        slider.value = stats.commonStatsContainer.energy;
                        break;
                    case StatsMode.PersonalCare:
                        title.text = "Personal care";
                        slider.value = stats.commonStatsContainer.personalCare;
                        break;
                    case StatsMode.Dizziness:
                        title.text = "Dizziness";
                        slider.value = stats.commonStatsContainer.diseaseContainer.dizziness;
                        break;
                    case StatsMode.Tastes:
                        title.text = "Tastes";
                        slider.value = stats.commonStatsContainer.personalCareContainer.tastes;
                        break;
                    case StatsMode.Cleanliness:
                        title.text = "Cleanliness";
                        slider.value = stats.commonStatsContainer.personalCareContainer.cleanliness;
                        break;
                    case StatsMode.PhisiologycalNeeds:
                        title.text = "P.Needs";
                        slider.value = stats.commonStatsContainer.personalCareContainer.physiologicalNeeds;
                        break;
                }
            }
        }
    }
}