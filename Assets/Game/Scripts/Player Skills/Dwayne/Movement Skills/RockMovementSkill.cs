using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRockMovementSkill
{
    float movementStrength { get; }

    float cooldown { get; }

    int level { get; }

    int xp { get; }

    int reqXP { get; }

    string description { get; }

    string name { get; }

    int durationType { get; }

    void applyForce(Rigidbody rb);

}
