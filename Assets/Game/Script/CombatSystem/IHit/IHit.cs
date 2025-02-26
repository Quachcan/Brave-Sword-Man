using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHit
{
    void TakeDamage(float damageAmount); 
    Transform GetTransform();
}
