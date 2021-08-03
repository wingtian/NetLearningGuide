using Newtonsoft.Json;
using Shouldly;
using System;
using System.ComponentModel;
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
        #region PropertyName  
        public class NewtonsoftJsonPropertyNameModel
        {
            public int Age { get; set; }
            [JsonProperty(PropertyName = "EnName")]
            public string Name { get; set; }
            public string Sex { get; set; }
            [JsonProperty(PropertyName = "MarryTest")]
            public bool IsMarry { get; set; }
            public DateTime Birthday { get; set; }
        }

        [Fact]
        public Task NewtonsoftJsonPropertyNameTestCase1()
        {
            var model = new NewtonsoftJsonPropertyNameModel() { Age = 1, Birthday = DateTime.Today, IsMarry = true, Name = "Aaron", Sex = "Men" };
            var convert = JsonConvert.SerializeObject(model);
            convert.ShouldContain("EnName");
            convert.ShouldContain("Age");
            convert.ShouldContain("Sex");
            convert.ShouldContain("MarryTest");
            convert.ShouldContain("Birthday");

            var desConvert = JsonConvert.DeserializeObject<NewtonsoftJsonPropertyNameModel>(convert);
            desConvert.Age.ShouldBe(model.Age);
            desConvert.Birthday.ShouldBe(model.Birthday);
            desConvert.IsMarry.ShouldBe(model.IsMarry);
            desConvert.Name.ShouldBe(model.Name);
            desConvert.Sex.ShouldBe(model.Sex);
            return Task.CompletedTask;
        }
        #endregion
        #region DefaultValue  
        public class NewtonsoftJsonDefaultValueModel
        {
            public int Age { get; set; }
            [DefaultValue("Aaron")]
            public string Name { get; set; }
            public string Sex { get; set; }
            public bool IsMarry { get; set; }
            public DateTime Birthday { get; set; }
        }

        [Fact]
        public Task NewtonsoftJsonDefaultValueTestCase1()
        {
            var model = new NewtonsoftJsonDefaultValueModel() { Age = 1, Birthday = DateTime.Today, IsMarry = true, Sex = "Men" ,Name = "Aaron" };
            var convert = JsonConvert.SerializeObject(model);
            convert.ShouldContain("Aaron");
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            convert = JsonConvert.SerializeObject(model, Formatting.Indented, jsetting);
            convert.ShouldNotContain("Aaron"); 
            return Task.CompletedTask;
        }
        #endregion
    }
}
