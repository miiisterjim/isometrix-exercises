using System;

namespace exercise2.LinkedList
{
    public class MySinglyLinkedList<T>
    {
        private Node<T> head;

        public MySinglyLinkedList(Node<T> head = null)
        {
            this.head = head; 
        }

        public void Insert(T data, int position = 0) 
        {
            var node = new Node<T>(data);

            if(head == null) 
            {
                if(position != 0) 
                {
                    return;
                }
                else {
                    head = node;
                    return;
                }
            } 

            if(position == 0) {
                node.next = this.head;
                this.head = node;
                return;
            }
            
            Node<T> current = this.head;
            Node<T> previous = null;

            int i = 0;

            while(i < position) {

                previous = current;
                current = current.next;

                if(current == null) {
                    break;
                }

                i++;
            }

            node.next = current;
            previous.next = node;            
        }


        public void Delete(int position) {

            if(this.head == null) {
                return;
            }

            if(position == 0) {
                this.head = head.next;
                return;
            }

            Node<T> current = this.head; 
           
            
            for(int i = 0; i < position - 1; i++) {                
                current = current.next;

                if(current == null) {
                    break;
                }
            }

            current.next = current.next.next;
        }

        public void Print() {
            
            var node = this.head;

            while(node != null) {                
                Console.WriteLine(node.ToString());
                node = node.next;
            }            
        }
        
    }
}