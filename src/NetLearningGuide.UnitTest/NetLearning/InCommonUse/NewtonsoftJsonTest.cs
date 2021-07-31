using Newtonsoft.Json;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class NewtonsoftJsonTest
    {
        //https://www.cnblogs.com/mqxs/p/9646354.html
        #region MemberSerialization.OptIn
        [JsonObject(MemberSerialization.OptIn)]
        public class NewtonsoftJsonOptInModel
        {
            public int Age { get; set; }
            [JsonProperty]
            public string Name { get; set; }
            public string Sex { get; set; }
            public bool IsMarry { get; set; }
            public DateTime Birthday { get; set; }
        }

        [Fact]
        public Task NewtonsoftJsonOptInTestCase1()
        {
            var model = new NewtonsoftJsonOptInModel() { Age = 1, Birthday = DateTime.Today, IsMarry = true, Name = "Aaron", Sex = "Men" };
            var convert = JsonConvert.SerializeObject(model);
            convert.ShouldContain("Name");
            convert.ShouldNotContain("Age");
            convert.ShouldNotContain("Sex");
            convert.ShouldNotContain("IsMarry");
            convert.ShouldNotContain("Birthday");
            return Task.CompletedTask;
        }
        #endregion

        #region MemberSerialization.OptOut
        [JsonObject(MemberSerialization.OptOut)]
        public class NewtonsoftJsonOptOutModel
        { 
            public int Age { get; set; }
            [JsonIgnore]
            public string Name { get; set; }
            public string Sex { get; set; }
            public bool IsMarry { get; set; }
            public DateTime Birthday { get; set; }
        }

        [Fact]
        public Task NewtonsoftJsonOptOutTestCase1()
        {
            var model = new NewtonsoftJsonOptOutModel() { Age = 1, Birthday = DateTime.Today, IsMarry = true, Name = "Aaron", Sex = "Men" };
            var convert = JsonConvert.SerializeObject(model); 
            convert.ShouldNotContain("Name");
            convert.ShouldContain("Age");
            convert.ShouldContain("Sex");
            convert.ShouldContain("IsMarry");
            convert.ShouldContain("Birthday");
            return Task.CompletedTask;
        } 
        #endregion
    }
}
