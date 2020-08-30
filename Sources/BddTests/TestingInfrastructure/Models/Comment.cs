using JetBrains.Annotations;

namespace Mmu.Mlh.RestExtensions.BddTests.TestingInfrastructure.Models
{
    [PublicAPI]
    public class Comment
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostId { get; set; }
    }
}