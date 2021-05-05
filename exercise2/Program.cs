using System;
using exercise2.LinkedList;

namespace exercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            var myList = new MySinglyLinkedList<int>();

            myList.Insert(1);
            myList.Insert(2,1);
            myList.Insert(3,0);

            myList.Delete(0);

            myList.Print();
        }
    }
}
