﻿using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.CSharpLeaning
{
    public class ChapterSix
    {
        #region 隐式转换
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <returns></returns>
        [Fact]
        public Task ImplicitTestCase1()
        {
            UtmCoordinates test = new GpsCoordinates();
            test.Test.ShouldBe("HHH");
            return Task.CompletedTask;
        }

        class GpsCoordinates
        {
            public static implicit operator UtmCoordinates(GpsCoordinates gps)
            {
                return new UtmCoordinates()
                {
                    Test = "HHH"
                };
            }
        }

        class UtmCoordinates
        {
            public string Test { get; set; }
        }
        #endregion
        #region 显式转换
        /// <summary>
        /// 显式转换
        /// </summary>
        /// <returns></returns>
        [Fact]
        public Task ExplicitTestCase1()
        {
            var test = (UtmCoordinatesCase2)(new GpsCoordinatesCase2()); //(UtmCoordinatesCase2) 代表显示转换
            test.Test.ShouldBe("HHH");
            return Task.CompletedTask;
        }

        class GpsCoordinatesCase2
        {
            public static explicit operator UtmCoordinatesCase2(GpsCoordinatesCase2 gps)
            {
                return new UtmCoordinatesCase2()
                {
                    Test = "HHH"
                };
            }
        }
        class UtmCoordinatesCase2
        {
            public string Test { get; set; }
        }
        #endregion 
        #region protected 访问修饰符 
        public class ProtectTest
        {
            protected Guid Id { get; set; }
        }

        public class ContactTest : ProtectTest
        {
            void Save()
            {
                var test = Id;
            }

            void Load(ProtectTest test)
            {
                // 错误  CS1540 无法通过“ChapterSix.ProtectTest”类型的限定符访问受保护的成员“ChapterSix.ProtectTest.Id”；
                // 限定符必须是“ChapterSix.ContactTest”类型(或者从该类型派生)
                //var model = test.Id; 
            }
        }
        #endregion
    }
}
