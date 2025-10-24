using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStat", menuName = "Status Data/Character")]
public class CharacterStats : ScriptableObject
{
    [SerializeField]
    Stat _hp;
    public Stat HP => _hp;
    [SerializeField]
    Stat _speed;
    public Stat Speed => _speed;

    public void OnInit(CharacterStats stats)
    {
        _speed = new Stat(stats.Speed.Value);
        _hp = new Stat(stats.HP.Value);
    }

    public void Reset()
    {
        _hp.Reset();
        _speed.Reset();
    }
}