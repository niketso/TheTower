using UnityEngine.UI;
using UnityEngine;

public class EnemyUIManager : MonoBehaviour, iPoolable 
{

    [SerializeField] private Slider enemyHpSlider;
    [SerializeField] private EnemyHealth enemyHp;

    private void Start()
    {
        enemyHp.OnHealthChanged -= UpdateSlider;
        enemyHp.OnHealthChanged += UpdateSlider;
    }

    public void OnPool()
    {
    }

    public void OnUnpool()
    {
        enemyHpSlider.maxValue = enemyHp.baseHealth;
        enemyHpSlider.value = enemyHp.baseHealth;
        enemyHp.OnHealthChanged -= UpdateSlider;
        enemyHp.OnHealthChanged += UpdateSlider;
    }

    public void UpdateSlider(float health) 
    {
        if (enemyHpSlider.maxValue < health)
            enemyHpSlider.maxValue = health;

        enemyHpSlider.value = health;
    }
}
