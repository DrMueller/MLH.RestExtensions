using JetBrains.Annotations;

namespace Mmu.Mlh.RestExtensions.BddTests.TestingInfrastructure.Models
{
    [UsedImplicitly]
    public class Post
    {
        public string Body { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        [UsedImplicitly]
        public int PostId { get; set; }
    }
}