using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : SingleTon<CharacterManager>
{
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }
    private Player _player;


}
