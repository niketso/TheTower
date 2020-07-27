using UnityEngine.UI;
using UnityEngine;

public class EnemyUIManager : MonoBehaviour, iPoolable 
{

    [SerializeField] private Slider enemyHpSlider;
    [SerializeField] private EnemyHealth enemyHp;

    public void OnPool()
    {
        enemyHp.OnHealthChanged -= UpdateSlider;
    }

    public void OnUnpool()
    {
        enemyHpSlider.maxValue = enemyHpSlider.value = enemyHp.baseHealth;
        enemyHp.OnHealthChanged += UpdateSlider;
    }

    public void UpdateSlider(float health) 
    {
        if (enemyHpSlider.maxValue < health)
            enemyHpSlider.maxValue = health;

        enemyHpSlider.value = health;
    }
}
