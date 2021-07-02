using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    public PlayerData playerData;
    public enum CooldownType
    {
        Dash
    }
    public Dictionary<CooldownType, SingleCooldown> cooldowns = new Dictionary<CooldownType, SingleCooldown>();

    void Update()
    {
        if (cooldowns.Count > 0) CooldownExecute();
    }

    public void SetOnCooldown(CooldownType type, float maxTime)
    {
        if (!cooldowns.ContainsKey(type))
        {
            SingleCooldown cooldown = new SingleCooldown();
            cooldown.cooldownTime = maxTime;
            cooldown.timer = maxTime;
            cooldowns.Add(type, cooldown);
        }
    }

    private void CooldownExecute()
    {
        foreach (var cooldown in cooldowns)
        {
            if (cooldown.Value.cooldownTime > 0) cooldown.Value.cooldownTime -= Time.deltaTime;
            else
            {
                if (cooldowns.Count - 1 != 0) cooldowns.Remove(cooldown.Key);
                else cooldowns = new Dictionary<CooldownType, SingleCooldown>();
            }
        }
    }
}
public class SingleCooldown
{
    public float cooldownTime;
    public float timer;
}
