using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSelect : NetworkBehaviour
{
    public int currentCharacterIndex = 0;
    public List<GameObject> characterInstances = new List<GameObject>();
}
