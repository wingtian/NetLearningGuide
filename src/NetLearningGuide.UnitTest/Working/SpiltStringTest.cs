using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.Working
{
    public class SpiltStringTest
    {
        [Fact]
        public Task SpiltStringTestCase1()
        {
            var a = @"30*30*30
35*35*35
39*36*36
40*30*20
40*30*30
40*40*30
42*31*26
44*44*36
45*35*30
45*40*40
45*45*45
46*29*24
46*32*28
46*46*36
47*35*39
48*30*45
50*30*30
50*30*40
50*35*40
50*40*30
50*40*33
50*40*38
50*40*40
50*40*50
50*50*40
50*50*50
52*41*52
53*29*37
55*40*35
55*40*48
55*45*45
56*42*30
58*38*34
60*40*45
60*40*50
60*46*46
60*50*35
60*54*35
60*40*40";
            var sp = a.Replace("\r\n", " ").Split(' ').ToList();
           var list = new List<BoxTest.Pieces>();  //Pieces
            foreach (var s in sp)
            {
                var value = s.Split('*').ToList();
                list.Add(new BoxTest.Pieces(Convert.ToInt16(value[0]), Convert.ToInt16(value[1]), Convert.ToInt16(value[2])));
            }
            return Task.CompletedTask;
        }
    }
}
