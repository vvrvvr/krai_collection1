using TMPro;
using UnityEngine;
using DG.Tweening;
using krai_shooter;

namespace krai_shooter
{
    public class MoneyScript : MonoBehaviour
    {

        private float currentMoney = 0f;
        private ulong moneyTodisplay;
        private float currentAddition = 0f;
        private int counter = 0;
        private bool isMoneyPowerup;

        [SerializeField] private TextMeshProUGUI money;
        public void UpdateMoney()
        {
            counter++;
            bool isPlus;
            if (isMoneyPowerup)
                isPlus = true;
            else
                isPlus = EncreaseMoneyprobability();

            CalculateCurrentAddition(isPlus);

            if (!isPlus & counter > 6)
            {
                currentAddition *= -1;
            }

            moneyTodisplay = (ulong)(currentMoney += currentAddition);
            DOTween.To(UpdateText, currentMoney, currentMoney + currentAddition, 0.5f).OnComplete(() => money.text = $"{moneyTodisplay}");
            currentMoney += currentAddition;
            ScaleEffect();

            //Debug.Log(counter);
        }

        public void MoneyPowerup()
        {
            isMoneyPowerup = true;
            UpdateMoney();
            isMoneyPowerup = false;
        }

        private void UpdateText(float val)
        {
            var integ = (ulong)val;
            if (currentAddition > 0)
                money.text = $"+{integ}";
            else
                money.text = $"-{integ}";
        }
        private void ScaleEffect()
        {
            Sequence scale = DOTween.Sequence();
            scale.Append(money.transform.DOScale(new Vector3(1f, 1.2f, 1f), 0.5f));
            scale.Append(money.transform.DOScale(Vector3.one, 0.1f));
        }

        private bool EncreaseMoneyprobability() //шанс на убавление 30%
        {
            var probability = new[] { 1, 0, 1, 1, 1, 1, 1, 1, 0, 1 };
            var value = probability[Random.Range(0, probability.Length)];
            //Debug.Log(value);
            if (value == 0)
                return false;
            return true;
        }
        private void CalculateCurrentAddition(bool plus)
        {
            var probability = new[] { 1, 1, 2, 2, 3, 3, 4, 4, 4, 5 }; // шанс да хуй знает
            var value = probability[Random.Range(0, probability.Length)];
            if (currentAddition < 0)
            {
                currentAddition *= -1;

            }
            if (!plus)
                value = 1;
            if (!isMoneyPowerup)
            {
                switch (value)
                {
                    case 1:
                        currentAddition += 100f;
                        break;
                    case 2:
                        currentAddition += 1000f;
                        break;
                    case 3:
                        currentAddition += 1200f;
                        break;
                    case 4:
                        currentAddition += 1300f;
                        break;
                    case 5:
                        currentAddition += 1400f;
                        break;
                }
            }
            else
            {
                currentAddition += 1800f;
            }

            if (counter > 10)
            {
                currentAddition *= 1.1f;
            }
            else if (counter > 30)
            {
                currentAddition *= 1.2f;
            }
            else if (counter > 50)
            {
                currentAddition *= 1.3f;
            }
            else if (counter > 70)
            {
                currentAddition *= 1.4f;
            }
            else if (counter > 90)
            {
                currentAddition *= 1.45f;
            }
            else if (counter > 100)
            {
                currentAddition *= 1.5f;
            }
            else if (counter > 150)
            {
                currentAddition *= 1.55f;
            }
        }
    }
}
