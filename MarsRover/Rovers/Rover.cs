using System;
using System.Collections.Generic;
using MarsRover.Enums;
using Action = MarsRover.Enums.Action;

namespace MarsRover.Rovers
{
    public class Rover : IRover
    {
        /// <summary>
        /// The series of actions the Rover will take
        /// </summary>
        public Queue<Action> Actions { get; set; }

        /// <summary>
        /// Initilises an instance of the <see cref="Rover"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates of the Rover</param>
        /// <param name="direction">The direction the Rover is facing</param>
        public Rover(Point coordinates, Direction direction)
            : base(coordinates, direction)
        {
        }

        /// <summary>
        /// Initilises an instance of the <see cref="Rover"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates of the Rover</param>
        /// <param name="direction">The direction the Rover is facing</param>
        /// <param name="actions">The series of actions the Rover will take</param>
        public Rover(Point coordinates, Direction direction, Queue<Action> actions)
            : base(coordinates, direction)
        {
            this.Actions = actions;
        }

        /// <inheritdocs />
        public override Action GetNextMove()
        {
            if (this.Actions != null && this.Actions.Count > 0)
            {
                return this.Actions.Peek();
            }

            return Action.Nothing;
        }

        /// <inheritdocs />
        public override void Move()
        {
            if (this.Actions == null || this.Actions.Count == 0)
            {
                throw new InvalidOperationException("Rover has no more movements to make");
            }

            Action action = this.Actions.Dequeue();
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

        /// <summary>
        /// Moves the Rover forward in the direction it is facing
        /// </summary>
        public void MoveForward()
        {
            switch (this.Direction)
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

        /// <summary>
        /// Rotates the Rover based on the action it is performing
        /// </summary>
        /// <param name="action">The action the Rover is performing</param>
        public void Rotate(Action action)
        {
            if (action != Action.RotateLeft && action != Action.RotateRight)
            {
                throw new ArgumentOutOfRangeException(nameof(action));
            }

            switch (this.Direction)
            {
                case Direction.North:
                    this.Direction = action == Action.RotateLeft ? Direction.West : Direction.East;

                    break;
                case Direction.East:
                    this.Direction = action == Action.RotateLeft ? Direction.North : Direction.South;

                    break;
                case Direction.South:
                    this.Direction = action == Action.RotateLeft ? Direction.East : Direction.West;

                    break;
                case Direction.West:
                    this.Direction = action == Action.RotateLeft ? Direction.South : Direction.North;

                    break;
                default:
                    throw new InvalidOperationException("Invalid direction on Rover");
            }
        }
    }
}
