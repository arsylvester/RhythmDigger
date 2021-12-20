using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectTangibility {Normal, Armor, Guard, Invincible};
public abstract class InteractableObject : MonoBehaviour
{
    [Header("   Health")]
    public int HP;
    public int maxHP;
    public ObjectTangibility myTang;

    [Header("   Time")]
    [Space(10)]
    public float scaledTime;
    public float activeTime;
    public int currentTime;
    [HideInInspector]
    public int hitID; //assigned from outer forces

    [Header("   Velocity")]
    [Space(10)]
    public float friction;
    public float airFriction;
    public Vector2 internalVelocity; //internal velocity
    public Vector2 groundVelocity; //velocity supplied from the ground
    public Vector2 externalVelocity; //assigned from outer forces
    [HideInInspector]
    public Vector2 targetVelocity; //target velocity each frame
    [HideInInspector]
    public float storeGroundVelocityTime; //assigned from outer forces

    [Header("   Gravity")]
    [Space(10)]
    public float terminalYVelocity;
    public float gravity;
    public float gravityMod;
    public bool isGrounded;

    [Header("   Collision")]
    [Space(10)]
    public LayerMask groundLayers;
    public LayerMask crushLayers;
    public LayerMask bounceLayers;
    public float collisionVelocity;
    public float bounceVelocity;
    public bool canBounce;
    public float bounceFriction;
    public int bounceTime;
    public BoxCollider2D groundCollider;
    public BoxCollider2D headCollider;
    public BoxCollider2D leftCollider;
    public BoxCollider2D rightCollider;

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (Crushed())
        {
            gameObject.SetActive(false);
        }
        if (externalVelocity.sqrMagnitude > bounceVelocity && canBounce)
        {
            Bounce(collision);
        }
    }

    public virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (Crushed())
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void OnPause(bool paused)
    {
        internalVelocity *= 0;
    }

    public abstract ObjectTangibility TakeDamage(int damage, float distance, Vector2 direction, float hitstopTime, bool armorPierce, bool fromPlayer);

    public void SetTimeScale(float speed)
    {
        scaledTime = speed;
        activeTime = Mathf.RoundToInt(activeTime);
    }
    public virtual void ApplyFriction()
    {
        isGrounded = Grounded(groundLayers);

        if (!isGrounded)
        {
            internalVelocity.y += -gravity * gravityMod * scaledTime;
        }

        if (isGrounded && internalVelocity.y <= 0)
        {
            internalVelocity.y = -gravity * scaledTime;
        }

        internalVelocity.x *= friction;

        if (isGrounded)
        {
            externalVelocity.x *= friction;
            externalVelocity.y *= friction * 0.9f;
            if (storeGroundVelocityTime == 0)
            {
                groundVelocity.x *= friction;
                groundVelocity.y *= friction * 0.9f;
            }
        }
        else
        {
            externalVelocity.x *= airFriction;
            externalVelocity.y *= airFriction * 0.9f;
            if (storeGroundVelocityTime == 0)
            {
                groundVelocity.x *= airFriction;
                groundVelocity.y *= airFriction * 0.9f;
            }
        }

        if (internalVelocity.y < terminalYVelocity)
        {
            internalVelocity.y = terminalYVelocity;
        }

        if (Mathf.Round(externalVelocity.sqrMagnitude * 100f) / 100f == 0)
        {
            externalVelocity *= 0;
        }
        if (Mathf.Round(groundVelocity.sqrMagnitude * 100f) / 100f == 0)
        {
            groundVelocity *= 0;
        }
        if (storeGroundVelocityTime > 0)
        {
            storeGroundVelocityTime -= 1;
        }

        if (bounceTime > 0)
        {
            bounceTime--;
        }
    }

    public bool Stomped() //Check if we stmped on an interactable object
    {
        float length = 0.2f;
        float width = 0.2f;
        if (GetComponent<CircleCollider2D>())
        {
            width = GetComponent<CircleCollider2D>().radius;
            length = GetComponent<CircleCollider2D>().radius;
        }
        else if (GetComponent<BoxCollider2D>())
        {
            width = (GetComponent<BoxCollider2D>().size.x * 0.5f);
            length = (GetComponent<BoxCollider2D>().size.y * 0.5f);
        }
        else if (GetComponent<CapsuleCollider2D>())
        {
            width = (GetComponent<CapsuleCollider2D>().size.x * 0.5f);
            length = (GetComponent<CapsuleCollider2D>().size.y * 0.5f);
        }
        RaycastHit2D midHit = Physics2D.Raycast(transform.position + new Vector3(0, length), Vector2.up, length * 0.5f, crushLayers);
        RaycastHit2D midLeftHit = Physics2D.Raycast(transform.position + new Vector3(-width * 0.4f, length), Vector2.up, length * 0.5f, crushLayers);
        RaycastHit2D midRightHit = Physics2D.Raycast(transform.position + new Vector3(width * 0.4f, length), Vector2.up, length * 0.5f, crushLayers);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position + new Vector3(width * 0.85f, length), Vector2.up, length * 0.5f, crushLayers);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position + new Vector3(-width * 0.85f, length), Vector2.up, length * 0.5f, crushLayers);
        if (midHit)
        {
            if (midHit.transform.gameObject != this.gameObject)
            {
                if (midHit.transform.gameObject.GetComponent<InteractableObject>())
                {
                    midHit.transform.gameObject.GetComponent<InteractableObject>().internalVelocity.y = 0;
                    midHit.transform.gameObject.GetComponent<InteractableObject>().externalVelocity.y = 15;
                    if (midHit.transform.gameObject.GetComponent<PlayerController>())
                    {
                        //You can put a check here if you want
                        {
                            midHit.transform.gameObject.GetComponent<PlayerController>().airJumpCount = midHit.transform.gameObject.GetComponent<PlayerController>().maxAirJumps;
                            midHit.transform.gameObject.GetComponent<PlayerController>().airDodgeCount = midHit.transform.gameObject.GetComponent<PlayerController>().maxAirDodges;
                            midHit.transform.gameObject.GetComponent<PlayerController>().coyoteTime = 6;
                            midHit.transform.gameObject.GetComponent<PlayerController>().playerMachine.ChangeState(PlayerStateEnums.Jump);
                        }
                    }
                }
                return true;
            }
            else
                return false;
        }
        if (midLeftHit)
        {
            if (midLeftHit.transform.gameObject != this.gameObject)
            {
                if (midLeftHit.transform.gameObject.GetComponent<InteractableObject>())
                {
                    midLeftHit.transform.gameObject.GetComponent<InteractableObject>().internalVelocity.y = 0;
                    midLeftHit.transform.gameObject.GetComponent<InteractableObject>().externalVelocity.y = 15;
                    if (midLeftHit.transform.gameObject.GetComponent<PlayerController>())
                    {
                        //You can put a check here if you want
                        {
                            midLeftHit.transform.gameObject.GetComponent<PlayerController>().airJumpCount = midLeftHit.transform.gameObject.GetComponent<PlayerController>().maxAirJumps;
                            midLeftHit.transform.gameObject.GetComponent<PlayerController>().airDodgeCount = midLeftHit.transform.gameObject.GetComponent<PlayerController>().maxAirDodges;
                            midLeftHit.transform.gameObject.GetComponent<PlayerController>().coyoteTime = 6;
                            midLeftHit.transform.gameObject.GetComponent<PlayerController>().playerMachine.ChangeState(PlayerStateEnums.Jump);
                        }
                    }
                }
                return true;
            }
            else
                return false;
        }
        if (midRightHit)
        {
            if (midRightHit.transform.gameObject != this.gameObject)
            {
                if (midRightHit.transform.gameObject.GetComponent<InteractableObject>())
                {
                    midRightHit.transform.gameObject.GetComponent<InteractableObject>().internalVelocity.y = 0;
                    midRightHit.transform.gameObject.GetComponent<InteractableObject>().externalVelocity.y = 15;
                    if (midRightHit.transform.gameObject.GetComponent<PlayerController>())
                    {
                        //You can put a check here if you want
                        {
                            midRightHit.transform.gameObject.GetComponent<PlayerController>().airJumpCount = midRightHit.transform.gameObject.GetComponent<PlayerController>().maxAirJumps;
                            midRightHit.transform.gameObject.GetComponent<PlayerController>().airDodgeCount = midRightHit.transform.gameObject.GetComponent<PlayerController>().maxAirDodges;
                            midRightHit.transform.gameObject.GetComponent<PlayerController>().coyoteTime = 6;
                            midRightHit.transform.gameObject.GetComponent<PlayerController>().playerMachine.ChangeState(PlayerStateEnums.Jump);
                        }
                    }
                }
                return true;
            }
            else
                return false;
        }
        if (rightHit)
        {
            if (rightHit.transform.gameObject != this.gameObject)
            {
                if (rightHit.transform.gameObject.GetComponent<InteractableObject>())
                {
                    rightHit.transform.gameObject.GetComponent<InteractableObject>().internalVelocity.y = 0;
                    rightHit.transform.gameObject.GetComponent<InteractableObject>().externalVelocity.y = 15;
                    if (rightHit.transform.gameObject.GetComponent<PlayerController>())
                    {
                        //You can put a check here if you want
                        {
                            rightHit.transform.gameObject.GetComponent<PlayerController>().airJumpCount = rightHit.transform.gameObject.GetComponent<PlayerController>().maxAirJumps;
                            rightHit.transform.gameObject.GetComponent<PlayerController>().airDodgeCount = rightHit.transform.gameObject.GetComponent<PlayerController>().maxAirDodges;
                            rightHit.transform.gameObject.GetComponent<PlayerController>().coyoteTime = 6;
                            rightHit.transform.gameObject.GetComponent<PlayerController>().playerMachine.ChangeState(PlayerStateEnums.Jump);
                        }
                    }
                }
                return true;
            }
            else
                return false;
        }
        if (leftHit)
        {
            if (leftHit.transform.gameObject != this.gameObject)
            {
                if (leftHit.transform.gameObject.GetComponent<InteractableObject>())
                {
                    leftHit.transform.gameObject.GetComponent<InteractableObject>().internalVelocity.y = 0;
                    leftHit.transform.gameObject.GetComponent<InteractableObject>().externalVelocity.y = 15;
                    if (leftHit.transform.gameObject.GetComponent<PlayerController>())
                    {
                        //You can put a check here if you want
                        {
                            leftHit.transform.gameObject.GetComponent<PlayerController>().airJumpCount = leftHit.transform.gameObject.GetComponent<PlayerController>().maxAirJumps;
                            leftHit.transform.gameObject.GetComponent<PlayerController>().airDodgeCount = leftHit.transform.gameObject.GetComponent<PlayerController>().maxAirDodges;
                            leftHit.transform.gameObject.GetComponent<PlayerController>().coyoteTime = 6;
                            leftHit.transform.gameObject.GetComponent<PlayerController>().playerMachine.ChangeState(PlayerStateEnums.Jump);
                        }
                    }
                }
                return true;
            }
        }
        return false;
    }

    public bool Bonked() //Check if we hit anything solid above our head 
    {
        Collider2D[] collisions = Physics2D.OverlapBoxAll(headCollider.transform.position, headCollider.size, 0, groundLayers);
        foreach (Collider2D hitObject in collisions)//anything hit
        {
            if (hitObject.gameObject != this && hitObject.gameObject != null && hitObject.gameObject != groundCollider.gameObject && !hitObject.GetComponent<GoldReward>())
            {
                return true;
            }
        }
        return false;
    }

    public bool Crushed()//Check if we were crushed 
    {
        bool verticalCrush = false;
        bool horizontalCrush = false;

        if (Bonked() && Grounded(crushLayers))
            verticalCrush = true;

        if (verticalCrush || horizontalCrush)
            return true;

        return false;
    } 

    public bool LeftCollision(LayerMask mask)
	{
        Collider2D[] collisions = Physics2D.OverlapBoxAll(leftCollider.transform.position, leftCollider.size, 0, groundLayers);
        foreach (Collider2D hitObject in collisions)//anything hit
        {
            if (hitObject.gameObject != this && hitObject.gameObject != null && hitObject.gameObject != groundCollider.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public bool RightCollision()
	{
        Collider2D[] collisions = Physics2D.OverlapBoxAll(rightCollider.transform.position, rightCollider.size, 0, groundLayers);
        foreach (Collider2D hitObject in collisions)//anything hit
        {
            if (hitObject.gameObject != this && hitObject.gameObject != null && hitObject.gameObject != groundCollider.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public bool Grounded(LayerMask mask) //Check if we touch the ground we want to touch
    {

        Collider2D[] collisions = Physics2D.OverlapBoxAll(groundCollider.transform.position, groundCollider.size, 0, groundLayers);
        foreach (Collider2D hitObject in collisions)//anything hit
		{
            if (hitObject.gameObject != this && hitObject.gameObject != null && hitObject.gameObject != groundCollider.gameObject)
            {
                return true;
            }
		}
        return false;
    }

    public void Bounce(Collision2D collision) //Check if we can bounce
    {
        if (bounceTime <= 0)
        {
            //Debug.Log("try Bounce");
            if (collision.gameObject.GetComponent<InteractableObject>())
            {
                externalVelocity *= bounceFriction;
                collision.gameObject.GetComponent<InteractableObject>().externalVelocity += externalVelocity;
                if (targetVelocity.sqrMagnitude < collisionVelocity)
                    externalVelocity = Vector2.Reflect(externalVelocity, collision.contacts[0].normal);
                else if (Vector3.Dot(targetVelocity, collision.transform.position - transform.position) > 0.65f)
                    collision.gameObject.GetComponent<InteractableObject>().TakeDamage(1, targetVelocity.sqrMagnitude, new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.x).normalized, 0.04f, true, false);
            }
            else
            {
                externalVelocity = Vector2.Reflect(externalVelocity, collision.contacts[0].normal);
            }
            bounceTime = 3;
        }
    }
}