using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover;
using System.Collections.Generic;
using System.Collections;

namespace MarsRoverTests
{
    [TestClass]
    public class PlanetTests
    {
        /// <summary>
        /// Tests that a valid grid is build if one coordinate is provided for the Planet
        /// </summary>
        [TestMethod]
        public void GetGrid_OneCoordinate_BuildsGrid()
        {
            // Arrange
            var planet = new Planet(1, 1);

            var expectedGrid = new Dictionary<Point, bool>
            {
                { new Point(0, 0), false }
            };

            // Act
            // Assert
            CollectionAssert.AreEquivalent(expectedGrid, (ICollection)planet.Grid);
        }

        /// <summary>
        /// Tests that a valid grid is built with correct Points as keys for the Planet
        /// </summary>
        [TestMethod]
        public void GetGrid_LazyLoaded_BuildsGrid()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var expectedGrid = new Dictionary<Point, bool>
            {
                { new Point(0, 0), false },
                { new Point(0, 1), false },
                { new Point(1, 0), false },
                { new Point(1, 1), false }
            };

            // Act
            // Assert
            CollectionAssert.AreEquivalent(expectedGrid, (ICollection)planet.Grid);
        }

        /// <summary>
        /// Tests that the key accessor works on the grid after being built
        /// </summary>
        [TestMethod]
        public void GetGrid_WithBuiltGrid_AllowsKeyAccess()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var tempValue = planet.Grid;

            // Act
            // Assert            
            Assert.IsFalse(planet.Grid[new Point(0, 0)]);
        }

        /// <summary>
        /// Tests that a null value is returned if a Planet has a height and a width of zero
        /// </summary>
        [TestMethod]
        public void GetGrid_ZeroHeightAndWidthPlanet_ReturnsNull()
        {
            // Arrange
            var planet = new Planet(0, 0);

            // Act
            // Assert
            Assert.IsNull(planet.Grid);
        }

        /// <summary>
        /// Tests that a null value is returned if a Planet has a height of zero, with a valid width
        /// </summary>
        [TestMethod]
        public void GetGrid_ZeroHeight_ReturnsNull()
        {
            // Arrange
            var planet = new Planet(0, 1);

            // Act
            // Assert
            Assert.IsNull(planet.Grid);
        }

        /// <summary>
        /// Tests that a null value is returned if a Planet has a width of zero, with a valid height
        /// </summary>
        [TestMethod]
        public void GetGrid_ZeroWidth_ReturnsNull()
        {
            // Arrange
            var planet = new Planet(1, 0);

            // Act
            // Assert
            Assert.IsNull(planet.Grid);
        }

        /// <summary>
        /// Tests that a null value will cause the BuildPlanet method to throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuildPlanet_WithNullRovers_ThrowsException()
        {
            // Arrange
            var planet = new Planet(1, 1);

            // Act
            planet.BuildPlanet(null);
        }

        /// <summary>
        /// Tests that an empty collection passed into the BuildPlanet method will not cause any changes to the grid
        /// </summary>
        [TestMethod]
        public void BuildPlanet_WithEmptyRovers_DoesNotSetGridTrue()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var expectedGrid = new Dictionary<Point, bool>
            {
                { new Point(0, 0), false },
                { new Point(0, 1), false },
                { new Point(1, 0), false },
                { new Point(1, 1), false }
            };

            // Act
            planet.BuildPlanet(new List<IRover>());

            // Assert
            CollectionAssert.AreEquivalent(expectedGrid, (ICollection)planet.Grid);
        }

        /// <summary>
        /// Tests that a Rover with an invalid coordinate causes an Exception to be thrown
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BuildPlanet_WithInvalidRoverCoordinates_ThrowsException()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var rovers = new List<IRover>
            {
                new Rover(new Point(3, 3))
            };

            // Act
            planet.BuildPlanet(rovers);

            Assert.Fail();
        }

        /// <summary>
        /// Tests that two Rovers at the same coordinates causes an Exception to be thrown
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BuildPlanet_WithDuplicateRoverCoordinates_ThrowsException()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 1)),
                new Rover(new Point(0, 1))
            };

            // Act
            planet.BuildPlanet(rovers);

            Assert.Fail();
        }

        /// <summary>
        /// Tests that the valid grid is built when a valid collection of Rovers is input
        /// </summary>
        [TestMethod]
        public void BuildPlanet_WithValidCoordinates_ReturnsValidGrid()
        {
            var planet = new Planet(2, 2);

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 0)),
                new Rover(new Point(1, 1))
            };

            var expectedGrid = new Dictionary<Point, bool>
            {
                { new Point(0, 0), true },
                { new Point(0, 1), false },
                { new Point(1, 0), false },
                { new Point(1, 1), true }
            };

            // Act
            planet.BuildPlanet(rovers);

            CollectionAssert.AreEquivalent(expectedGrid, (ICollection)planet.Grid);
        }

        /// <summary>
        /// Tests that the valid grid is built when a height that is different to the width is used
        /// </summary>
        [TestMethod]
        public void BuildPlanet_WithDifferentDimensions_ReturnsValidGrid()
        {
            var planet = new Planet(1, 2);

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 0)),
                new Rover(new Point(1, 0))
            };

            var expectedGrid = new Dictionary<Point, bool>
            {
                { new Point(0, 0), true },
                { new Point(1, 0), true }
            };

            // Act
            planet.BuildPlanet(rovers);

            CollectionAssert.AreEquivalent(expectedGrid, (ICollection)planet.Grid);
        }
    }
}
