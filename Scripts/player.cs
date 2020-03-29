using Godot;
using System;

public class player : KinematicBody2D
{
    [Export] public int speed = 100;

    public Vector2 velocity = new Vector2();

    public void GetInput()
    {
        velocity = new Vector2();
        string direction = string.Empty;
        AnimationPlayer animation = (AnimationPlayer)GetNode("anim");
        if (Input.IsActionPressed("right"))
        {
            velocity.x += 1;
            animation.Play("Walkright");
            direction = "right";
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
            direction = "left";
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
            direction = "down";
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
            direction = "up";
        }
        if (Input.IsActionJustReleased("up"))
        {
            animation.Stop();
            animation.Play("Idleup");
        }

        velocity = velocity.Normalized() * speed;
    }

    public override void _PhysicsProcess(float delta)
    {
        GetInput();
        velocity = MoveAndSlide(velocity);
    }
}
