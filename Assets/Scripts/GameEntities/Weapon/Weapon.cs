using Unity.Netcode;
using UnityEngine;

public class Weapon : NetworkBehaviour
{
    public Character Owner { get; set; }
}
