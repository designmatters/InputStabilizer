using Xunit;
using FluentAssertions;
using InputStabilizer;

namespace InputStabilizer.Tests
{
    public class InputStabilizerTests
    {
        [Fact]
        public void Should_Return_Value_After_Threshold_Is_Met()
        {
            var stabilizer = new InputStabilizer<string>(3);

            stabilizer.GetStableInput("A").Should().BeNull();
            stabilizer.GetStableInput("A").Should().BeNull();
            stabilizer.GetStableInput("A").Should().Be("A");
        }

        [Fact]
        public void Should_Return_Stable_Value_When_Input_Is_Already_Stable()
        {
            var stabilizer = new InputStabilizer<string>(2);

            stabilizer.GetStableInput("X").Should().BeNull();
            stabilizer.GetStableInput("X").Should().Be("X");
            stabilizer.GetStableInput("X").Should().Be("X");
            stabilizer.GetStableInput("X").Should().Be("X");
        }

        [Fact]
        public void Test3() //TODO
        {
            var stabilizer = new InputStabilizer<string>(2);

            stabilizer.GetStableInput("X").Should().BeNull();
            stabilizer.GetStableInput("X").Should().Be("X");
            stabilizer.GetStableInput("X").Should().Be("X");
            stabilizer.GetStableInput("Y").Should().Be("X");
            stabilizer.GetStableInput("Y").Should().Be("Y");
        }

        [Fact]
        public void Should_Not_Return_Value_If_Not_Repeated_Enough()
        {
            var stabilizer = new InputStabilizer<string>(4);

            stabilizer.GetStableInput("X").Should().BeNull();
            stabilizer.GetStableInput("X").Should().BeNull();
            stabilizer.GetStableInput("X").Should().BeNull();
            stabilizer.GetStableInput("Y").Should().BeNull();
            stabilizer.GetStableInput("X").Should().BeNull();
        }

        [Fact]
        public void Should_Handle_Alternating_Inputs_Without_Stabilizing()
        {
            var stabilizer = new InputStabilizer<string>(3);

            stabilizer.GetStableInput("A").Should().BeNull();
            stabilizer.GetStableInput("B").Should().BeNull();
            stabilizer.GetStableInput("A").Should().BeNull();
            stabilizer.GetStableInput("B").Should().BeNull();
        }

        [Fact]
        public void Should_Return_Default_For_Value_Types_When_Unstable()
        {
            var stabilizer = new InputStabilizer<int?>(3);

            stabilizer.GetStableInput(5).Should().BeNull();
            stabilizer.GetStableInput(5).Should().BeNull();
            stabilizer.GetStableInput(5).Should().Be(5);
            stabilizer.GetStableInput(5).Should().Be(5);
            stabilizer.GetStableInput(6).Should().Be(5);
        }

        [Fact]
        public void Should_Work_With_Custom_Objects()
        {
            var obj1 = new Person("Alice");
            var obj2 = new Person("Alice");
            var obj3 = new Person("Alice");

            var stabilizer = new InputStabilizer<Person>(3);

            stabilizer.GetStableInput(obj1).Should().BeNull();
            stabilizer.GetStableInput(obj2).Should().BeNull();
            stabilizer.GetStableInput(obj3).Should().BeEquivalentTo(obj3);
        }

        private record Person(string Name);
    }
}