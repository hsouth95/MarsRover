using System;
using System.Collections;
using System.Collections.Generic;

namespace MarsRover
{
    public class Rover : IRover
    {
        Direction direction;
        Queue<Action> actions;

        public Queue<Action> Actions
        {
            get
            {
                return this.actions;
            }
            set
            {
                this.actions = value;
            }
        }

        public Direction Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                this.direction = value;
            }
        }

        public Rover(Point coordinates)
            : base(coordinates)
        {
        }

        public Rover(Point coordinates, Direction direction)
            : base(coordinates)
        {
            this.direction = direction;
        }

        public Rover(Point coordinates, Direction direction, Queue<Action> actions)
            : base(coordinates)
        {
            this.direction = direction;
            this.actions = actions;
        }

        public Action GetNextMove()
        {
            if (this.actions != null && this.actions.Count > 0)
            {
                return this.actions.Peek();
            }

            return Action.Nothing;
        }

        public override void Move()
        {
            if (this.actions == null || this.actions.Count == 0)
            {
                throw new InvalidOperationException("Rover has no more movements to make");
            }

            Action action = this.actions.Dequeue();
            if(action == Action.MoveForward)
            {
                this.MoveForward();
            } 
            else if (action == Action.RotateLeft || action == Action.RotateRight)
            {
                this.Rotate(action);
            }
            else
            {
                throw new InvalidOperationException("Rover has invalid movement");
            }
        }

        public void MoveForward()
        {
            switch (this.direction)
            {
                case Direction.North:
                    this.Coordinates.Y++;
                    break;
                case Direction.South:
                    if (this.Coordinates.Y == 0)
                    {
                        throw new InvalidOperationException("Rover was instructed to go SOUTH off the grid");
                    }

                    this.Coordinates.Y--;
                    break;
                case Direction.East:
                    this.Coordinates.X++;
                    break;
                case Direction.West:
                    if (this.Coordinates.X == 0)
                    {
                        throw new InvalidOperationException("Rover was instructed to go WEST off the grid");
                    }

                    this.Coordinates.X--;
                    break;
                default:
                    throw new InvalidOperationException("Invalid direction specified for Rover");
            }
        }

        public void Rotate(Action action)
        {
            if (action != Action.RotateLeft && action != Action.RotateRight)
            {
                throw new ArgumentOutOfRangeException(nameof(action));
            }

            switch (this.direction)
            {
                case Direction.North:
                    if (action == Action.RotateLeft)
                    {
                        this.direction = Direction.West;
                    }
                    else
                    {
                        this.direction = Direction.East;
                    }

                    break;
                case Direction.East:
                    if (action == Action.RotateLeft)
                    {
                        this.direction = Direction.North;
                    }
                    else
                    {
                        this.direction = Direction.South;
                    }

                    break;
                case Direction.South:
                    if (action == Action.RotateLeft)
                    {
                        this.direction = Direction.East;
                    }
                    else
                    {
                        this.direction = Direction.West;
                    }

                    break;
                case Direction.West:
                    if (action == Action.RotateLeft)
                    {
                        this.direction = Direction.South;
                    }
                    else
                    {
                        this.direction = Direction.North;
                    }

                    break;
                default:
                    throw new InvalidOperationException("Invalid direction on Rover");
            }
        }
    }
}
