using NetLearningGuide.Core.MySqlDomain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLearningGuide.Core.Domain.Demo
{
    [Table("test_dbup")]
    public class TestDbUp : IEntity
    {
        [Key, Column("GUID", TypeName = "char(36)")]
        public Guid GUID { get; set; }
        [Column("DescInfo", TypeName = "char(45)")]
        public string DescInfo { get; set; }
    }
}
