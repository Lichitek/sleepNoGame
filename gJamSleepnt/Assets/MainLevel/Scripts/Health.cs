using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private List<Image> _hpImages;
    private static List<bool> _hpState = new List<bool>();

    private UnityAction _Death;
    void Start()
    {
        _Death += FindObjectOfType<Score>().MakeDead;
        for (int i = 0; i < _hpImages.Count; i++)
        {
            _hpState.Add(true);
            _hpImages[i].sprite = _fullHeart;
        }
    }

    public void TakeDamage()
    {
        if (_hpState[1] != false)
        {
            _hpImages[_hpState.LastIndexOf(true)].enabled = false; 
            _hpState[_hpState.LastIndexOf(true)] = false;
        }
        else
            Death();
    }

    public void TakeHeal()
    {
        if (_hpState[2] != true)
        {
            _hpImages[_hpState.IndexOf(false)].enabled = true;
            _hpState[_hpState.IndexOf(false)] = true;
        }
    }

    public void Death()
    {
        for (int i = 0; i < _hpImages.Count; i++)
        {
            _hpImages[i].enabled = false;
            _hpState[i] = false;
        }
        foreach(Road road in FindObjectsOfType(typeof(Road)))
        {
            road.StopRoad();
        }
        
        Player player = (Player)FindObjectOfType(typeof(Player));
        if (player != null)
            player.StopMoving();
        else
        {
            PlayerFalling playerFalling = (PlayerFalling)FindObjectOfType(typeof(PlayerFalling));
            playerFalling.StopMoving();
        }
        Time.timeScale = 0f;
    }
}
