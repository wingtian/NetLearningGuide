using Shouldly;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class YieldTest
    {
        ///https://www.cnblogs.com/blueberryzzz/p/8678700.html
        [Fact]
        public Task YieldTestCase1()
        {
            IEnumerable<int> result = EnumerableFuc();
            result.Count().ShouldBe(2);
            return Task.CompletedTask;
        }

        public IEnumerable<int> EnumerableFuc()
        {
            yield return 1;
            yield return 2;
            yield break;
            yield return 3;
        }
        public IEnumerable<EnumerableClassTest1> EnumerableFuc1()
        {
            yield return new EnumerableClassTest1("Te");
            yield return new EnumerableClassTest1("ST");
            yield return new EnumerableClassTest1("BB");
        }

        public class EnumerableClassTest1
        {
            public EnumerableClassTest1(string input)
            {
                Input = input;
            }
            public string Input { get; set; }
        }
        [Fact]
        public Task YieldTestCase2()
        {
            IEnumerator<int> result = EnumerableFuc().GetEnumerator();
            var current1 = result.Current;
            current1.ShouldBe(0);
            var next1 = result.MoveNext();
            next1.ShouldBeTrue();
            var current2 = result.Current;
            current2.ShouldBe(1);
            var next2 = result.MoveNext();
            next2.ShouldBeTrue();
            var current3 = result.Current;
            current3.ShouldBe(2);
            var next3 = result.MoveNext();
            next3.ShouldBeFalse();
            var current4 = result.Current;
            current4.ShouldBe(2);
            var next4 = result.MoveNext();
            next4.ShouldBeFalse();
            result.Dispose();
            return Task.CompletedTask;
        }
        [Fact]
        public Task YieldTestCase3()
        {
            IEnumerator<EnumerableClassTest1> result = EnumerableFuc1().GetEnumerator();
            var current1 = result.Current;
            current1.ShouldBeNull();
            var next1 = result.MoveNext();
            next1.ShouldBeTrue();
            var current2 = result.Current;
            current2.ShouldNotBeNull();
            current2.Input.ShouldBe("Te");
            var next2 = result.MoveNext();
            next2.ShouldBeTrue();
            var current3 = result.Current;
            current3.ShouldNotBeNull();
            current3.Input.ShouldBe("ST");
            var next3 = result.MoveNext();
            next3.ShouldBeTrue();
            var current4 = result.Current;
            current4.ShouldNotBeNull();
            current4.Input.ShouldBe("BB");
            var next4 = result.MoveNext();
            next4.ShouldBeFalse();
            var current5 = result.Current;
            current5.ShouldNotBeNull();
            current5.Input.ShouldBe("BB");
            var next5 = result.MoveNext();
            next5.ShouldBeFalse();
            result.Dispose();
            return Task.CompletedTask;
        }

        public IEnumerable<EnumerableClassTest1> EnumerableFuc2()
        {
            yield return new EnumerableClassTest1("Te");
            yield return new EnumerableClassTest1("ST");
            yield return new EnumerableClassTest1("BB");
        }

        public class EnumerableClassTest2 : IEnumerator
        {
            public EnumerableClassTest2(IEnumerable<EnumerableClassTest1> input)
            {
                _input = input;
                _position = -1;
                _current = null;
            }

            private IEnumerable<EnumerableClassTest1> _input;
            private int _position;
            private EnumerableClassTest1 _current;

            public object Current => _current;

            public bool MoveNext()
            {
                if (_position < _input.Count() - 1)
                {
                    _current = _input.ElementAt(++_position);
                    return true;
                }
                else
                {
                    _position = -1;
                    return false;
                }
            }

            public void Reset()
            {
                _position = -1;
            }
        }
        [Fact]
        public Task YieldTestCase4()
        {
            var result = new EnumerableClassTest2(EnumerableFuc2());
            var current1 = result.Current;
            current1.ShouldBeNull();
            var next1 = result.MoveNext();
            next1.ShouldBeTrue();
            var current2 = (EnumerableClassTest1)result.Current;
            current2.ShouldNotBeNull();
            current2.Input.ShouldBe("Te");
            var next2 = result.MoveNext();
            next2.ShouldBeTrue();
            var current3 = (EnumerableClassTest1)result.Current;
            current3.ShouldNotBeNull();
            current3.Input.ShouldBe("ST");
            var next3 = result.MoveNext();
            next3.ShouldBeTrue();
            var current4 = (EnumerableClassTest1)result.Current;
            current4.ShouldNotBeNull();
            current4.Input.ShouldBe("BB");
            var next4 = result.MoveNext();
            next4.ShouldBeFalse();
            var current5 = (EnumerableClassTest1)result.Current;
            current5.ShouldNotBeNull();
            current5.Input.ShouldBe("BB");
            var next5 = result.MoveNext();
            next5.ShouldBeTrue();
            var current6 = (EnumerableClassTest1)result.Current;
            current6.ShouldNotBeNull();
            current6.Input.ShouldBe("Te");
            //result.Dispose();
            return Task.CompletedTask;
        }
    }
}
