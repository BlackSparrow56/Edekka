using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;

namespace Game.Player
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private List<Arm> arms;
        [SerializeField] private float armsCooldownBase;

        [SerializeField] private float punchBonus;
        [SerializeField] private float minuteBonus;

        [SerializeField] private AnimationCurve armsCooldownCurve;
        [SerializeField] private AnimationCurve experienceGrowCurve;

        [SerializeField] private Hint hint;

        [SerializeField] private float maxExperience;
        [SerializeField] private int maxLevel;

        private float experience = 0f;
        private int level = 0;

        private void GiveExperience(float bonus)
        {
            experience += bonus;

            float maxExperience = this.maxExperience * experienceGrowCurve.Evaluate((float)level / maxLevel);
            if (experience > maxExperience)
            {
                experience -= maxExperience;
                LevelUp();
            }
        }

        private void GiveExperienceForPunch()
        {
            GiveExperience(punchBonus);
        }

        private void LevelUp()
        {
            hint.ShowText($"Уровень повышен! Уровень: {level}", 1f);

            level++;
            arms.ForEach(value => value.SetReturnSpeed(armsCooldownBase * armsCooldownCurve.Evaluate(level / maxLevel)));
        }

        private void Start()
        {
            StartCoroutine(BonusCoroutine());
            LevelUp();
        }

        private void OnEnable()
        {
            foreach (var arm in arms)
            {
                arm.onRockHitted += GiveExperienceForPunch;
            }
        }

        private void OnDisable()
        {
            foreach (var arm in arms)
            {
                arm.onRockHitted -= GiveExperienceForPunch;
            }
        }

        private IEnumerator BonusCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(60f);

                experience += minuteBonus;
                hint.ShowText($"Выдан бонус к опыту за нахождение в игре", 2.5f);
            }
        }
    }
}