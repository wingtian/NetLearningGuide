using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace NetLearningGuide.UnitTest.Working
{
    /// <summary>
    /// 装箱问题
    /// </summary>
    public class BoxTest
    {
        [Fact]
        public Task ComputeBox()
        {
            var pieces = new Pieces(9, 17.5m, 7);
            var pieces1 = new Pieces(17.5m, 9, 7);
            var caseInput = new Case(60, 40, 40);
            var test = ComputeCase(caseInput, pieces);
            var test1 = ComputeCase(caseInput, pieces1);
            test.Number.ShouldBe(60);
            test1.Number.ShouldBe(60);
            return Task.CompletedTask;
        }

        public class Pieces
        {
            public Pieces(decimal length, decimal width, decimal height)
            {
                Length = length;
                Width = width;
                Height = height;
            }
            public decimal Length { get; set; }
            public decimal Width { get; set; }
            public decimal Height { get; set; }
        }
        public class Case
        {
            public Case(decimal length, decimal width, decimal height, int number = 0, int lengthSideNumber = 0
                , int widthSideNumber = 0, int heightSideNumber = 0)
            {
                Length = length;
                Width = width;
                Height = height;
                Number = number;
                LengthSideNumber = lengthSideNumber;
                WidthSideNumber = widthSideNumber;
                HeightSideNumber = heightSideNumber;
            }
            public decimal Length { get; set; }
            public decimal Width { get; set; }
            public decimal Height { get; set; }
            public int Number { get; set; }
            public int LengthSideNumber { get; set; }
            public int WidthSideNumber { get; set; }
            public int HeightSideNumber { get; set; }
        }

        public class ComputeCaseResult
        {
            public int LoadQuantity { get; set; }
            public decimal RemainingLength { get; set; }
            public decimal RemainingWidth { get; set; }
            public decimal RemainingHeight { get; set; }
            public decimal Weight { get; set; }
        }
        private Case ComputeCase(Case cs, Pieces pc)
        {
            var caseList = new List<Case>();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    for (int k = 0; k < 20; k++)
                    {
                        caseList.Add(new Case(i * pc.Length, j * pc.Width, k * pc.Height, i * j * k, i, j, k));
                    }
                }
            }
            return caseList.Where(x => x.Length <= cs.Length && x.Width <= cs.Width && x.Height <= cs.Height)
                .OrderByDescending(x => x.Number).ToList().FirstOrDefault();
        }
    }
}
