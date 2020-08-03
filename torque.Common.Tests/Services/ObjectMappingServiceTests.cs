using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using torque.Common.Contracts.Services;
using torque.Common.Enum;
using torque.Common.Models.Contracts.DatabaseObjects;
using torque.Common.Services;
using torque.Models.DatabaseObjects;

namespace torque.Common.Tests.Services
{
    class ObjectMappingServiceTests
    {
        private IObjectMappingService sut;

        [SetUp]
        public void SetUp()
        {
            sut = new ObjectMappingService();
        }

        [Test]
        public void Given2MatchingObjectsAreProvided_WhenMapObjectsIsCalled_ThenAnEmptyListIsReturned()
        {
            var listA = new List<IComparableEntity>
            {
                new Function { Definition = "thingA", Name = "nameA", Schema = "dbo" }
            };
            var listB = new List<IComparableEntity>
            {
                new Function { Definition = "thingA", Name = "nameA", Schema = "dbo" }
            };

            var result = sut.MapObjects(listA, listB);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void GivenObjectsOnlyIn1AreProvided_WhenMapObjectsIsCalled_ThenObjectsToCreateAreReturned()
        {
            var listA = new List<IComparableEntity>
            {
                new Function { Definition = "thingA", Name = "nameA", Schema = "dbo" }
            };
            var listB = new List<IComparableEntity>();

            var result = sut.MapObjects(listA, listB);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(ComparisonDirection.OnlyInSource, result.First().Direction);
            Assert.AreEqual("dbo.nameA", result.First().CanonicalName);
            Assert.AreEqual("thingA", result.First().ObjectDiff);
        }

        [Test]
        public void GivenObjectsOnlyIn2AreProvided_WhenMapObjectsIsCalled_ThenObjectsToDropAreReturned()
        {
            var listB = new List<IComparableEntity>
            {
                new Function { Definition = "thingA", Name = "nameA", Schema = "dbo" }
            };
            var listA = new List<IComparableEntity>();

            var result = sut.MapObjects(listA, listB);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(ComparisonDirection.OnlyInDest, result.First().Direction);
            Assert.AreEqual("dbo.nameA", result.First().CanonicalName);
            Assert.AreEqual("thingA", result.First().ObjectDiff);
        }

        [Test]
        public void GivenObjectsInBothButDifferent_WhenMapObjectsIsCalled_ThenObjectsToUpdateAreReturned()
        {
            var listA = new List<IComparableEntity>
            {
                new Function { Definition = "thingA", Name = "nameA", Schema = "dbo" }
            };
            var listB = new List<IComparableEntity>
            {
                new Function { Definition = "thingB", Name = "nameA", Schema = "dbo" }
            };

            var result = sut.MapObjects(listA, listB);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(ComparisonDirection.InBothButDifferent, result.First().Direction);
            Assert.AreEqual("dbo.nameA", result.First().CanonicalName);
            Assert.AreEqual("thingA", result.First().ObjectDiff);
        }

        [Test]
        public void GivenObjectsOfEachDirectionAreProvided_WhenMapObjectsIsCalled_ThenObjectsToCreateUpdateAndDropAreReturned()
        {
            var listA = new List<IComparableEntity>
            {
                new Function { Definition = "thingA", Name = "nameA", Schema = "dbo" },
                new Function { Definition = "thingB", Name = "nameB", Schema = "dbo" },
                new Function { Definition = "thingD", Name = "nameD", Schema = "dbo" }

            };
            var listB = new List<IComparableEntity>
            {
                new Function { Definition = "thingB", Name = "nameA", Schema = "dbo" },
                new Function { Definition = "thingB", Name = "nameC", Schema = "dbo" },
                new Function { Definition = "thingD", Name = "nameD", Schema = "dbo" }
            };

            var result = sut.MapObjects(listA, listB);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(ComparisonDirection.InBothButDifferent, result.First().Direction);
            Assert.AreEqual("dbo.nameA", result.First().CanonicalName);
            Assert.AreEqual("thingA", result.First().ObjectDiff);
            Assert.AreEqual(ComparisonDirection.OnlyInSource, result.Skip(1).Take(1).First().Direction);
            Assert.AreEqual("dbo.nameB", result.Skip(1).Take(1).First().CanonicalName);
            Assert.AreEqual("thingB", result.Skip(1).Take(1).First().ObjectDiff);
            Assert.AreEqual(ComparisonDirection.OnlyInDest, result.Skip(2).Take(1).First().Direction);
            Assert.AreEqual("dbo.nameC", result.Skip(2).Take(1).First().CanonicalName);
            Assert.AreEqual("thingB", result.Skip(2).Take(1).First().ObjectDiff);
        }
    }
}
