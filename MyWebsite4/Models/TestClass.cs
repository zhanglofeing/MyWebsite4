using System;

namespace MyWebsite4.Models
{
    public class TestClass
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string Name { get; set; }

        public TestClass Left { get; set; }

        public TestClass Right { get; set; }

        public int Order { get; set; }
    }
}
