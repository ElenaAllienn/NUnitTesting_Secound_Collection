using NUnit.Framework.Constraints;

namespace CollectionT.NUnitTests
{
    public class CollectionTests
    {


        [Test]
        public void Test_Collection_EmptyConstuctor()
        {
            //Arrange and Act
            var coll = new Collection<int>();

            //Assert
            Assert.AreEqual(coll.ToString(), "[]");

        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            //Arrange and Act
            var coll = new Collection<int>(5);

            //Assert
            Assert.AreEqual(coll.ToString(), "[5]");

        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            //Arrange and Act
            var coll = new Collection<int>(5, 10, 15);
            //Assert
            Assert.That(coll.ToString(), Is.EqualTo("[5, 10, 15]"));
        }

        [Test]
        public void Test_Collection_CountAndCapacity()
        {
            //Arrange and Act
            var coll = new Collection<int>(5, 10, 15);
            //Assert
            Assert.AreEqual(coll.Count, 3, "Check for Count");
            Assert.That(coll.Capacity, Is.GreaterThanOrEqualTo(coll.Count));
        }

        [Test]
        public void Test_Collection_Add()
        {
            //Arrange
            var coll = new Collection<string>("Ivan", "Maria");
            //Act
            coll.Add ("Elena");

            //Assert
            Assert.AreEqual(coll.ToString(), "[Ivan, Maria, Elena]");
  
        }
        [Test]
        public void Test_Collection_AddRange()
        {
            //Arrange
            var coll = new Collection<string>("Ivan", "Maria", "Elena");
            //Act
            coll.AddRange("Petar", "Gosho");

            //Assert
            Assert.AreEqual(coll.ToString(), "[Ivan, Maria, Elena, Petar, Gosho]");

        }
        [Test]
        public void Test_Collection_GetByIndex()
        {   //Arrange and Act
            var collection = new Collection<int>(5, 6, 7);
            var item = collection[1];

            //Assert
            Assert.That(item.ToString(), Is.EqualTo("6"));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {   //Arrange and Act
            var collection = new Collection<int>(5, 6, 7);
            collection[1] = 666;

            //Assert
            Assert.That(collection.ToString(), Is.EqualTo("[5, 666, 7]"));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            //Arrange
            var coll = new Collection<string>("Ivan", "Maria", "Elena");

            //Assert
            Assert.That(() => { var item = coll[3]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_GetByInvalidIndexx()
        {
            var names = new Collection<string>("Bob", "Joe");
            Assert.That(() => { var name = names[-1]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[2]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[500]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Bob, Joe]"));
        }
        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();
            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }
        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            var names = new Collection<string>("Teddy", "Gerry");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();
            var nested = new Collection<object>(names, nums, dates);
            string nestedToString = nested.ToString();
            Assert.That(nestedToString,
              Is.EqualTo("[[Teddy, Gerry], [10, 20], []]"));
        }
        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MillionItems()
        {
            const int itemsCount = 1000000;
            var nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
                nums.RemoveAt(i);
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }
        [Test]
        public void Test_Collection_InsertAtStart()
        {   
            //Arrange
            var collection = new Collection<int>(25, 36, 777);
            //Act
            collection.InsertAt(0, 55);
            //Assert
            Assert.That(collection.ToString(), Is.EqualTo("[55, 25, 36, 777]"));

        }
        [Test]
        public void Test_Collection_ExchangeFirstLast()
        {
            //Arrange
            var collection = new Collection<int>(25, 36, 777);
            //Act
            collection.Exchange(0, 2);
            //Assert
            Assert.That(collection.ToString(), Is.EqualTo("[777, 36, 25]"));
        }

        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            //Arrange
            var coll = new Collection<string>("Ivan", "Maria", "Elena", "Petar", "Gosho");
            //Act
            coll.RemoveAt(4);

            //Assert
            Assert.AreEqual(coll.ToString(), "[Ivan, Maria, Elena, Petar]");

        }
        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            //Arrange
            var collection = new Collection<int>(55, 25, 44, 36, 777);
            //Act
            collection.RemoveAt(2);
            //Assert
            Assert.That(collection.ToString(), Is.EqualTo("[55, 25, 36, 777]"));

        }

        //this test failed with Exeption
        [Test]
        public void Test_Collection_RemoveAtInvalidIndex()
        {
            //Arrange
            var nums = new Collection<int>(55, 33, 44, 22, 777);
            //Act
            nums.RemoveAt(5);
            //Assert

            Assert.That(() => { var index = nums[5]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(nums.ToString(), Is.EqualTo("[55, 25, 36, 777]"));
        }

        [Test]
        public void Test_Collection_Clear()
        {
            //Arrange and Act
            var numbers = new Collection<int>(5, 10, 15);

            //Act
            numbers.Clear();
            //Assert
            Assert.AreEqual(numbers.ToString(), "[]" ,"Verify collaction is empty");
        }

        //DDT
        [TestCase("Petar,Maria,Ivan", 0, "Petar")]
        [TestCase("Petar,Maria,Ivan", 1, "Maria")]
        [TestCase("Petar,Maria,Ivan", 2, "Ivan")]
        [TestCase("Petar", 0, "Petar")]
        public void Test_Collection_GetByValidIndex_DDT(string data, int index, string expected)
        {
            var coll = new Collection<string>(data.Split(","));
            var actual = coll[index];
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("", 0)]
        [TestCase("Petar", -1)]
        [TestCase("Petar", 1)]
        [TestCase("Petar,Maria,Steve", -1)]
        [TestCase("Petar,Maria,Steve", 3)]
        [TestCase("Petar,Maria,Steve", 150)]

        public void Test_Collection_GetByInvalidIndex_DDT(string data, int index)
        {
            var coll = new Collection<string>(data.Split(",") );
 
            Assert.That(() => coll[index], Throws.TypeOf<ArgumentOutOfRangeException>());

        }


    }
}