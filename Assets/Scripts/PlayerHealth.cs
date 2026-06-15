using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public float maxHP = 200f;
    public float currentHP;

    void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float amount)
    {
        if (GameManager.Instance.IsGameOver()) return;

        currentHP -= amount;
        currentHP = Mathf.Max(currentHP, 0f);
        Debug.Log($"{gameObject.name}: {currentHP} HP restantes.");

        if (currentHP <= 0)
            GameManager.Instance.PlayerEliminated(gameObject);
    }
}
