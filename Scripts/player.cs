using Godot;
using System;

public class player : KinematicBody2D
{
    [Export] public int speed = 100;
    [Signal] public delegate void _BloodSeeker();
    public Vector2 velocity = new Vector2();
    public int jumpVelocity = 3;
    public int movementModifier = 0;

    public bool isJumping = false;
    public bool isFalling = false;

    public float jumpStartY = 0;

    public void GetInput()
    {
        velocity = new Vector2();
        AnimationPlayer animation = (AnimationPlayer)GetNode("anim");
        string direction = animation.CurrentAnimation;

        if (Input.IsActionPressed("right"))
        {
            velocity.x += 1;
            animation.Play("Walkright");
        }
        if (Input.IsActionJustReleased("right"))
        {
            animation.Stop();
            animation.Play("Idleright");
        }
        if (Input.IsActionPressed("left"))
        {
            velocity.x -= 1;
            animation.Play("Walkleft");
        }
        if (Input.IsActionJustReleased("left"))
        {
            animation.Stop();
            animation.Play("Idleleft");
        }
        if (Input.IsActionPressed("down"))
        {
            velocity.y += 1;
            animation.Play("Walkdown");
        }
        if (Input.IsActionJustReleased("down"))
        {
            animation.Stop();
            animation.Play("Idledown");
        }
        if (Input.IsActionPressed("up"))
        {
            velocity.y -= 1;
            animation.Play("Walkup");
        }
        if (Input.IsActionJustReleased("up"))
        {
            animation.Stop();
            animation.Play("Idleup");
        }

        // JUMPING
        if (Input.IsActionPressed("jump"))
        {
            if (!isJumping)
            {
                jumpStartY = Position.y;
            }
            isJumping = true;
            movementModifier = jumpVelocity;

            if (direction.Contains("up"))
            {
                velocity.y -= movementModifier;
            }

            if (direction.Contains("right"))
            {
                velocity.y -= movementModifier;
                velocity.x += jumpVelocity;
            }

            if (direction.Contains("down"))
            {

                velocity.y += movementModifier;

            }
            if (direction.Contains("left"))
            {

                velocity.y -= movementModifier;
                velocity.x -= jumpVelocity;
            }
        }
        else if (Input.IsActionJustReleased("jump"))
        {
            Console.WriteLine("released jump");
            isJumping = false;
            isFalling = true;
        }
        else if (isFalling)
        {
            Console.WriteLine("isFalling jumpStartY =" + jumpStartY);
            if (Position.y < jumpStartY)
            {
                Console.WriteLine("returning to position");
                if (direction.Contains("up"))
                {
                    velocity.y -= -movementModifier;
                }

                if (direction.Contains("right"))
                {
                    velocity.y -= -movementModifier;
                    velocity.x += jumpVelocity;
                }

                if (direction.Contains("down"))
                {

                    velocity.y += -movementModifier;

                }
                if (direction.Contains("left"))
                {

                    velocity.y -= -movementModifier;
                    velocity.x -= jumpVelocity;
                }
            }
            else if (Position.y >= jumpStartY)
            {
                Console.WriteLine("Landed");
                isFalling = false;
            }
        }

        for (int i = 0; i < GetSlideCount(); i++)
        {
            var collision = GetSlideCollision(i);
            var collider = collision.Collider;
            if (collider is KinematicBody2D)
            {
                EmitSignal(nameof(_BloodSeeker), "thirsty");
            }
        }
        velocity = velocity.Normalized() * speed;
    }

    public override void _PhysicsProcess(float delta)
    {
        GetInput();
        velocity = MoveAndSlide(velocity);
    }
}
