using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        [Fact]
        public Task NewtonsoftJsonOptInTestCase2()
        {
            var convert = JsonConvert.SerializeObject(null);
            convert.ShouldBe("null");
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

        public class NewtonsoftJsonPropertyNameDeserializeObjectModel
        {
            public int AGE { get; set; }
            public string ENNAME { get; set; }
            public string SEX { get; set; }
            public bool marrytest { get; set; }
            public DateTime birthday { get; set; }
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

        /// <summary>
        /// 测试DeserializeObject 不区分字段大小写
        /// </summary>
        /// <returns></returns>
        [Fact]
        public Task NewtonsoftJsonPropertyNameTestCase2()
        {
            var model = new NewtonsoftJsonPropertyNameModel() { Age = 1, Birthday = DateTime.Today, IsMarry = true, Name = "Aaron", Sex = "Men" };
            var convert = JsonConvert.SerializeObject(model);
            convert.ShouldContain("EnName");
            convert.ShouldContain("Age");
            convert.ShouldContain("Sex");
            convert.ShouldContain("MarryTest");
            convert.ShouldContain("Birthday");

            var desConvert = JsonConvert.DeserializeObject<NewtonsoftJsonPropertyNameDeserializeObjectModel>(convert);
            desConvert.AGE.ShouldBe(model.Age);
            desConvert.birthday.ShouldBe(model.Birthday);
            desConvert.marrytest.ShouldBe(model.IsMarry);
            desConvert.ENNAME.ShouldBe(model.Name);
            desConvert.SEX.ShouldBe(model.Sex);
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
            var model = new NewtonsoftJsonDefaultValueModel() { Age = 1, Birthday = DateTime.Today, IsMarry = true, Sex = "Men", Name = "Aaron" };
            var convert = JsonConvert.SerializeObject(model);
            convert.ShouldContain("Aaron");
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            convert = JsonConvert.SerializeObject(model, Formatting.Indented, jsetting);
            convert.ShouldNotContain("Aaron");
            return Task.CompletedTask;
        }
        #endregion

        [Fact]
        public Task NewtonsoftJsonTestCase1()
        {
            var a = new string[] { };
            var b = new string[] { "" };
            var c = new List<string>();
            var d = new List<string>() { "" };
            var convertA = JsonConvert.SerializeObject(a);
            var convertB = JsonConvert.SerializeObject(b);
            var convertC = JsonConvert.SerializeObject(c);
            var convertD = JsonConvert.SerializeObject(d);
            convertA.ShouldBe(convertC);
            convertB.ShouldBe(convertD);
            return Task.CompletedTask;
        }
        //TODO 未完待续
        //{"code":"503","msg":"未找到该分类或该分类已删除"}
        public class ReturnList<T>
        {
            [JsonProperty(PropertyName = "code")]
            public string Code { get; set; }
            [JsonProperty(PropertyName = "msg")]
            public string Msg { get; set; }
            [JsonProperty(PropertyName = "data")]
            public T Data { get; set; }
        }

        [Fact]
        public Task NewtonsoftDeserializeObjectTestCase1()
        {
            var msg = "{\"code\":\"503\",\"msg\":\"未找到该分类或该分类已删除\"}";
            var convert = JsonConvert.DeserializeObject<ReturnList<NewtonsoftJsonDefaultValueModel>>(msg);
            return Task.CompletedTask;
        }

        [Fact]
        public Task NewtonsoftJsonTestCase2()
        {
            var a = new List<int>() { 1, 2, 107, 4 };
            var convertA = JsonConvert.SerializeObject(a);
            convertA.ShouldBe("[1,2,107,4]");
            return Task.CompletedTask;
        }

        public class Input
        {
            [JsonProperty("product_number")]
            public int ProductNumber;

            [JsonProperty("inference_date")]
            public string InferenceDate;

            [JsonProperty("succeeded")]
            public bool Succeeded;

            [JsonProperty("msg")]
            public string Msg;

            [JsonProperty("model")]
            public string Model;

            [JsonProperty("data")]
            public Dictionary<string, string> Data;
        }
        [Fact]
        public Task NewtonsoftJsonTestCase3()
        {
            var a = "{\"0\": {\"product_number\": 3, \"inference_date\": \"2021-11-22\", \"succeeded\": true, \"msg\": \"Ok\", \"model\": \"Transformer\", \"data\": {\"2021-10-16\": \"7\", \"2021-10-17\": \"7\", \"2021-10-18\": \"7\", \"2021-10-19\": \"7\", \"2021-10-20\": \"7\", \"2021-10-21\": \"7\", \"2021-10-22\": \"7\"}}, \"1\": {\"product_number\": 80, \"inference_date\": \"2021-11-22\", \"succeeded\": true, \"msg\": \"Only 1 datapoint\", \"model\": \"Forward Filled\", \"data\": {\"2021-08-25\": \"13\", \"2021-08-26\": \"13\", \"2021-08-27\": \"13\", \"2021-08-28\": \"13\", \"2021-08-29\": \"13\", \"2021-08-30\": \"13\", \"2021-08-31\": \"13\"}}, \"2\": {\"product_number\": 164, \"inference_date\": \"2021-11-22\", \"succeeded\": true, \"msg\": \"Not enough datapoint\", \"model\": \"Extrapolated\", \"data\": {\"2021-10-13\": \"12\", \"2021-10-14\": \"12\", \"2021-10-15\": \"12\", \"2021-10-16\": \"12\", \"2021-10-17\": \"12\", \"2021-10-18\": \"12\", \"2021-10-19\": \"12\"}}, \"3\": {\"product_number\": 210, \"inference_date\": \"2021-11-22\", \"succeeded\": true, \"msg\": \"Only 1 datapoint\", \"model\": \"Forward Filled\", \"data\": {\"2021-09-29\": \"11\", \"2021-09-30\": \"11\", \"2021-10-01\": \"11\", \"2021-10-02\": \"11\", \"2021-10-03\": \"11\", \"2021-10-04\": \"11\", \"2021-10-05\": \"11\"}}, \"4\": {\"product_number\": 154, \"inference_date\": \"2021-11-22\", \"succeeded\": true, \"msg\": \"Not enough datapoint\", \"model\": \"Extrapolated\", \"data\": {\"2021-09-10\": \"14\", \"2021-09-11\": \"15\", \"2021-09-12\": \"14\", \"2021-09-13\": \"13\", \"2021-09-14\": \"13\", \"2021-09-15\": \"14\", \"2021-09-16\": \"16\"}}}";
            var convertA = JsonConvert.DeserializeObject<Dictionary<string, Input>>(a);

            if (convertA != null)
            {
                var test = convertA.Values.FirstOrDefault(x => x.ProductNumber == 3);

                var test2 = new Dictionary<string, Input>();
                var test22 = test2.Values.FirstOrDefault(x => x.ProductNumber == 3);
                test22.ShouldBeNull();
                test.ShouldBeNull();
            }

            return Task.CompletedTask;
        }
    }
}
