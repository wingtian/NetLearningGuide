using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    //https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/
    public class WriteTest
    {
        #region Basic tests using xUnit [Fact]
        public class Calculator
        {
            public int Add(int value1, int value2)
            {
                return value1 + value2;
            }
        }
        [Fact]
        public void CanAdd()
        {
            var calculator = new Calculator();

            int value1 = 1;
            int value2 = 2;

            var result = calculator.Add(value1, value2);

            Assert.Equal(3, result);
        }
        #endregion
        #region Using the [Theory] attribute to create parameterised tests with [InlineData] 
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-4, -6, -10)]
        [InlineData(-2, 2, 0)]
        [InlineData(int.MinValue, -1, int.MaxValue)]
        public void CanAddTheory(int value1, int value2, int expected)
        {
            var calculator = new Calculator();

            var result = calculator.Add(value1, value2);

            Assert.Equal(expected, result);
        }
        #endregion

        #region Using a dedicated data class with [ClassData]
        [Theory]
        [ClassData(typeof(CalculatorTestData))]
        public void CanAddTheoryClassData(int value1, int value2, int expected)
        {
            var calculator = new Calculator();

            var result = calculator.Add(value1, value2);

            Assert.Equal(expected, result);
        }
        public class CalculatorTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 1, 2, 3 };
                yield return new object[] { -4, -6, -10 };
                yield return new object[] { -2, 2, 0 };
                yield return new object[] { int.MinValue, -1, int.MaxValue };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        #endregion
        #region Using generator properties with the [MemberData] properties
        [Theory]
        [MemberData(nameof(Data))]
        public void CanAddTheoryMemberDataProperty(int value1, int value2, int expected)
        {
            var calculator = new Calculator();

            var result = calculator.Add(value1, value2);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { 1, 2, 3 },
                new object[] { -4, -6, -10 },
                new object[] { -2, 2, 0 },
                new object[] { int.MinValue, -1, int.MaxValue },
            };
        #endregion
        #region Loading data from a method on the test class
        [Theory]
        [MemberData(nameof(GetData), parameters: 3)]
        public void CanAddTheoryMemberDataMethod(int value1, int value2, int expected)
        {
            var calculator = new Calculator();

            var result = calculator.Add(value1, value2);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> GetData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] { 1, 2, 3 },
                new object[] { -4, -6, -10 },
                new object[] { -2, 2, 0 },
                new object[] { int.MinValue, -1, int.MaxValue },
            };

            return allData.Take(numTests);
        }
        #endregion

        #region Loading data from a property or method on a different class
        public class CalculatorTests
        {
            [Theory]
            [MemberData(nameof(CalculatorData.Data), MemberType = typeof(CalculatorData))]
            public void CanAddTheoryMemberDataMethod(int value1, int value2, int expected)
            {
                var calculator = new Calculator();

                var result = calculator.Add(value1, value2);

                Assert.Equal(expected, result);
            }
        }

        public class CalculatorData
        {
            public static IEnumerable<object[]> Data =>
                new List<object[]>
                {
                    new object[] { 1, 2, 3 },
                    new object[] { -4, -6, -10 },
                    new object[] { -2, 2, 0 },
                    new object[] { int.MinValue, -1, int.MaxValue },
                };
        }
        #endregion
    }
}
