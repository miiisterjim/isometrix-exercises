namespace exercise2.LinkedList
{
    public sealed class Node<T>
    {
        public T data;
        public Node<T> next;

        public Node(T data, Node<T> next = null) 
        {
            this.data = data;
            this.next = next;
        }

        public override string ToString()
        {
            return $"Data: {data}";

        }
    }
}