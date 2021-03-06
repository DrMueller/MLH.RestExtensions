﻿using JetBrains.Annotations;

namespace Mmu.Mlh.RestExtensions.IntegrationTests.TestingInfrastructure.Models
{
    public class Todo
    {
        public bool Completed { get; set; }

        [UsedImplicitly]
        public int Id { get; set; }

        public string Title { get; set; }
        public int UserId { get; set; }
    }
}